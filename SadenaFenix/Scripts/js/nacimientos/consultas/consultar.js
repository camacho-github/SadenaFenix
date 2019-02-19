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
                $("#PorcentajeSubregistro").text(data.PorcentajeSubregistro + " %");

                /* Fill badges registro oportuno */
                $("#TotalRegistroOportuno").text(data.TotalRegistroOportuno);
                $("#PorcentajeRegistroOportuno").text(data.PorcentajeRegistroOportuno + " %");

                /* Fill badges registro extemporáneos */
                $("#TotalRegistroExtemporaneo").text(data.TotalRegistroExtemporaneo);
                $("#PorcentajeRegistroExtemporaneo").text(data.PorcentajeRegistroExtemporaneo + " %");

                /* Showing results */
                $('#cantidadesResumenConsulta').show();
            },
            error: function (request, status, error) {
                alert(request);
            }
        });
    });

    /* Get info to show in tables */
    $('#subregistradosLink, #registradosLink, #extemporaneosLink').click(function () {
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

                    /* TODO: Clean this part */
                    console.log(objSubRegistro);
                    console.log(objRegistroOportuno);
                    console.log(objRegistroExtemporaneo);

                    /* Fill table result */
                    prepareSubregistradosTable(objSubRegistro);
                   // prepareRegistradosTable(objRegistroOportuno);
                   // prepareExtemporaneosTable(objRegistroExtemporaneo);

                    /* Show results */
                    $('#resultadosPanelFooterTable').show();

                },
                error: function (request, status, error) {
                    alert(request);
                }
            }); 
        }
    });

    /* Get resumen por municipio. */
    $('#resumenTotalesLink').click(function () {
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

                    /* TODO: Clean this part */
                    console.log(objResumenMunicipios);

                    /* Fill table result */
                    //prepareResumenTotalesTable(objResumenMunicipios);

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

/* Reset components */
function cleanComponentsForConsulta() {
    /* Hide old results */
    $('#cantidadesResumenConsulta').hide();
    $('#resultadosPanelFooterTable').hide(); 
    objRegistroExtemporaneo = undefined;
    objRegistroOportuno = undefined;
    objSubRegistro = undefined;
    objResumenMunicipios = undefined;
}

/* Table: subregistradosTable */
function prepareSubregistradosTable(data) {
    /* Prepare subregistradosTable */
    $('#subregistradosTable').DataTable({
        responsive: true,
        scrollX: true,
        searching: true,
        ordering: true,
        paging: true,
        lengthChange: false,
        info: false,
        autoWidth: true,
        data: data
    });

}

