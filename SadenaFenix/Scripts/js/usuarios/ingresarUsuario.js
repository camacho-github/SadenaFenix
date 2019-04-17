
$(function () {
    if ($("#ErrorAcceso").val() !== undefined && $("#ErrorAcceso").val().length > 0) {
        fnShowDialogModal("Usuario incorrecto", "Los datos para iniciar sesión son inválidos")
    }  

    $.getJSON("https://api.ipify.org/?format=json", function (e) {
        $("#IP").val(e.ip);
    });     
});

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

