var objUsuarioAlta = {
    RolId: 0
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
   
    });
    
    $('#BotonCrearUsuario').click(function () {
        if (fnValidarGuardarUsuario() == true) {
            $('#BotonCrearUsuario').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
            fnGuardarUsuario();
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
    
});


function fnValidarGuardarUsuario() {
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

function fnObtenerJsonUsuarioAlta() {
                
    objUsuarioAlta.UsuarioDesc = $('#UsuarioDesc').val();
    objUsuarioAlta.CorreoE = $('#CorreoE').val();
    objUsuarioAlta.Contrasenia = $('#Contrasenia').val();    

    return JSON.stringify(objUsuarioAlta);
}

function fnGuardarUsuario() {
    var objArray = {
        "jsonUsuarioAlta": fnObtenerJsonUsuarioAlta()
    },
        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('GuardarUsuario', params);

        if (data !== "" && data !== null) {

            if (data.Cabecero.CodigoRespuesta == "1") {
                errorAlta = true;
                $('#BotonCrearUsuario').removeAttr('disabled').attr('class','btn btn-primary');
                fnShowDialogModal('Operación incompleta', 'El usuario ya existe o no puede ser guardado.');

            } else if (data.Cabecero.CodigoRespuesta == "0") {
                errorAlta = false;
                fnShowDialogModal('Operación correcta', 'El usuario fue guardado exitósamente.');
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



