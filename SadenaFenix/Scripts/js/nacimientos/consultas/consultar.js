/* Script for: Consultar
 */
var objUsuario;
var objRegistroExtemporaneo;
var objRegistroOportuno;
var objSubRegistro;
var objResumenMunicipios;
var CONST_ROL_ANALISTA = 3;

$(function () {

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
       
    //Initialize Select2 Elements
    $('.select2').select2({
        tags: "true",
        placeholder: "Selección...",
        allowClear: true
    });

    /* Unregistered table */
    $('#noRegistradosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Registered table */
    $('#registradosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Subregistered table */
    $('#extemporaneosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Summary table */
    var summaryData = [
        ['008', 'Escobedo', '13', '12', '0'],
        ['009', 'Francisco I.Madero', '23', '24', '1'],
        ['010', 'Frontera', '2', '1', '0'],
        ['011', 'General Cepeda', '9', '18', '35'],
        ['012', 'Guerrero', '22', '16', '3']
    ];
    $('#resumenTable').DataTable({
        'data': summaryData,
        'scrollX': true,
        'searching': false,
        'ordering': false,
        'paging': false,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    // Initialize components
    $('.treeview').blur(function () {
        $(this).removeClass('active');
    });

    $('.treeview').click(function () {
        $(this).addClass('active');
    });

    $('#loadBtn').click(function () {
        $('#infoModal').show();
    });

    $('#consultarDatosProcesadosBtn').click(function () {
        $.ajax({
            type: "POST",
            url: "ConsultarTotales",
            data: JSON.stringify(objUsuario),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                $("#TotalSubregistro").text(data.TotalSubregistro);
                $("#TotalRegistroOportuno").text(data.TotalRegistroOportuno);
                $("#TotalRegistroExtemporaneo").text(data.TotalRegistroExtemporaneo);
                
                $("#PorcentajeRegistroOportuno").text(data.PorcentajeRegistroOportuno + "%");
                $("#PorcentajeRegistroExtemporaneo").text(data.PorcentajeRegistroExtemporaneo + "%");
                $("#PorcentajeSubregistro").text(data.PorcentajeSubregistro + "%");
            },
            error: function (request, status, error) {
                alert(request);
            }
        });
        $('#searchingResult').show();
    });

    $('.consultaDetalleSubRegistro').click(function () {
        //provisionalmente para evitar que se consulte varias veces
        if (objSubRegistro == undefined) {
        
        $.ajax({
            type: "POST",
            url: "ConsultarSubRegistroNacimientos",
            data: JSON.stringify(objUsuario), 
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                if (objRegistroExtemporaneo == undefined) {
                    objRegistroExtemporaneo = data.ColExtemporaneos;
                }
                if (objRegistroOportuno == undefined) {
                    objRegistroOportuno = data.ColOportunos;
                }
                if (objSubRegistro == undefined) {
                    objSubRegistro = data.ColSubregistros;
                }                
            },
            error: function (request, status, error) {
                alert(request);
            }
            }); 

        }
    });


    $('#ResumenTotalesTab').click(function () {
        //provisionalmente para evitar que se consulte varias veces
        if (objResumenMunicipios == undefined) {

            $.ajax({
                type: "POST",
                url: "ConsultarReporteTotalesSubregistro",
                data: JSON.stringify(objUsuario),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    if (objResumenMunicipios == undefined) {
                        objResumenMunicipios = {
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

    


    $("#callImportarArchivo").click(function () {
        window.location.href = "/Archivos/Importar?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $("#callVerReportes").click(function () {
        window.location.href = "/Reportes/VerReportes?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    $("#callConsultar").click(function () {
        window.location.href = "/Consultas/Consultar?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    }); 

    

});