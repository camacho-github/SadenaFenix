var objMpiosMapas = {};
var mpiosList;
var mapVector
var jsonTotales;
var objTotales;


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

    $("#hrfooter").remove();

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

    fnObtenerTotalesMapa(params);

   $("#linkTodos").trigger("click");    
}

function PorcentajeColor(col, amt) {
    //amt = amt * 2;
    var usePound = false;
    if (col[0] == "#") {
        col = col.slice(1); usePound = true;
    }
    var num = parseInt(col, 16);
    var r = (num >> 16) + amt;
    if (r > 255)
        r = 255;
    else if (r < 0) r = 0;
    var b = ((num >> 8) & 0x00FF) + amt;
    if (b > 255)
        b = 255;
    else if (b < 0)
        b = 0;
    var g = (num & 0x0000FF) + amt;
    if (g > 255)
        g = 255;
    else if (g < 0)
        g = 0;
    return (usePound ? "#" : "") + (g | (b << 8) | (r << 16)).toString(16);
}

function fnObtenerTotalesMapa(params) {
    mapVector = $('#coahuilaMap').vectorMap('get', 'mapObject');       

    var data = fnGetJSONResponse('ReporteTotalesMunicipios', params);
    objTotales = {};

    if (data !== "" && data !== null) {

        if (data.JsonTotales !== null) {
            //jsonTotales = data.JsonTotales;
            var obj = $.parseJSON(data.JsonTotales);
            var sumaSubRegistro = 0;           
            var minimoSubRegistro = 999999;
            var maximoSubRegistro = 0;            


            $.each(obj, function (key, value) {
                var IdMunicipio = value.IdMunicipio;
                objMpiosMapas[IdMunicipio] = value;

                $("#bodySubregistroMapa").prepend('<tr class=' + IdMunicipio +'><td>' + IdMunicipio + '</td><td>' + value.MpioDesc + '</td><td>' + value.TotalSubregistro + '</td></tr>');
                                
                sumaSubRegistro = sumaSubRegistro + parseInt(value.TotalSubregistro);
                objMpiosMapas[IdMunicipio].TotalSubregistro = parseInt(value.TotalSubregistro);

                if (parseInt(value.TotalSubregistro) < minimoSubRegistro) {
                    minimoSubRegistro = parseInt(value.TotalSubregistro);
                }
                if (parseInt(value.TotalSubregistro) > maximoSubRegistro) {
                    maximoSubRegistro = parseInt(value.TotalSubregistro);
                }
                 
            });
            objTotales.sumaSubRegistro = sumaSubRegistro;            
            objTotales.minimoSubRegistro = minimoSubRegistro;
            objTotales.maximoSubRegistro = maximoSubRegistro; 

            $("#footSubRegistroMapa").prepend('<tr><th>TOTAL</td><th></td><td>' + sumaSubRegistro + '</td></tr>');

            $.each(obj, function (key, value) {
                var IdMunicipio = value.IdMunicipio;
               
                var porcentajeSubregistro = parseFloat(parseInt(value.TotalSubregistro) * 100 / objTotales.sumaSubRegistro);

                if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.sumaSubRegistro / 5 * 2)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#317f43', (100 - porcentajeSubregistro));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.sumaSubRegistro / 5 * 3)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#ffff00', (100 - porcentajeSubregistro));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.sumaSubRegistro)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#ff0000', (100 - porcentajeSubregistro));
                } 

                objMpiosMapas[IdMunicipio].porcentajeSubregistro = porcentajeSubregistro.toFixed(2);
                mapVector.regions[IdMunicipio].element.setStyle({'fill': '#CDD6D5'});
                mapVector.regions[IdMunicipio].element.config.style.selected.fill = objMpiosMapas[IdMunicipio].color;
                mapVector.setSelectedRegions(IdMunicipio);

            });           

        }
    }

    var hCols = [1];
    fnCrearTabla('SubRegistroMapaTabla', hCols, false);
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
