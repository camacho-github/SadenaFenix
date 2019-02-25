/* Script for: Consultar
 */
var objUsuario;
var objRegistroExtemporaneo;
var objRegistroOportuno;
var objSubRegistro;
var objResumenMunicipios;

/* On ready */
$(function () {

    /* Initialize Select2 */
    $('.select2').select2({
        tags: "true",
        placeholder: "Selección...",
        allowClear: true
    });

    /* Get totals and percents. */
    $('#consultarDatosProcesadosBtn').click(function () {
        /* Clean results */
        cleanComponentsForConsulta();
        $.ajax({
            type: "POST",
            url: "ConsultarTotales",
            data: JSON.stringify(objUsuario),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                /* Fill badges subregistro */
                $("#TotalSubregistro").text(data.TotalSubregistro);
                $("#PorcentajeSubregistro").text("".concat(data.PorcentajeSubregistro," %"));

                /* Fill badges registro oportuno */
                $("#TotalRegistroOportuno").text(data.TotalRegistroOportuno);
                $("#PorcentajeRegistroOportuno").text("".concat(data.PorcentajeRegistroOportuno, " %"));

                /* Fill badges registro extemporáneos */
                $("#TotalRegistroExtemporaneo").text(data.TotalRegistroExtemporaneo);
                $("#PorcentajeRegistroExtemporaneo").text("".concat(data.PorcentajeRegistroExtemporaneo, " %"));

                /* Showing results */
                $('#cantidadesResumenConsulta').show();
            },
            error: function (request, status, error) {
                alert(request);
            }
        });
    });

    /* Get info to show in tables */
    $('.description-block-review-link').click(function () {
        /* Provisionalmente para evitar que se consulte varias veces: This part is OK */
        if (objSubRegistro == undefined) {
            $.ajax({
                type: "POST",
                url: "ConsultarSubRegistroNacimientos",
                data: JSON.stringify(objUsuario), 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    /* Get result to fill tables */
                    objSubRegistro = data.ColSubregistros;
                    objRegistroOportuno = data.ColOportunos;
                    objRegistroExtemporaneo = data.ColExtemporaneos;

                    /* Fill result tables */
                    prepareSubregistroTable(objSubRegistro, 'subregistradosTable');
                    prepareSubregistroTable(objRegistroOportuno, 'registradosTable');
                    prepareSubregistroTable(objRegistroExtemporaneo, 'extemporaneosTable');

                    /* Show results */
                    $('#resultadosPanelFooterTable').show();

                },
                error: function (request, status, error) {
                    alert(request);
                }
            }); 
        }
    });

    /* Get review by municipio. */
    $('.description-block-review-link').click(function () {
        /* Provisionalmente para evitar que se consulte varias veces: This part is OK */
        if (objResumenMunicipios == undefined) {
            $.ajax({
                type: "POST",
                url: "ConsultarReporteTotalesSubregistro",
                data: JSON.stringify(objUsuario),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    /* Setting resumen in object */
                    objResumenMunicipios = {
                        cabeceros: data.ColCabeceros,
                        filas: data.ColFilas
                    };

                    /* Fill result table */
                    prepareResumenTotalesTable(objResumenMunicipios, 'resumenTotalesTable');

                    /* Show results */
                    $('#resultadosPanelFooterTable').show();

                },
                error: function (request, status, error) {
                    alert(request);
                }
            });
        }
    });

});

/* Reset components to show results */
function cleanComponentsForConsulta() {
    /* Hide old results */
    $('#cantidadesResumenConsulta').hide();
    $('#resultadosPanelFooterTable').hide(); 
    objRegistroExtemporaneo = undefined;
    objRegistroOportuno = undefined;
    objSubRegistro = undefined;
    objResumenMunicipios = undefined;

    return;
}

/* Prepare the table for the specific result */
function prepareSubregistroTable(data, tableId) {
    /* Prepare table */
    var subregistroTable = createSubregistroTable(tableId);
    var tbody = $('<tbody></tbody>');
    /* Pull records. */
    var tr;
    for (i in data) {
        tr = $('<tr></tr>');
        tr.append('<td>'.concat(data[i].Folio, '</td>'));
        tr.append('<td>'.concat(data[i].Edad, '</td>'));
        tr.append('<td>'.concat(data[i].EdoCivilId, '</td>'));
        tr.append('<td>'.concat(data[i].EdoCivilDesc, '</td>'));
        tr.append('<td>'.concat(data[i].Calle, '</td>'));
        tr.append('<td>'.concat(data[i].NoExt, '</td>'));
        tr.append('<td>'.concat(data[i].NoInt, '</td>'));
        tr.append('<td>'.concat(data[i].EdoId, '</td>'));
        tr.append('<td>'.concat(data[i].EdoDesc, '</td>'));
        tr.append('<td>'.concat(data[i].MpioId, '</td>'));
        tr.append('<td>'.concat(data[i].MpioDesc, '</td>'));
        tr.append('<td>'.concat(data[i].LocId, '</td>'));
        tr.append('<td>'.concat(data[i].LocDesc, '</td>'));
        tr.append('<td>'.concat(data[i].NumNacimiento, '</td>'));
        tr.append('<td>'.concat(data[i].EscolId, '</td>'));
        tr.append('<td>'.concat(data[i].EscolDesc, '</td>'));
        tr.append('<td>'.concat(data[i].Ocupacion, '</td>'));
        tr.append('<td>'.concat(data[i].FechaNacimiento, '</td>'));
        tr.append('<td>'.concat(data[i].HoraNacimiento, '</td>'));
        tr.append('<td>'.concat(data[i].SexoId, '</td>'));
        tr.append('<td>'.concat(data[i].SexoDesc, '</td>'));
        /* Adding row */
        tbody.append(tr);
    }
    /* Adding body */
    $(subregistroTable).append(tbody);

    /* Add to container. */
    var containerSelector = '#'.concat(tableId, 'Container');
    $(containerSelector).append(subregistroTable);

    /* Adding datable functionality */
    prepareSubregistroDataTable(tableId);
    $(containerSelector).fadeIn("slow");

    return;
}

/* Create a table element prepared to show results */
function createSubregistroTable(tableId) {
    /* Create subregistro table */
    var subregistroTable = $('<table></table>');
    $(subregistroTable).attr('id', tableId);
    $(subregistroTable).addClass('table table-striped table-hover responsive');

    /* Create headers */
    var thead = $('<thead></thead>');
    var trFirstHeader = $('<tr></tr>');
    $(trFirstHeader).append($('<th colspan="1" style="border-top: 2px solid #dddddd;">FOLIO</th>'));
    $(trFirstHeader).append($('<th colspan="3" style="border-top: 2px solid #dddddd;">DATOS DE LA MADRE</th>'));
    $(trFirstHeader).append($('<th colspan="9" style="border-top: 2px solid #dddddd;">DATOS DE LA RESIDENCIA DE LA MADRE</th>'));
    $(trFirstHeader).append($('<th colspan="4" style="border-top: 2px solid #dddddd;">DATOS SOCIOECONÓMICOS DE LA MADRE</th>'));
    $(trFirstHeader).append($('<th colspan="4" style="border-top: 2px solid #dddddd;">DATOS DEL RECIEN NACIDO</th>'));

    var trSecondHeader = $('<tr></tr>');
    $(trSecondHeader).append('<th>Folio del certificado de nacimiento</th>');
    $(trSecondHeader).append('<th>Edad</th>');
    $(trSecondHeader).append('<th>ID Estado civil</th>');
    $(trSecondHeader).append('<th>Estado civil</th>');
    $(trSecondHeader).append('<th>Calle</th>');
    $(trSecondHeader).append('<th>Número exterior</th>');
    $(trSecondHeader).append('<th>Número interior</th>');
    $(trSecondHeader).append('<th>ID Entidad</th>');
    $(trSecondHeader).append('<th>Entidad</th>');
    $(trSecondHeader).append('<th>ID Municipio</th>');
    $(trSecondHeader).append('<th>Municipio</th>');
    $(trSecondHeader).append('<th>ID Localidad</th>');
    $(trSecondHeader).append('<th>Localidad</th>');
    $(trSecondHeader).append('<th>Número de nacimiento vivo</th>');
    $(trSecondHeader).append('<th>ID Escolaridad</th>');
    $(trSecondHeader).append('<th>Escolaridad</th>');
    $(trSecondHeader).append('<th>Ocupación</th>');
    $(trSecondHeader).append('<th>Fecha de nacimiento</th>');
    $(trSecondHeader).append('<th>Hora de nacimiento</th>');
    $(trSecondHeader).append('<th>ID Sexo</th>');
    $(trSecondHeader).append('<th>Sexo</th>');

    /* Adding headers */
    $(thead).append(trFirstHeader);
    $(thead).append(trSecondHeader);
    $(subregistroTable).append(thead);

    return subregistroTable;
}

/* Prepare the table for review by municipio */
function prepareResumenTotalesTable(data, tableId) {
    /* Create resumenTotales table */
    var resumenTotalesTable = createResumenTotalesTable(data, tableId);
    var tbody = $('<tbody></tbody>');
    /* Pull records. */
    var tr;
    var dataRow;
    for (i in data.filas) {
        dataRow = data.filas[i].ColCeldas;
        tr = $('<tr></tr>');
        for (j in dataRow) {
            tr.append('<td>'.concat(dataRow[j].Valor, '</td>'));
        }
        /* Adding row */
        tbody.append(tr);
    }
    /* Adding body */
    $(resumenTotalesTable).append(tbody);

    /* Add to container. */
    var containerSelector = '#'.concat(tableId, 'Container');
    $(containerSelector).append(resumenTotalesTable);

    /* Adding datable functionality */
    prepareSubregistroDataTable(tableId);
    $(containerSelector).fadeIn("slow");

    return;
}

/* Create the table for review by municipio */
function createResumenTotalesTable(data, tableId) {
    /* Create resumenTotales table */
    var resumenTotalesTable = $('<table></table>');
    $(resumenTotalesTable).attr('id', tableId);
    $(resumenTotalesTable).addClass('table table-striped table-hover responsive');

    /* Create header */
    var thead = $('<thead></thead>');
    var trFirstHeader = $('<tr></tr>');
    for (i in data.cabeceros) {
        $(trFirstHeader).append($('<th style="border-top: 2px solid #dddddd;">'.concat(data.cabeceros[i], '</th>')));
    }
    /* Adding header */
    $(thead).append(trFirstHeader);
    $(resumenTotalesTable).append(thead);

    return resumenTotalesTable;
}

/* Adding datable functionality */
function prepareSubregistroDataTable(tableId) {
    /* Prepare registradosTable */
    var subregistroTable = $('#'.concat(tableId)).DataTable({
        //responsive: true,
        //scrollX: true,
        searching: true,
        ordering: true,
        paging: true,
        lengthChange: false,
        info: false,
        //autoWidth: true,
        buttons: [
            'excel', 'pdf'
        ]
    });
    /* Adding icons */
    $(subregistroTable.buttons[0]).prepend('<i class="fa fa-clipboard" aria-hidden="true"></i>');
    $(subregistroTable.buttons[1]).prepend('<i class="fa fa-file-excel-o" aria-hidden="true"></i>');
    $(subregistroTable.buttons[2]).prepend('<i class="fa fa-file-pdf-o" aria-hidden="true"></i>');
    $(subregistroTable.buttons[3]).prepend('<i class="fa fa-print" aria-hidden="true"></i>');

    return;
}

