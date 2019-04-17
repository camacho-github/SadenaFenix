$(function () {
    if ($("#ResultadoCarga").val() !== undefined && $("#ResultadoCarga").val() == "0") {
        fnShowDialogModal("Carga completa", "Los catálogos fueron agregados correctamente.")
    } else if ($("#ResultadoCarga").val() !== undefined && $("#ResultadoCarga").val() == "2") {
        fnShowDialogModal("Carga completa", "Los catálogos ya fueron cargados anteriormente.")
    }else if ($("#ResultadoCarga").val() !== undefined && $("#ResultadoCarga").val() == "-1") {
        fnShowDialogModal("Error", "Ocurrió un error en la carga de los catálogos.")
    }
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