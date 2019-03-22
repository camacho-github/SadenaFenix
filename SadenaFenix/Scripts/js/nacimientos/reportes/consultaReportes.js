var objMpiosMapas = {};
var mpiosList;
var mapVector
var jsonTotales;


window.onscroll = function (e) {
    var vertical_position = 0;
    if (pageYOffset)//usual
        vertical_position = pageYOffset;
    else if (document.documentElement.clientHeight)//ie
        vertical_position = document.documentElement.scrollTop;
    else if (document.body)//ie quirks
        vertical_position = document.body.scrollTop;

    if (vertical_position > 300) {
        if ((vertical_position + $("#vectorMapDiv").innerHeight()) < $(".footer-container").position().top) {
            $("#vectorMapDiv").css("top", (vertical_position - 320) + 'px');
        }
    } else {
        $("#vectorMapDiv").css("top", "");
    }

    //your_div.top = (vertical_position + 200) + 'px';//200 is arbitrary.. just to show you could now position it how you want
}


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

    mapVector = $('#coahuilaMap').vectorMap('get', 'mapObject');
    mpiosList = $("#MpiosLista").val();
    if (mpiosList.length == 0) {
        for (obj in objMap) {
            objMpiosMapas[obj] = obj;
            mapVector.regions[obj].element.setStyle({
                'fill': '#CDD6D5'
            });
            mapVector.setSelectedRegions([obj]);
        }
    }else if (mpiosList.length > 0) {
        for (var i = 0; i < mpiosList.length; i++) {
            //console.info(i);
            objMpiosMapas[mpiosList[i]] = mpiosList[i];
            mapVector.regions[mpiosList[i]].element.setStyle({
                'fill': '#CDD6D5'
            });
            mapVector.setSelectedRegions(mpiosList[i]);
        }
    } 

      
    
    
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

    fnObtenerTotalesMunicipio(params);

    $("#linkTodos").trigger("click");    
}

function fnObtenerTotalesMunicipio(params) {
    var data = fnGetJSONResponse('ReporteTotalesMunicipios', params);

    if (data !== "" && data !== null) {

        if (data.JsonTotales !== null) {
            jsonTotales = data.JsonTotales;
            fnMessage("Operación correcta", data.JsonTotales);
        }
    }
}

function fnCrearTablasEdadSubregistro() {
    var hCols = [];
    fnCrearTabla('SubRegistroTabla', hCols,false);
    fnCrearTabla('OportunosTabla', hCols,false);
    fnCrearTabla('ExtemporaneosTabla', hCols,false);
        
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);

    $('#REdadOpo').appendTo('#edadMadreOpoContainer');
    $('#REdadSub').appendTo('#edadMadreSubContainer');
    $('#REdadExt').appendTo('#edadMadreExtContainer');   
 
}

function fnCrearTablasEdoCivilSubregistro() {
    var hCols = [];
    fnCrearTabla('EdoCivSubTabla', hCols,false);
    fnCrearTabla('EdoCivOporTabla', hCols,false);
    fnCrearTabla('EdoCivExtTabla', hCols,false);

    $('#ResReporteEdoCivOpor').appendTo('#estadoCivilRegistradosTableContainer');
    $('#ResReporteEdoCivSub').appendTo('#estadoCivilSubregistradosTableContainer');
    $('#ResReporteEdoCivExt').appendTo('#estadoCivilRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasNumNacSubregistro() {
    var hCols = [];
    fnCrearTabla('NumNacSubTabla', hCols,false);
    fnCrearTabla('NumNacOporTabla', hCols,false);
    fnCrearTabla('NumNacExtTabla', hCols,false);

    $('#ResReporteNumNacOpor').appendTo('#numeroNacidosVivosRegistradosTableContainer');
    $('#ResReporteNumNacSub').appendTo('#numeroNacidosVivosSubregistradosTableContainer');
    $('#ResReporteNumNacExt').appendTo('#numeroNacidosVivosRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}



function fnCrearTablasEscolSubregistro() {
    var hCols = [];
    fnCrearTabla('EscolSubTabla', hCols,false);
    fnCrearTabla('EscolOporTabla', hCols,false);
    fnCrearTabla('EscolExtTabla', hCols,false);

    $('#ResReporteEscolOpor').appendTo('#escolaridadMadreRegistradosTableContainer');
    $('#ResReporteEscolSub').appendTo('#escolaridadMadreSubregistradosTableContainer');
    $('#ResReporteEscolExt').appendTo('#escolaridadMadreRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasSexoSubregistro() {
    var hCols = [];
    fnCrearTabla('SexoSubTabla', hCols,false);
    fnCrearTabla('SexoOporTabla', hCols,false);
    fnCrearTabla('SexoExtTabla', hCols,false);

    $('#ResReporteSexoOpor').appendTo('#sexoRecienNacidoRegistradosTableContainer');
    $('#ResReporteSexoSub').appendTo('#sexoRecienNacidoSubregistradosTableContainer');
    $('#ResReporteSexoExt').appendTo('#sexoRecienNacidoRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}
