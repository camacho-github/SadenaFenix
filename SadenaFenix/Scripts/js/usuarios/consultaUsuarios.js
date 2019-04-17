var errorEliminar = false;

$(function () {

    fnCrearTablaUsuarios();

    $('.actualizarUsuario').click(function () {
        var id = $(this).attr('idUsuario');
        window.location.href = "/Usuarios/ActualizarUsuario?id=" + id + "&userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $('.eliminarUsuario').click(function () {
        var id = $(this).attr('idUsuario');
        fnAdvertenciaEliminarUsuario(id);
    }); 

    $('#largeModalAcceptBtn').click(function () {
        var id = $(this).attr('idUsuario');
        fnEliminarUsuario(id);
    }); 

    $('#DialogoModal button').click(function () {
        if (!errorEliminar) {
            fnIrConsulta();
        }
    });
});

function fnCrearTablaUsuarios() {
    var hCols = [0,3,4,6];
    fnCrearTabla('usuariosTabla', hCols,false);    
}

function fnAdvertenciaEliminarUsuario(id) {
    var idDiv = $('#ConfirmacionModal');
    $('#largeModalAcceptBtn').attr('idUsuario',id);
    idDiv.find('#Titulo').text("Confirmación");
    idDiv.find('#Mensaje').text("El usuario seleccionado será eliminado, ¿desea continuar?");
    idDiv.modal({ backdrop: 'static' });
    return;
}

function fnEliminarUsuario(id) {
    var objArray = {
        "usuarioId": id
    }

    params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('EliminarUsuario', params);
        
        if (data !== "" && data !== null) {

            if (data.Cabecero.CodigoRespuesta == "1") {
                errorEliminar = true;
                $('#BotonEliminarUsuario').removeAttr('disabled').attr('class', 'btn btn-primary');
                fnShowDialogModal('Operación incompleta', 'El usuario no pudo ser eliminado.');

            } else if (data.Cabecero.CodigoRespuesta == "0") {
                errorEliminar = false;
                fnShowDialogModal('Operación correcta', 'El usuario fue eliminado exitosamente.');
            }

        }

    };

    fnWaitForLoading(fnComplete);

}

function fnMensajeConfirmacion(tittle, message) {
    var idDiv = $('#ConfirmacionModal');

    idDiv.find('#Titulo').text(tittle);
    idDiv.find('#Mensaje').text(message);       
    
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
