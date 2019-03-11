$(function () {


    $("#actualizarConfigDiasExtBtn").click(function () {
        //fnMessage("Operación correcta", "La configuración fue actualizada exitósamente", fnRefrescarConsulta);
        fnShowDialogModal('Operación correcta', 'La configuración fue actualizada exitósamente.');
    });

});


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