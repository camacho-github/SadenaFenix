/* Script for: Consultar
 */
var objUsuario;
var objRegistroExtemporaneo;
var objRegistroOportuno;
var objSubRegistro;
var objResumenMunicipios;
var objReporteEdadSubregistro;
var CONST_ROL_ANALISTA = 3;

$(function () {

    /* Session */
    if ($("#objUsuario").val() != undefined && $("#objUsuario").val().length > 0) {
        objUsuario = JSON.parse($("#objUsuario").val());
        $("#etiquetaSesionUsuarioDesc").text(objUsuario.UsuarioDesc);
        $("#etiquetaSesionCorreoE").text(objUsuario.CorreoE);

        if (objUsuario.Rol.RolId == CONST_ROL_ANALISTA) {
            $("#opcionImportarArchivos").hide();
            $("#estiloDivisionMenu").hide();
        }

    } else {
        $("#botonCirculoSesion").hide();
        $("#menuGeneral").hide();        
    }
       
    /* Initialize Select2 Elements */
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
                    prepareRegistradosTable(objRegistroOportuno);
                    prepareExtemporaneosTable(objRegistroExtemporaneo);

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
                    prepareResumenTotalesTable(objResumenMunicipios);

                    /* Show results */
                    $('#resultadosPanelFooterTable').show();

                },
                error: function (request, status, error) {
                    alert(request);
                }
            });
        }
    });

    /* Menu options */
    $("#callImportarArchivo").click(function () {
        window.location.href = "/Archivos/Importar?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callVerReportes").click(function () {
        window.location.href = "/Reportes/VerReportes?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $("#callConsultar").click(function () {
        window.location.href = "/Consultas/Consultar?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });  

    $("#callAcercaDe").click(function () {
        window.location.href = "/Home/About?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $("#callContacto").click(function () {
        window.location.href = "/Home/Contact?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#botonCargarArchivos").click(function () {
        function explode() {
            $("#defaultModal").show();
        }
        setTimeout(explode, 2000);       
    }); 

    $("#botonConsultar").click(function () {
        window.location.href = "/Consultas/Consultar?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $('#ConsultarReporteEdadSubregistro').click(function () {
        //provisionalmente para evitar que se consulte varias veces
        if (objReporteEdadSubregistro == undefined) {

            $.ajax({
                type: "POST",
                url: "ConsultarReporteEdadSubregistro",
                data: JSON.stringify(objUsuario),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    if (objReporteEdadSubregistro == undefined) {
                        objReporteEdadSubregistro = {
                            cabeceros: data.ColCabeceros,
                            filas: data.ColFilas
                        };
                    }
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
    var subregistradosTable = $('#subregistradosTable').DataTable({
        responsive: true,
        scrollX: true,
        searching: true,
        ordering: true,
        paging: true,
        lengthChange: false,
        info: false,
        //autoWidth: true,
        data: data,
        columns: [
            { data: 'Folio' }, // <th>Folio del certificado de nacimiento</th>
            { data: 'Edad' }, // <th>Edad</th>
            { data: 'EdoCivilId' }, // <th>ID Estado civil</th>
            { data: 'EdoCivilDesc' }, // <th>Estado civil</th>
            { data: 'Domicilio' }, // <th>Calle</th>
            //{ data: '' }, // <th>Número exterior</th>
            //{ data: '' }, // <th>Número interior</th>
            { data: 'EdoId' }, // <th>ID Entidad</th>
            //{ data: '' }, // <th>Entidad</th>
            { data: 'MpioId' }, // <th>ID Municipio</th>
            //{ data: '' }, // <th>Municipio</th>
            { data: 'LocId' }, // <th>ID Localidad</th>
            { data: 'LocDesc' }, // <th>Localidad</th>
            { data: 'NumNacimiento' }, // <th>Número de nacimiento vivo</th>
            { data: 'EscolId' }, // <th>ID Escolaridad</th>
            { data: 'EscolDesc' }, // <th>Escolaridad</th>
            { data: 'Ocupacion' }, // <th>Ocupación</th>
            { data: 'FechaNacimiento' }, // <th>Fecha de nacimiento</th>
            //{ data: '' }, // <th>Hora de nacimiento</th>
            { data: 'SexoId' }, // <th>ID Sexo</th>
            { data: 'SexoDesc' } // <th>Sexo</th>
        ],
        buttons: [
            { extend: 'copyHtml5', className: 'btn btn-primary btn-sm', text: ' Copiar filas', },
            { extend: 'excelHtml5', className: 'btn btn-primary btn-sm', text: ' Exportar a Excel' },
            { extend: 'pdfHtml5', className: 'btn btn-primary btn-sm', text: ' Exportar a PDF' },
            { extend: 'printHtml5', className: 'btn btn-primary btn-sm', text: ' Imprimir' }
        ]
    });
    /* Adding bottons to container */
    /*subregistradosTable.buttons().container()
        .appendTo($('#subregistradosTableBottonsContainer', subregistradosTable.table().container()));
    /* Adding icons */
    $(subregistradosTable.buttons[0]).prepend('<i class="fa fa-clipboard" aria-hidden="true"></i>');
    $(subregistradosTable.buttons[1]).prepend('<i class="fa fa-file-excel-o" aria-hidden="true"></i>');
    $(subregistradosTable.buttons[2]).prepend('<i class="fa fa-file-pdf-o" aria-hidden="true"></i>');
    $(subregistradosTable.buttons[3]).prepend('<i class="fa fa-print" aria-hidden="true"></i>');
}

/* Table registradosTable */
function prepareRegistradosTable(objRegistroOportuno) {
    /* Prepare registradosTable */
    var registradosTable = $('#registradosTable').DataTable({
        responsive: true,
        scrollX: true,
        searching: true,
        ordering: true,
        paging: true,
        lengthChange: false,
        info: false,
        //autoWidth: true,
        data: objRegistroOportuno,
        columns: [
            { data: 'Folio' }, // <th>Folio del certificado de nacimiento</th>
            { data: 'Edad' }, // <th>Edad</th>
            { data: 'EdoCivilId' }, // <th>ID Estado civil</th>
            { data: 'EdoCivilDesc' }, // <th>Estado civil</th>
            { data: 'Domicilio' }, // <th>Calle</th>
            //{ data: '' }, // <th>Número exterior</th>
            //{ data: '' }, // <th>Número interior</th>
            { data: 'EdoId' }, // <th>ID Entidad</th>
            //{ data: '' }, // <th>Entidad</th>
            { data: 'MpioId' }, // <th>ID Municipio</th>
            //{ data: '' }, // <th>Municipio</th>
            { data: 'LocId' }, // <th>ID Localidad</th>
            { data: 'LocDesc' }, // <th>Localidad</th>
            { data: 'NumNacimiento' }, // <th>Número de nacimiento vivo</th>
            { data: 'EscolId' }, // <th>ID Escolaridad</th>
            { data: 'EscolDesc' }, // <th>Escolaridad</th>
            { data: 'Ocupacion' }, // <th>Ocupación</th>
            { data: 'FechaNacimiento' }, // <th>Fecha de nacimiento</th>
            //{ data: '' }, // <th>Hora de nacimiento</th>
            { data: 'SexoId' }, // <th>ID Sexo</th>
            { data: 'SexoDesc' }, // <th>Sexo</th>
            //{ data: '' }, // <th>Fecha de registro (según folio)</th>
            //{ data: '' }, // <th>Fecha de registro (según fecha)</th>
            //{ data: '' } // <th>Días al nacimiento</th>
        ],
        buttons: [
            { extend: 'copy', className: 'btn btn-primary btn-sm', text: ' Copiar filas', },
            { extend: 'excel', className: 'btn btn-primary btn-sm', text: ' Exportar a Excel' },
            { extend: 'pdf', className: 'btn btn-primary btn-sm', text: ' Exportar a PDF' },
            { extend: 'print', className: 'btn btn-primary btn-sm', text: ' Imprimir' }
        ]
    });
    /* Adding bottons to container */
    /*registradosTable.buttons().container()
        .appendTo($('#registradosTableBottonsContainer', registradosTable.table().container()));
    /* Adding icons */
    $(registradosTable.buttons[0]).prepend('<i class="fa fa-clipboard" aria-hidden="true"></i>');
    $(registradosTable.buttons[1]).prepend('<i class="fa fa-file-excel-o" aria-hidden="true"></i>');
    $(registradosTable.buttons[2]).prepend('<i class="fa fa-file-pdf-o" aria-hidden="true"></i>');
    $(registradosTable.buttons[3]).prepend('<i class="fa fa-print" aria-hidden="true"></i>');
}

/* Prepare extemporaneosTable */
function prepareExtemporaneosTable(objRegistroExtemporaneo) {
    /* Prepare extemporaneosTable */
    var extemporaneosTable = $('#extemporaneosTable').DataTable({
        responsive: true,
        scrollX: true,
        searching: true,
        ordering: true,
        paging: true,
        lengthChange: false,
        info: false,
        //autoWidth: true,
        data: objRegistroExtemporaneo,
        columns: [
            { data: 'Folio' }, // <th>Folio del certificado de nacimiento</th>
            { data: 'Edad' }, // <th>Edad</th>
            { data: 'EdoCivilId' }, // <th>ID Estado civil</th>
            { data: 'EdoCivilDesc' }, // <th>Estado civil</th>
            { data: 'Domicilio' }, // <th>Calle</th>
            //{ data: '' }, // <th>Número exterior</th>
            //{ data: '' }, // <th>Número interior</th>
            { data: 'EdoId' }, // <th>ID Entidad</th>
            //{ data: '' }, // <th>Entidad</th>
            { data: 'MpioId' }, // <th>ID Municipio</th>
            //{ data: '' }, // <th>Municipio</th>
            { data: 'LocId' }, // <th>ID Localidad</th>
            { data: 'LocDesc' }, // <th>Localidad</th>
            { data: 'NumNacimiento' }, // <th>Número de nacimiento vivo</th>
            { data: 'EscolId' }, // <th>ID Escolaridad</th>
            { data: 'EscolDesc' }, // <th>Escolaridad</th>
            { data: 'Ocupacion' }, // <th>Ocupación</th>
            { data: 'FechaNacimiento' }, // <th>Fecha de nacimiento</th>
            //{ data: '' }, // <th>Hora de nacimiento</th>
            { data: 'SexoId' }, // <th>ID Sexo</th>
            { data: 'SexoDesc' }, // <th>Sexo</th>
            //{ data: '' }, // <th>Fecha de registro (según folio)</th>
            //{ data: '' }, // <th>Fecha de registro (según fecha)</th>
            //{ data: '' }  // <th>Días al nacimiento</th>
        ],
        buttons: [
            { extend: 'copy', className: 'btn btn-primary btn-sm', text: ' Copiar filas', },
            { extend: 'excel', className: 'btn btn-primary btn-sm', text: ' Exportar a Excel' },
            { extend: 'pdf', className: 'btn btn-primary btn-sm', text: ' Exportar a PDF' },
            { extend: 'print', className: 'btn btn-primary btn-sm', text: ' Imprimir' }
        ]
    });
    /* Adding bottons to container */
   /* extemporaneosTable.buttons().container()
        .appendTo($('#extemporaneosTableBottonsContainer', extemporaneosTable.table().container()));
    /* Adding icons */
    $(extemporaneosTable.buttons[0]).prepend('<i class="fa fa-clipboard" aria-hidden="true"></i>');
    $(extemporaneosTable.buttons[1]).prepend('<i class="fa fa-file-excel-o" aria-hidden="true"></i>');
    $(extemporaneosTable.buttons[2]).prepend('<i class="fa fa-file-pdf-o" aria-hidden="true"></i>');
    $(extemporaneosTable.buttons[3]).prepend('<i class="fa fa-print" aria-hidden="true"></i>');
}

/* Table resumenTotalesTable */
function prepareResumenTotalesTable(objResumenMunicipios) {
    /* Prepare resumenTotalesTable */
    var data = [];
    for (x in objResumenMunicipios.filas) {
        data.push({
            'ID Municipio': objResumenMunicipios.filas[x],
            'Municipio': objResumenMunicipios.filas[x],
            'Total Subregistro': objResumenMunicipios.filas[x],
            'Total Registro Oportuno': objResumenMunicipios.filas[x],
            'Total Registro Extemporaneo': objResumenMunicipios.filas[x]
        });
    }
    var resumenTotalesTable = $('#resumenTotalesTable').DataTable({
        responsive: true,
        scrollX: true,
        searching: false,
        ordering: false,
        paging: false,
        lengthChange: false,
        info: false,
        //autoWidth: true,
        /*data: objResumenMunicipios.ColFilas,
        columns: [
            { data: 'name' },
            { data: 'position' },
            { data: 'salary' },
            { data: 'salary' },
            { data: 'office' }
        ],*/
        buttons: [
            { extend: 'copy', className: 'btn btn-primary btn-sm', text: ' Copiar filas', },
            { extend: 'excel', className: 'btn btn-primary btn-sm', text: ' Exportar a Excel' },
            { extend: 'pdf', className: 'btn btn-primary btn-sm', text: ' Exportar a PDF' },
            { extend: 'print', className: 'btn btn-primary btn-sm', text: ' Imprimir' }
        ]
    });
    /* Adding bottons to container */
    resumenTotalesTable.buttons().container()
        .appendTo($('#resumenTotalesTableBottonsContainer', resumenTotalesTable.table().container()));
    /* Adding icons */
    $(resumenTotalesTable.buttons[0]).prepend('<i class="fa fa-clipboard" aria-hidden="true"></i>');
    $(resumenTotalesTable.buttons[1]).prepend('<i class="fa fa-file-excel-o" aria-hidden="true"></i>');
    $(resumenTotalesTable.buttons[2]).prepend('<i class="fa fa-file-pdf-o" aria-hidden="true"></i>');
    $(resumenTotalesTable.buttons[3]).prepend('<i class="fa fa-print" aria-hidden="true"></i>');
}
