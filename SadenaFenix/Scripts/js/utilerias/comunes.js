var objMeses = {};
var objAnios = {};
var objMpios = {};
var objAnioRegistro = {};


$(function () {
    $(".ValidacionTexto").on('keypress', function (event) {
        var regExp = /^[a-zA-z0-9 ]{0,245}$/;
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        var text = $(this).val();

        if (!regExp.test(key) || !regExp.test(text)) {
            event.preventDefault();
            return false;
        }
    });


    $("input.ValidacionTelefono").focus(function () {
        $(this).trigger("click");
    });

    $("input.ValidacionTelefono").click(function () {
        if (!$(this).hasClass("disable")) {
            var input = $(this);
            input.val(input.attr("realValue"));
            fnMoveCursorToEnd(input);
        }
    });

    $("input.ValidacionTelefono").on('blur', function (event) {
        var input = $(this),
            number = input.val();
        input.attr("realValue", input.val());
        fnSetFormatPhoneNumber(input, number);
        input.val($.trim(input.val()));
    });


    $("input.ValidacionNumero").on('keydown', function (event) {
        var okVal = funOnlyNumberExtended(event);
        return okVal;
    });

    /* Initialize Select2 */
    $('.select2').select2({
        placeholder: "Selección...",
        allowClear: true,
        "language": {
            "noResults": function () {
                return "No existen coincidencias";
            }
        }
    });

    $('.select2').on('select2:select', function (e) {
        var data = e.params.data;
        var idSelect = $(this).attr('id');
        if (idSelect == "MesLista")
            objMeses[data.id] = data.text;
        else if (idSelect == "AnioLista" || idSelect =="AnioNacimientoLista")
            objAnios[data.id] = data.text;
        else if (idSelect == "AnioRegistroLista")
            objAnioRegistro[data.id] = data.text;
        else if (idSelect == "MpiosLista")
            objMpios[data.id] = data.text;
                
        console.log(data);
        console.log($(this).val());
    });
});

function fnGetJSONResponse(actionName, parameters) {
    var httpRequest = fnPrepareHttpRequest(actionName);
    httpRequest.send(parameters);
    var content = httpRequest.responseText;
    return fnParseJSONResponse(content);
}

function fnPrepareHttpRequest(actionName) {
    var httpRequest = fnGenReqObj();
    httpRequest.open('POST', actionName, false);
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded; charset=UTF-8");

    return httpRequest;
}

/**get a new request */
function fnGenReqObj() {
    var httpRequest = null;
    if (window.XMLHttpRequest)
        httpRequest = new XMLHttpRequest();
    else if (window.ActiveXObject) {
        try {
            httpRequest = new ActiveXObject("MSXML2.XMLHTTP");
        } catch (e) {
            httpRequest = new AXObject("Microsoft.XMLHTTP");
        }
    }
    return httpRequest;
}

function fnParseJSONResponse(content) {
    var jsonResponse = "";

    try {
        jsonResponse = $.parseJSON(content);
    } catch (e) {
        jsonResponse = "";
    }

    return jsonResponse;
}

function fnGetAndSetTemplate(actionName, params, divName, fnName, noWait) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: actionName,
        data: params,
        converters: { 'text json': true },
        beforeSend: function () {
            if (noWait === undefined || noWait === null)
                fnWaitForPost();
        },
        success: function (data) {
            $("#" + divName).empty();
            $("#" + divName).html(data);
            fnCompleteWait();
        },
        error: function (xhr, textStatus) {
            console.error('Error during the ajax call. Error: '
                + xhr + ' ' + textStatus);
            fnCompleteWait();
        },
        complete: function () {
            if (fnName !== undefined && fnName !== null)
                fnName();
        }
    });

}

function fnGetAndSetTemplateNoAsync(actionName, params, divName, fnName, noWait) {
    fnWaitForPost();
    $.ajax({
        type: 'POST',
        async: false,
        cache: false,
        url: actionName,
        data: params,
        converters: { 'text json': true },
        beforeSend: function () {
           fnWaitForPost();
        },
        success: function (data) {
            $("#" + divName).empty();
            $("#" + divName).html(data);
            fnCompleteWait();
        },
        error: function (xhr, textStatus) {
            console.error('Error during the ajax call. Error: '
                + xhr + ' ' + textStatus);
            fnCompleteWait();
        },
        complete: function () {
            if (fnName !== undefined && fnName !== null)
                fnName();

            fnCompleteWait();
        }
    });

}

function fnConcatenaValoresObjeto(obj) {
    var index = 0;
    var result = "";
    $.each(obj, function (key, value) {
        if (index === 0) {
            result += value;
        } else {
            result += ", " + value;
        }
        index++;
    });
    return result;
}

function fnParamsString(objArray) {
    var paramsArray = "";
    var index = 0;
    $.each(objArray, function (key, value) {
        if (index === 0) {
            paramsArray += key + "=" + value;
        } else {
            paramsArray += "&" + key + "=" + value;
        }
        index++;
    });
    return paramsArray;
}

function fnMessage(title, message, finalFunction, clase) {
    var idDiv = $('#AlertaMensaje');

    if (clase != undefined) {
        idDiv.toggleClass("alert-success", false);
        idDiv.toggleClass(clase, true);
    }

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



function fnMensajeBotonesLista(tittle, message, btnList, clase) {
    var idDiv = $('#ConfirmacionMensaje'),
        containerDiv = $('#ConfirmacionMensaje #buttonsContainer'),
        transparentLayer = $('#transparentBackLayer');

    if (clase != undefined) {
        idDiv.toggleClass("alert-warning", false);
        idDiv.toggleClass(clase, true);
    }

    idDiv.find('#Titulo').text(tittle);
    idDiv.find('#Mensaje').text(message);

    containerDiv.empty();
    $.each(btnList, function (key, value) {
        var btn = $('<button/>',
            {
                text: key,
                class: containerDiv.attr('classToChildren'),
                click: function () {
                    transparentLayer.toggleClass("hiddElement", true);
                    idDiv.toggleClass("hiddElement", true);
                    value();
                }
            });

        containerDiv.append(btn);
    });

    idDiv.find('#closeMessage').on("click", function () {
        idDiv.toggleClass("hiddElement", true);
    });

    idDiv.toggleClass("hiddElement", false);
    transparentLayer.toggleClass("hiddElement", false);

    $('html, body').animate({
        scrollTop: 0
    }, 100);
}



function fnSetFormatPhoneNumber(element, number) {
    try {
        if (number !== "" && number.length > 0) {
            var tmp1 = number.substring(0, 3),
                tmp2 = number.substring(3, 6),
                tmp3 = number.substring(6);

            number = "(" + tmp1 + ")" + tmp2 + " - " + tmp3;
        }
        $(element).val(number);
    } catch (e) {
        console.error("Error at setFormat phone number cause: " + e);
    }

}

function fnMoveCursorToEnd(element) {
    try {
        var target = event.currentTarget || event.srcElement || event.target;
        var position = $(element).val().length;
        fnMoveCursor(target, position);
    } catch (e) {
        console.info("No target to move end" + e);
    }

}

function fnMoveCursor(o, newPosition) {
    if (o.createTextRange) {
        var r = o.createTextRange();
        var value = o.value;
        var moveTo = value.length - newPosition;
        r.moveStart('character', newPosition);
        r.moveEnd('character', -moveTo);
        r.select();
    } else {
        o.selectionStart = newPosition;
        o.selectionEnd = newPosition;
    }
}

function funOnlyNumberExtended(event) {
    var whichCode = (window.Event) ? event.which : event.keyCode;
    if (whichCode == 13) {
        return true;
    }
    if (whichCode == 8 || whichCode == 0) {
        return true;
    }
    if ((whichCode >= 48 && whichCode <= 57)
        || (whichCode >= 96 && whichCode <= 105)) {
        return true;
    } else {
        return false;
    }

}


function fnCrearTabla(nombreTabla, columnasOcultas, paginacion) {
    try {
        if (paginacion == undefined) {
            paginacion = true;
        }

        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes();
        var dateTime = date + ' ' + time;

        $('#' + nombreTabla).DataTable({
            "bFilter": false,
            "dom": 'Blfrtip',
            "paging": paginacion,
            "searching": true,
            "autoWidth": true,
            "columnDefs": [{
                "visible": false,
                "targets": columnasOcultas
            }],
            "buttons": [{
                extend: 'colvis',
                collectionLayout: 'three-column',
                text: function () {
                    var totCols = $('#' + nombreTabla + ' thead th').length;
                    
                    var hiddenCols = columnasOcultas.length;
                    
                    var shownCols = totCols - hiddenCols;
                    return 'Columnas (' + shownCols + ' de ' + totCols + ')';
                },
                prefixButtons: [{
                    extend: 'colvisGroup',
                    text: 'Mostrar todo',
                    show: ':hidden'
                },
                {
                    extend: 'colvisGroup',
                    text: 'Ocultar todo',
                    hide: ':visible'
                },
                {
                    extend: 'colvisRestore',
                    text: 'Restaurar'
                }]

            }, {
                text: 'Imprimir',
                title: ' ',
                extend: 'print',
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10pt');

                    //$(".buttons-print")[0].click();

                    $(win.document.body).find('table').css('font-size', '9px');
                    $(win.document.body).find('thead').prepend(
                        '<img src="http://localhost:54040/Content/images/cohauila-gob-logo.png" style="position:fixed; top:30px; left:30;" /><img src="http://localhost:54040/Content/images/bid-logo.png" style="position:fixed; top:30px; left:500px;" /><img src="http://localhost:54040/Content/images/canada-logo.png" style="position:fixed; top:30px; left:1000px;" /><img src="http://localhost:54040/Content/images/regcivil-gob-logo.png" style="position:fixed; top:30px; left:1500px;" /><div style="margin-top:130px">');

                    if ($("#vectorMapDiv").exists()) {
                        $(win.document.body).find('thead').prepend($("#coahuilaMap").clone().addClass("mapaImpresion"));
                    }

                    var headerColumns = $(win.document.body).find('thead').find("th").length;
                    var footer = '<TR> <TH ALIGN=LEFT COLSPAN=' + headerColumns + '><img src="http://localhost:54040/Content/images/pie_sadena6.png" /></TH></TR >';
                    $(win.document.body).find('tfoot').empty().prepend(footer);

                },
                footer: true,
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'collection',
                text: 'Exportar',
                buttons: [{
                    text: 'Excel',
                    extend: 'excelHtml5',
                    title: nombreTabla + "_" + dateTime,
                    footer: false,
                    exportOptions: {
                        columns: ':visible'
                    }
                }, {
                    text: 'CSV',
                    extend: 'csvHtml5',
                    title: nombreTabla + "_" + dateTime,
                    fieldSeparator: ';',
                    exportOptions: {
                        columns: ':visible'
                    }
                }, {
                    text: 'PDF',
                    title: ' ',
                    extend: 'print',
                    customize: function (win) {
                        $(win.document.body)
                            .css('font-size', '10pt');

                        $(win.document.body).find('table').css('font-size', '9px');
                        $(win.document.body).find('thead').prepend(
                            '<img src="http://localhost:54040/Content/images/cohauila-gob-logo.png" style="position:fixed; top:30px; left:30;" /><img src="http://localhost:54040/Content/images/bid-logo.png" style="position:fixed; top:30px; left:500px;" /><img src="http://localhost:54040/Content/images/canada-logo.png" style="position:fixed; top:30px; left:1000px;" /><img src="http://localhost:54040/Content/images/regcivil-gob-logo.png" style="position:fixed; top:30px; left:1500px;" /><div style="margin-top:130px"></div>');
                        var headerColumns = $(win.document.body).find('thead').find("th").length;
                        var footer = '<TR> <TH ALIGN=LEFT COLSPAN=' + headerColumns + '><img src="http://localhost:54040/Content/images/pie_sadena6.png" style="position:absolute; bottom:0; /></TH></TR >';
                        $(win.document.body).find('tfoot').empty().prepend(footer);

                    },
                    footer: true,
                    exportOptions: {
                        columns: ':visible'
                    }
                }]
            }]
            , language: {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "searchPlaceholder": "Buscar coincidencias",
                "search": "",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            }
            , "initComplete": function (settings, json) {
                // Adjust hidden columns counter text in button -->
                $('#' + nombreTabla).on('column-visibility.dt', function (e, settings, column, state) {
                    var visCols = $('#' + nombreTabla + ' thead tr:first th').length;
                    //Below: The minus 2 because of the 2 extra buttons Show all and Restore
                    var tblCols = $('.dt-button-collection li[aria-controls=' + nombreTabla + '] a').length - 3;
                    if (tblCols < 0) {
                        $('.buttons-colvis[aria-controls=' + nombreTabla + '] span').html('Columnas (' + visCols + ')');
                    } else {
                        $('.buttons-colvis[aria-controls=' + nombreTabla + '] span').html('Columnas (' + visCols + ' de ' + tblCols + ')');
                    }
                    $('.dt-button-collection li[aria-controls=' + nombreTabla + '] a').blur();
                    //$("a").blur();
                    e.stopPropagation();
                });
            }

        });
        $('.dataTables_length').addClass('bs-select');

        $('#' + nombreTabla).on('page.dt', function () {
            console.log('page change');
        });

    } catch (e) {
        console.log(e)
    }  

}

function fnCrearTablaSimple(nombreTabla, columnasOcultas, paginacion) {
    try {
        fnWaitForPost();

        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes();
        var dateTime = date + ' ' + time;

        if (paginacion == undefined) {
            paginacion = true;
        }

        $('#' + nombreTabla).DataTable({
            "bFilter": false,
            "dom": 'Blfrtip',
            "paging": paginacion,
            "searching": true,
            "autoWidth": true,
            "columnDefs": [{
                "visible": false,
                "targets": columnasOcultas
            }],
            "buttons": [{
                extend: 'colvis',
                collectionLayout: 'three-column',
                text: function () {
                    var totCols = $('#' + nombreTabla + ' thead th').length;
                    var hiddenCols = columnasOcultas.length;
                    var shownCols = totCols - hiddenCols;
                    return 'Columnas1 (' + shownCols + ' de ' + totCols + ')';
                },
                prefixButtons: [{
                    extend: 'colvisGroup',
                    text: 'Mostrar todo',
                    show: ':hidden'
                },
                {
                    extend: 'colvisGroup',
                    text: 'Ocultar todo',
                    hide: ':visible'
                },
                {
                    extend: 'colvisRestore',
                    text: 'Restaurar'
                }]

            }, {
                text: 'Imprimir',
                title: ' ',
                extend: 'print',
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10pt');

                    //$(".buttons-print")[0].click();

                    $(win.document.body).find('table').css('font-size', '9px');
                    $(win.document.body).find('thead').prepend(
                        '<img src="http://localhost:54040/Content/images/cohauila-gob-logo.png" style="position:fixed; top:30px; left:30;" /><img src="http://localhost:54040/Content/images/bid-logo.png" style="position:fixed; top:30px; left:500px;" /><img src="http://localhost:54040/Content/images/canada-logo.png" style="position:fixed; top:30px; left:1000px;" /><img src="http://localhost:54040/Content/images/regcivil-gob-logo.png" style="position:fixed; top:30px; left:1500px;" /><div style="margin-top:130px">');

                    if ($("#vectorMapDiv").exists()) {
                        $(win.document.body).find('thead').prepend($("#coahuilaMap").clone().addClass("mapaImpresion"));
                    }

                    var headerColumns = $(win.document.body).find('thead').find("th").length;
                    var footer = '<TR> <TH ALIGN=LEFT COLSPAN=' + headerColumns + '><img src="http://localhost:54040/Content/images/pie_sadena6.png" /></TH></TR >';
                    $(win.document.body).find('tfoot').empty().prepend(footer);

                },
                footer: true,
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'collection',
                text: 'Exportar',
                buttons: [{
                    text: 'Excel',
                    extend: 'excelHtml5',
                    title: nombreTabla + "_" + dateTime,
                    footer: false,
                    exportOptions: {
                        columns: ':visible'
                    }
                }, {
                    text: 'CSV',
                    extend: 'csvHtml5',
                    title: nombreTabla + "_" + dateTime,
                    fieldSeparator: ';',
                    exportOptions: {
                        columns: ':visible'
                    }
                }, {
                    text: 'PDF',
                    title: ' ',
                    extend: 'print',
                    customize: function (win) {
                        $(win.document.body)
                            .css('font-size', '10pt');

                        $(win.document.body).find('table').css('font-size', '9px');
                        $(win.document.body).find('thead').prepend(
                            '<img src="http://localhost:54040/Content/images/cohauila-gob-logo.png" style="position:fixed; top:30px; left:30;" /><img src="http://localhost:54040/Content/images/bid-logo.png" style="position:fixed; top:30px; left:500px;" /><img src="http://localhost:54040/Content/images/canada-logo.png" style="position:fixed; top:30px; left:1000px;" /><img src="http://localhost:54040/Content/images/regcivil-gob-logo.png" style="position:fixed; top:30px; left:1500px;" /><div style="margin-top:130px"></div>');
                        var headerColumns = $(win.document.body).find('thead').find("th").length;
                        var footer = '<TR> <TH ALIGN=LEFT COLSPAN=' + headerColumns + '><img src="http://localhost:54040/Content/images/pie_sadena6.png" style="position:absolute; bottom:0; /></TH></TR >';
                        $(win.document.body).find('tfoot').empty().prepend(footer);

                    },
                    footer: true,
                    exportOptions: {
                        columns: ':visible'
                    }
                }]
            }]
            , language: {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "searchPlaceholder": "Buscar coincidencias",
                "search": "",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            }
            , "initComplete": function (settings, json) {
                // Adjust hidden columns counter text in button -->
                $('#' + nombreTabla).on('column-visibility.dt', function (e, settings, column, state) {
                    var visCols = $('#' + nombreTabla + ' thead tr:first th').length;
                    //Below: The minus 2 because of the 2 extra buttons Show all and Restore
                    var tblCols = $('.dt-button-collection li[aria-controls=' + nombreTabla + '] a').length - 3;
                    if (tblCols < 0) {
                        $('.buttons-colvis[aria-controls=' + nombreTabla + '] span').html('Columnas (' + visCols + ')');
                    } else {
                        $('.buttons-colvis[aria-controls=' + nombreTabla + '] span').html('Columnas (' + visCols + ' de ' + tblCols + ')');
                    }                    
                    $('.dt-button-collection li[aria-controls=' + nombreTabla + '] a').blur();
                    //$("a").blur();
                    e.stopPropagation();
                });
            }

        });
        $('.dataTables_length').addClass('bs-select');
        fnCompleteWait();
    } catch (e) {
        console.log(e)
    }

}


$.fn.exists = function () {
    return this.val() !== undefined;
};

$.fn.isEmpty = function () {
    return this.val() === "";
};

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};