var objUsuarioAlta = {
    RolId: $("input#RolDesc").attr("identificador")
};
var errorAlta = false;

$(function () {

    var $selectRol = $('#RolesLista');
    $selectRol.attr('data-live-search', true);
    $selectRol.selectpicker(
    {
        width: '100%',
        title: '- [selecciona un rol del usuario] -',
        limit: 1,
        size: 6
    });


    $selectRol.on('change', function () {
        var nombreTexto = $(this).find("option:selected").text();
        var selected = $('#RolesLista option:selected').val();
        objUsuarioAlta.RolId = selected;  

        $("input#RolDesc").val(nombreTexto);

        $('div.selectRol').toggleClass("hiddElement", "true");
        $("input#RolDesc").toggleClass("hiddElement", "false");
   
    });
    
    $('#BotonActualizarUsuario').click(function () {
        if (fnValidarActualizarUsuario() == true) {
            if ($("#hidStatusId").val() == "1") {
                $('#BotonActualizarUsuario').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
                var UsuarioId = $(this).attr('UsuarioId');
                fnActualizarUsuario(UsuarioId);
            } else {
                var UsuarioId = $(this).attr('UsuarioId');
                fnConfirmarRestaurarUsuario(UsuarioId);               
            }           
        }
    });

    $('#DialogoModal button').click(function () {
        if (!errorAlta) {
            fnIrConsulta();
        }
        
    });

    $('#redirectConsultarUsuarios').click(function () {
        fnIrConsulta();
    });

    $("input#RolDesc").on('focus', function (event) {
        $(this).toggleClass("hiddElement", "true");
        $('div.selectRol').toggleClass("hiddElement", "false");

        $("#RolesLista").focus();
    });

    $(".toggle-password").click(function () {

        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });

    $('#ConfirmAcceptBtn').click(function () {
        var UsuarioId = $(this).attr('UsuarioId');
        fnActualizarUsuario(UsuarioId);
    }); 
    
});

function fnConfirmarRestaurarUsuario(UsuarioId) {
    var idDiv = $('#ConfirmacionModal');
    $('#ConfirmAcceptBtn').attr('UsuarioId', UsuarioId);
    idDiv.find('#Titulo').text("Confirmación");
    idDiv.find('#Mensaje').text("El usuario seleccionado fue eliminado, para actualizarlo es necesario restaurarlo, ¿desea continuar?");
    idDiv.modal({ backdrop: 'static' });
    return;
}


function fnValidarActualizarUsuario() {
    var valid = true;
    
    
    if ($('#UsuarioDesc').val() == '') {
        $('#valMessageUsuario').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageUsuario').text("");        
    }

    if ($('#CorreoE').val() == '') {
        $('#valMessageCorreo').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageCorreo').text("");
    }

    if (objUsuarioAlta.RolId <= 0) {
        $('#valMessageRol').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageRol').text("");
    }

    if ($('#Contrasenia').val() == '') {
        $('#valMessageContrasenia').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageCorreo').text("");
    }

    return valid;
}

function fnObtenerJsonUsuarioAlta(UsuarioId) {

    objUsuarioAlta.UsuarioId = UsuarioId;
    objUsuarioAlta.UsuarioDesc = $('#UsuarioDesc').val();
    objUsuarioAlta.CorreoE = $('#CorreoE').val();
    objUsuarioAlta.Contrasenia = $('#Contrasenia').val();    

    return JSON.stringify(objUsuarioAlta);
}

function fnActualizarUsuario(UsuarioId) {
    var objArray = {
        "jsonUsuarioAlta": fnObtenerJsonUsuarioAlta(UsuarioId)
    },
        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('ActualizarUsuario', params);

        if (data !== "" && data !== null) {

            if (data.Cabecero.CodigoRespuesta == "1") {
                errorAlta = true;
                $('#BotonActualizarUsuario').removeAttr('disabled').attr('class','btn btn-primary');
                fnShowDialogModal('Operación incompleta', 'El usuario no pudo ser actualizado.');

            } else if (data.Cabecero.CodigoRespuesta == "0") {
                errorAlta = false;
                fnShowDialogModal('Operación correcta', 'El usuario fue actualizado exitósamente.');
            }           
                
        }
    };

    fnWaitForLoading(fnComplete);
}

/* Showing large dialog modal. */
function fnShowDialogModal(title, text) {
    /* Prepare modal. */
    var modal = $('#DialogoModal');
    $(modal).find('.modal-title').html(title);
    $(modal).find('.modal-text').html(text);
    /* Showing */
    $(modal).modal({ backdrop: 'static' });
    return;
}

function fnIrConsulta() {
    fnWaitForPost();
    $("#callConsultarUsuarios").click();    
}



