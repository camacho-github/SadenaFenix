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
        fnShowDiv("contentCoberturaSIC", 1);
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
    fnGetAndSetTemplate('/AnalisisSIC/CoberturaSIC2', params,
        "CoberturaSICTable2", fnCrearTablasCoberturaSIC2);    

    fnWaitForPost();
    fnGetAndSetTemplate('/AnalisisSIC/CoberturaSIC3', params,
        "CoberturaSICTable3", fnCrearTablasCoberturaSIC3);

   

    fnGetAndSetTemplateNoAsync('/AnalisisSIC/TotalSINAC', params,
        "CoberturaTotalSINAC", fnObtenerTotalSINAC); 

    fnGetAndSetTemplate('/AnalisisSIC/InconsistenciasSIC', params,
        "InconsistenciasSICTable", fnCrearTablasInconsistenciasSIC); 

    fnGetAndSetTemplate('/AnalisisSIC/OtrosFoliosSIC', params,
        "OtrosFoliosSICTable", fnCrearTablasOtrosFoliosSIC); 

    fnGetAndSetTemplateNoAsync('/AnalisisSIC/CoberturaSIC', params,
        "CoberturaSICTable", fnCrearTablasCoberturaSIC); 

    fnShowDiv("modalConsulta", 0);
    //fnShowDiv("loader", 0);
    //fnCompleteWait();
}

function fnCrearTablasCoberturaSIC() {
    var hCols = [];
    fnCrearTabla('OportunoRelacionPorFolioTabla', hCols);
    fnCrearTabla('OportunoRelacionPorFechaTabla', hCols);
    

    $('#ROportunoRelacionPorFolio').appendTo('#OportunoRelacionPorFolioTableGralContainer');
    $('#ROportunoRelacionPorFecha').appendTo('#OportunoRelacionPorFechaTableGralContainer');
   
   
}

function fnCrearTablasCoberturaSIC2() {
    var hCols = [];
    fnCrearTabla('ExtemporaneoRelacionPorFolioTabla', hCols);
    fnCrearTabla('ExtemporaneoRelacionPorFechaTabla', hCols);

    $('#RExtemporaneoRelacionPorFolio').appendTo('#ExtemporaneoRelacionPorFolioTableGralContainer');
    $('#RExtemporaneoRelacionPorFecha').appendTo('#ExtemporaneoRelacionPorFechaTableGralContainer');
}

function fnCrearTablasCoberturaSIC3() {
    var hCols = [];
   
    fnCrearTabla('OportunoSinRelacionTabla', hCols);
    fnCrearTabla('ExtemporaneoSinRelacionTabla', hCols);

    $('#ROportunoSinRelacion').appendTo('#OportunoSinRelacionTableGralContainer');
    $('#RExtemporaneoSinRelacion').appendTo('#ExtemporaneoSinRelacionTableGralContainer');

    if ($("#objTotalesCobertura").val() != undefined && $("#objTotalesCobertura").val().length > 0) {
        objTotales = JSON.parse($("#objTotalesCobertura").val());

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
            $("#spanCoberturaRegistral").text(objTotales.SumatoriaTotal + " / " + porcentajeTotal + " % ");
        } else {
            $("#spanCoberturaRegistral").text(objTotales.SumatoriaTotal);
        }

    }

}

function fnObtenerTotalSINAC() {
    

    if ($("#valTotalSINAC").val() != undefined && $("#valTotalSINAC").val().length > 0) {
        totalSinac = parseInt($("#valTotalSINAC").val());

        $("#spanTotalSinac").text(totalSinac);
        
        if (objTotales != undefined && objTotales.SumatoriaTotal > 0) {
            var porcentajeTotal = (parseFloat(objTotales.SumatoriaTotal * 100 / totalSinac)).toFixed(2);
            $("#spanCoberturaRegistral").text(objTotales.SumatoriaTotal + " / " + porcentajeTotal + " % ");
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



 


