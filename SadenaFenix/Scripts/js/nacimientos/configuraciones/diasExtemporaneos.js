$(function () {

    if ($("#perfilInvalido").val() == 1) {
        $(".perfilInvalido").addClass("hiddElement");
    }

    $("#actualizarConfigDiasExtBtn").click(function () {
        var valor = parseInt($("#NoDiasExtemporaneos").val());
        if (valor > 0) 
            fnActualizarDiasExtemporaneos(valor);
        else {
            fnShowDialogModal('Operación incorrecta', 'El parámetro ingresado no es válido');
        }
    });

});

function fnActualizarDiasExtemporaneos(valor) {
    var objArray = {
        "valor": valor
    },
        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('ActualizarDiasExtemporaneos', params);

        if (data !== "" && data !== null) {
            fnShowDialogModal('Operación correcta', 'La configuración fue actualizada exitósamente.');
        }
    };

    fnWaitForLoading(fnComplete);
}


function fnRefrescarConsulta() {
    fnWaitForPost();
    $(".callConfigDiasExtemporaneos").click();
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


function fnMessageModal(title, message, finalFunction, clase) {
    var idDiv = $('#largeContinueModal');

    idDiv.stop(true, true);
    idDiv.removeAttr('style');
    idDiv.find('#Titulo').text(title);
    idDiv.find('#Mensaje').text(message);
    idDiv.fadeOut(10000, "linear", function () {
        if (finalFunction !== undefined)
            finalFunction();
    });
    idDiv.find('#CierreMensaje').on("click", function () {
        idDiv.stop(true, true);
    });

    $('html, body').animate({
        scrollTop: 0
    }, 100);
}