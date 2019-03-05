/* Script for: Importar
 */

/* On ready */
$(function () {

    /*
    $("#myBtn").click(function () {
        $("#myModal").modal({ backdrop: true });
    });
    $("#myBtn2").click(function () {
        $("#myModal2").modal({ backdrop: false });
    });
    $("#myBtn3").click(function () {
        $("#myModal3").modal({ backdrop: "static" });
    });
    $('input[type=file]').change(function () {
        var t = $(this).val();
        var inputText = 'Archivo: '.concat(t.substr(12, t.length));
        $(this).prev('input').val(inputText);
    });*/

    /* Validate loading files: SINAC
     * Example: COAHUILA_RC.SINAC_2018_CAMPOS_REQUERIDOS_SADENA.mdb
     */
    $('#SinacFile').change(function () {
        var isValid = false;
        var t = $(this).val();
        var fileName = t.substr(t.lastIndexOf('\\') + 1);
        var val = fileName.toLowerCase();
        if (val.includes('coahuila_rc.sinac_') == true) {
            if (val.includes('_campos_requeridos_sadena') == true) {
                val = val.replace(/coahuila_rc.sinac_/, '');
                val = val.replace(/_campos_requeridos_sadena/, '');
                var indexOf = val.indexOf('.');
                if (indexOf == 4) {
                    var anio = parseInt(val.substr(0, indexOf));
                    var extension = val.substr(indexOf);
                    if (extension == '.mdb' || extension == '.dbf' || extension == '.accdb') {
                        if (anio >= 1900) {
                            isValid = true;
                            $('#SinacFileText').val('Archivo: '.concat(fileName));
                        }
                    }
                }
            }
        }
        /* File not valid. */
        if (isValid == false) {
            $(this).val('');
            $('#SinacFileText').val('');
            showAlertModal('alert-warning', 'Información', 'El tipo de archivo es incorrecto o éste tiene una extensión no válida.')
        }
    });

    /* Validate loading files: SIC
     * Example: COAHUILA_RC.SIC_2018_CAMPOS_REQUERIDOS_SADENA.xlsx
     * */
    $('#SicFile').change(function () {
        var isValid = false;
        var t = $(this).val();
        var fileName = t.substr(t.lastIndexOf('\\') + 1);
        var val = fileName.toLowerCase();
        if (val.includes('coahuila_rc.sic_') == true) {
            if (val.includes('_campos_requeridos_sadena')) {
                val = val.replace(/coahuila_rc.sic_/, '');
                val = val.replace(/_campos_requeridos_sadena/, '');
                var indexOf = val.indexOf('.');
                if (indexOf == 4) {
                    var anio = parseInt(val.substr(0, indexOf));
                    var extension = val.substr(indexOf);
                    if (extension == '.xls' || extension == '.xlsx') {
                        if (anio >= 1900) {
                            isValid = true;
                            $('#SicFileText').val('Archivo: '.concat(fileName));
                        }
                    }
                }
            }
        }
        /* File not valid. */
        if (isValid == false) {
            $(this).val('');
            $('#SicFileText').val('');
            showAlertModal('alert-warning', 'Información', 'El tipo de archivo es incorrecto o éste tiene una extensión no válida.')
        }
    });

    /* Showing conversation. */
    $('#loadingFilesBtn').click(function () {
        var sinacFile = $('#SinacFile');
        var sicFile = $('#SicFile');
        if (sinacFile.val() != '' && sicFile.val() != '') {
            showLargeDialogModal('Mensaje', 'Los archivos seleccionados requieren de un tiempo de procesamiento, por favor espere a que estos sean cargados completamente.');
        } else {
            showAlertModal('alert-warning', 'Información', 'Por favor seleccione los archivos de SINAC y SIC.')
        }
    });

    /* Sending files. */
    $('#largeModalAcceptBtn').click(function () {
        $('#loadingFilesForm').submit();
        fnWaitForPost();
    });

});

/* Showing large dialog modal. */
function showLargeDialogModal(title, text) {
    /* Prepare modal. */
    var modal = $('#largeModal');
    $(modal).find('.modal-title').html(title);
    $(modal).find('.modal-text').html(text);
    /* Showing */
    $(modal).modal({ backdrop: 'static' });
    return;
}

/* Showing alert modal. */
function showAlertModal(className, title, text) {
    /* Prepare modal. */
    var modal = $('#alertModal');
    $(modal).find('.modal-title').html(title);
    $(modal).find('.modal-text').html(text);
    $(modal).find('.alert').first().addClass(className);
    /* Showing */
    $(modal).modal({ backdrop: true });
    return;
}