var objTotales;
var totalSinac;

$(function () {
       

    $("#consultarDatosProcesadosBtn").click(function () {        
        fnWaitForPost();
        fnWaitForLoading2(fnConsultarDatos);

        //var loader = $("#loader");
        //fnShowDiv(loader.attr('id'), 1);
        //fnConsultarDatos();
        //loader.fadeOut(5000, function () {
        //    loader.removeAttr('style');
        //    fnShowDiv(loader.attr('id'), 0);
        //});

        
        //fnWaitForLoading(fnConsultarDatos)
        //fnConsultarDatos();
        $("#linkRegistrosCompilados").trigger("click");
        fnShowDiv("contentCoverturaSIC", 1);
    });    

    $("#linkRegistrosCompilados").click(function () {
        fnShowDiv("naveindicator", 0, true);
        fnShowDiv("navRegistrosCompilados", 1);
        $(this).addClass("active");
    });
    $("#linkRegistrosSoloSic").click(function () {
        fnShowDiv("naveindicator", 0, true);
        fnShowDiv("navRegistrosSoloSic", 1);
        $(this).addClass("active");
    });
    $("#linkFoliosInconsistentes").click(function () {
        fnShowDiv("naveindicator", 0, true);
        fnShowDiv("navInconsistenciasSIC", 1);
        $(this).addClass("active");
    });
    $("#linkOtrosFoliosSIC").click(function () {
        fnShowDiv("naveindicator", 0, true);
        fnShowDiv("navOtrosFoliosSIC", 1);
        $(this).addClass("active");
    });

    $(".list-group-item").click(function () {
        $(".list-group-item").removeClass("active");
        $(this).addClass("active");

        //var tbCont = $(this).attr("tbCont");
        //if (tbCont != undefined) {
        //    fnShowDiv("report-by-indicator", 0, true);
        //    fnShowDiv(tbCont, 1, true);
        //} else {
        //    fnShowDiv("report-by-indicator", 1, true);
        //}
    });
   
});

function fnConsultarDatos() {
    fnWaitForPost();
    var objArray = {
        "MesesJson": JSON.stringify($("#MesLista").val()),
        "AniosRegistroJson": JSON.stringify($("#AnioRegistroLista").val()),
        "AniosNacimientoJson": JSON.stringify($("#AnioNacimientoLista").val()),
        "MpiosJson": JSON.stringify($("#MpiosLista").val()),
    },
        params = fnParamsString(objArray);   

    fnWaitForPost();
    fnGetAndSetTemplate('/AnalisisSIC/CoverturaSIC2', params,
        "CoverturaSICTable2", fnCrearTablasCoverturaSIC2);    

    fnWaitForPost();
    fnGetAndSetTemplate('/AnalisisSIC/CoverturaSIC3', params,
        "CoverturaSICTable3", fnCrearTablasCoverturaSIC3);

   

    fnGetAndSetTemplateNoAsync('/AnalisisSIC/TotalSINAC', params,
        "CoverturaTotalSINAC", fnObtenerTotalSINAC); 

    fnGetAndSetTemplate('/AnalisisSIC/InconsistenciasSIC', params,
        "InconsistenciasSICTable", fnCrearTablasInconsistenciasSIC); 

    fnGetAndSetTemplate('/AnalisisSIC/OtrosFoliosSIC', params,
        "OtrosFoliosSICTable", fnCrearTablasOtrosFoliosSIC); 

    fnGetAndSetTemplateNoAsync('/AnalisisSIC/CoverturaSIC', params,
        "CoverturaSICTable", fnCrearTablasCoverturaSIC); 

    fnShowDiv("modalConsulta", 0);
    //fnShowDiv("loader", 0);
    //fnCompleteWait();
}

function fnCrearTablasCoverturaSIC() {
    var hCols = [];
    fnCrearTabla('OportunoRelacionPorFolioTabla', hCols);
    fnCrearTabla('OportunoRelacionPorFechaTabla', hCols);
    

    $('#ROportunoRelacionPorFolio').appendTo('#OportunoRelacionPorFolioTableGralContainer');
    $('#ROportunoRelacionPorFecha').appendTo('#OportunoRelacionPorFechaTableGralContainer');
   
   
}

function fnCrearTablasCoverturaSIC2() {
    var hCols = [];
    fnCrearTabla('ExtemporaneoRelacionPorFolioTabla', hCols);
    fnCrearTabla('ExtemporaneoRelacionPorFechaTabla', hCols);

    $('#RExtemporaneoRelacionPorFolio').appendTo('#ExtemporaneoRelacionPorFolioTableGralContainer');
    $('#RExtemporaneoRelacionPorFecha').appendTo('#ExtemporaneoRelacionPorFechaTableGralContainer');
}

function fnCrearTablasCoverturaSIC3() {
    var hCols = [];
   
    fnCrearTabla('OportunoSinRelacionTabla', hCols);
    fnCrearTabla('ExtemporaneoSinRelacionTabla', hCols);

    $('#ROportunoSinRelacion').appendTo('#OportunoSinRelacionTableGralContainer');
    $('#RExtemporaneoSinRelacion').appendTo('#ExtemporaneoSinRelacionTableGralContainer');

    if ($("#objTotalesCovertura").val() != undefined && $("#objTotalesCovertura").val().length > 0) {
        objTotales = JSON.parse($("#objTotalesCovertura").val());

        $("#spanRegistrosCompilados").text(objTotales.TotalRegistrosCompilados + " / " + objTotales.PorcentajeRegistrosCompilados + " % ");
        $("#spanOportunoRelacionPorFolio").text(objTotales.TotalOportunoRelacionPorFolio + " / " + objTotales.PorcentajeOportunoRelacionPorFolio + " % ");
        $("#spanOportunoRelacionPorFecha").text(objTotales.TotalOportunoRelacionPorFecha + " / " + objTotales.PorcentajeOportunoRelacionPorFecha + " % ");
        $("#spanExtemporaneoRelacionPorFolio").text(objTotales.TotalExtemporaneoRelacionPorFolio + " / " + objTotales.PorcentajeExtemporaneoRelacionPorFolio + " %");
        $("#spanExtemporaneoRelacionPorFecha").text(objTotales.TotalExtemporaneoRelacionPorFecha + " / " + objTotales.PorcentajeExtemporaneoRelacionPorFecha + " %");

        $("#spanRegistrosSoloSic").text(objTotales.TotalRegistrosSinRelacion + " / " + objTotales.PorcentajeRegistrosSinRelacion + " % ");
        $("#spanOportunoSinRelacion").text(objTotales.TotalOportunoSinRelacion + " / " + objTotales.PorcentajeOportunoSinRelacion + " % ");
        $("#spanExtemporaneoSinRelacion").text(objTotales.TotalExtemporaneoSinRelacion + " / " + objTotales.PorcentajeExtemporaneoSinRelacion + " % ");


        if (totalSinac != undefined && totalSinac > 0) {
            var porcentajeTotal = (parseFloat(objTotales.SumatoriaTotal * 100 / totalSinac)).toFixed(2);
            $("#spanCoverturaRegistral").text(objTotales.SumatoriaTotal + " / " + porcentajeTotal + " % ");
        } else {
            $("#spanCoverturaRegistral").text(objTotales.SumatoriaTotal);
        }

    }

}

function fnObtenerTotalSINAC() {
    

    if ($("#valTotalSINAC").val() != undefined && $("#valTotalSINAC").val().length > 0) {
        totalSinac = parseInt($("#valTotalSINAC").val());

        $("#spanTotalSinac").text(totalSinac);
        
        if (objTotales != undefined && objTotales.SumatoriaTotal > 0) {
            var porcentajeTotal = (parseFloat(objTotales.SumatoriaTotal * 100 / totalSinac)).toFixed(2);
            $("#spanCoverturaRegistral").text(objTotales.SumatoriaTotal + " / " + porcentajeTotal + " % ");
        }
             

    }

}

function fnCrearTablasInconsistenciasSIC() {
    var hCols = [];
    fnCrearTabla('CaracteresEspecialesTabla', hCols);
    fnCrearTabla('DuplicadosSICTabla', hCols);

    $('#RCaracteresEspeciales').appendTo('#CaracteresEspecialesTableGralContainer');
    $('#RDuplicadosSIC').appendTo('#DuplicadosSICTableGralContainer');

    var totalCaracteresEspeciales = 0;
    var totalDuplicadosSIC = 0;
    if ($("#totalCaracteresEspeciales").val() != undefined && $("#totalCaracteresEspeciales").val().length > 0) {
        totalCaracteresEspeciales = parseInt($("#totalCaracteresEspeciales").val());            

        $("#spanCaracteresEspeciales").text(totalCaracteresEspeciales);

    }

    if ($("#totalDuplicadosSIC").val() != undefined && $("#totalDuplicadosSIC").val().length > 0) {
        totalDuplicadosSIC = parseInt($("#totalDuplicadosSIC").val());       

        $("#spanDuplicadosSIC").text(totalDuplicadosSIC);
    }

    $("#spanFoliosInconsistentes").text(totalCaracteresEspeciales + totalDuplicadosSIC);
}

function fnCrearTablasOtrosFoliosSIC() {
    var hCols = [];
    fnCrearTabla('OtrosEstadosTabla', hCols);
    fnCrearTabla('OtrosAnosTabla', hCols);

    $('#ROtrosEstados').appendTo('#OtrosEstadosTableGralContainer');
    $('#ROtrosAnos').appendTo('#OtrosAnosTableGralContainer');

    var totalOtrosEstados = 0;
    var totalOtrosAnos = 0;
    if ($("#totalOtrosEstados").val() != undefined && $("#totalOtrosEstados").val().length > 0) {
        totalOtrosEstados = parseInt($("#totalOtrosEstados").val());

        $("#spanOtrosEstados").text(totalOtrosEstados);

    }

    if ($("#totalOtrosAnos").val() != undefined && $("#totalOtrosAnos").val().length > 0) {
        totalOtrosAnos = parseInt($("#totalOtrosAnos").val());

        $("#spanOtrosAnos").text(totalOtrosAnos);
    }

    $("#spanOtrosFoliosSIC").text(totalOtrosEstados + totalOtrosAnos);
}



 


