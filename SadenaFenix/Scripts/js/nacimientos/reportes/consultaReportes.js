
$(function () {
       

    $("#consultarDatosProcesadosBtn").click(function () {        
        fnConsultarDatos();
    });

    $("#linkTodos").click(function () {
        fnShowDiv("report-by-indicator", 1, true);
        $(this).addClass("active");        
    });

    $(".list-group-item").click(function () {
        $(".list-group-item").removeClass("active");
        $(this).addClass("active");

        var tbCont = $(this).attr("tbCont");
        if (tbCont != undefined) {
            fnShowDiv("report-by-indicator", 0, true);
            fnShowDiv(tbCont, 1, true);
        } else {
            fnShowDiv("report-by-indicator", 1, true);
        }
        

    });
   
});

function fnConsultarDatos() {
    fnWaitForPost();
    var objArray = {
        "MesesJson": JSON.stringify($("#MesLista").val()),
        "AniosJson": JSON.stringify($("#AnioLista").val()),
        "MpiosJson": JSON.stringify($("#MpiosLista").val()),
    },
        params = fnParamsString(objArray);
    
    fnGetAndSetTemplate('/Reportes/ReportesEdad', params,
        "tablaContenedorEdad", fnCrearTablasEdadSubregistro);  

    fnGetAndSetTemplate('/Reportes/ReportesEdoCivil', params,
        "tablaContenedorECiv", fnCrearTablasEdoCivilSubregistro);     

    fnGetAndSetTemplate('/Reportes/ReportesNumNac', params,
        "tablaContenedorNumNac", fnCrearTablasNumNacSubregistro);     

    fnGetAndSetTemplate('/Reportes/ReportesEscolaridad', params,
        "tablaContenedorEscol", fnCrearTablasEscolSubregistro);   

    fnGetAndSetTemplate('/Reportes/ReportesSexo', params,
        "tablaContenedorSexo", fnCrearTablasSexoSubregistro);      

    $("#linkTodos").trigger("click");
    
}

function fnCrearTablasEdadSubregistro() {
    var hCols = [2, 3, 5, 7, 9, 17, 19];
    fnCrearTabla('SubRegistroTabla', hCols);
    fnCrearTabla('OportunosTabla', hCols);
    fnCrearTabla('ExtemporaneosTabla', hCols);
        
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);

    $('#REdadOpo').appendTo('#edadMadreOpoContainer');
    $('#REdadSub').appendTo('#edadMadreSubContainer');
    $('#REdadExt').appendTo('#edadMadreExtContainer');   
 
}

function fnCrearTablasEdoCivilSubregistro() {
    var hCols = [];
    fnCrearTabla('EdoCivSubTabla', hCols);
    fnCrearTabla('EdoCivOporTabla', hCols);
    fnCrearTabla('EdoCivExtTabla', hCols);

    $('#ResReporteEdoCivOpor').appendTo('#estadoCivilRegistradosTableContainer');
    $('#ResReporteEdoCivSub').appendTo('#estadoCivilSubregistradosTableContainer');
    $('#ResReporteEdoCivExt').appendTo('#estadoCivilRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasNumNacSubregistro() {
    var hCols = [];
    fnCrearTabla('NumNacSubTabla', hCols);
    fnCrearTabla('NumNacOporTabla', hCols);
    fnCrearTabla('NumNacExtTabla', hCols);

    $('#ResReporteNumNacOpor').appendTo('#numeroNacidosVivosRegistradosTableContainer');
    $('#ResReporteNumNacSub').appendTo('#numeroNacidosVivosSubregistradosTableContainer');
    $('#ResReporteNumNacExt').appendTo('#numeroNacidosVivosRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}



function fnCrearTablasEscolSubregistro() {
    var hCols = [];
    fnCrearTabla('EscolSubTabla', hCols);
    fnCrearTabla('EscolOporTabla', hCols);
    fnCrearTabla('EscolExtTabla', hCols);

    $('#ResReporteEscolOpor').appendTo('#escolaridadMadreRegistradosTableContainer');
    $('#ResReporteEscolSub').appendTo('#escolaridadMadreSubregistradosTableContainer');
    $('#ResReporteEscolExt').appendTo('#escolaridadMadreRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasSexoSubregistro() {
    var hCols = [];
    fnCrearTabla('SexoSubTabla', hCols);
    fnCrearTabla('SexoOporTabla', hCols);
    fnCrearTabla('SexoExtTabla', hCols);

    $('#ResReporteSexoOpor').appendTo('#sexoRecienNacidoRegistradosTableContainer');
    $('#ResReporteSexoSub').appendTo('#sexoRecienNacidoSubregistradosTableContainer');
    $('#ResReporteSexoExt').appendTo('#sexoRecienNacidoRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}
