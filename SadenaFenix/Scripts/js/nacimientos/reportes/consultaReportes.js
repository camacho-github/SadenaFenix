﻿var objMpiosMapas = {};
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
    
    $(".subregistradosPane").click(function () {
        var table = $("#SubRegistroMapaTabla").DataTable();
        table.columns().visible(false);
        table.columns([0, 1, 2]).visible(true);

        objMpiosMapas.seleccion = "subregistrados";

        $.each(objMpiosMapas, function (cod, value) {            

            mapVector.regions[cod].element.config.style.selected.fill = objMpiosMapas[cod].color;
            if (mapVector.regions[cod].element.isSelected) {
                mapVector.regions[cod].element.setStyle({ 'fill': '#CDD6D5' });
                mapVector.setSelectedRegions(cod);
            }
                
        });
    });

    $(".registradosPane").click(function () {
        var table = $("#SubRegistroMapaTabla").DataTable();
        table.columns().visible(false);
        table.columns([0, 1, 3]).visible(true);

        objMpiosMapas.seleccion = "registrados";

        $.each(objMpiosMapas, function (cod, value) {
            mapVector.regions[cod].element.config.style.selected.fill = objMpiosMapas[cod].colorOportuno; 

            if (mapVector.regions[cod].element.isSelected) {
                mapVector.regions[cod].element.setStyle({ 'fill': '#CDD6D5' });
                mapVector.setSelectedRegions(cod);
            }
        });
    });

    $(".extemporaneosPane").click(function () {
        var table = $("#SubRegistroMapaTabla").DataTable();
        table.columns().visible(false);
        table.columns([0, 1, 4]).visible(true);

        objMpiosMapas.seleccion = "registradosExt";

        $.each(objMpiosMapas, function (cod, value) {
            mapVector.regions[cod].element.config.style.selected.fill = objMpiosMapas[cod].colorExt;

            if (mapVector.regions[cod].element.isSelected) {
                mapVector.regions[cod].element.setStyle({ 'fill': '#CDD6D5' });
                mapVector.setSelectedRegions(cod);
            }
        });
    });

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


    //$("body").on("click", ".dataTables_paginate", function () {
    //    alert($(this).text());
    //});

    //$('.reports').on("click",'.dataTables_paginate', function () {
    //    alert('Page Clicked');
    //});
       
});

function fnConsultarDatos() {
    fnWaitForPost();
    var objArray = {
        "MesesJson": JSON.stringify($("#MesLista").val()),
        "AniosJson": JSON.stringify($("#AnioLista").val()),
        "MpiosJson": JSON.stringify($("#MpiosLista").val()),
        "MesesDesc": fnConcatenaValoresObjeto(objMeses),
        "AniosDesc": fnConcatenaValoresObjeto(objAnios),        
        "MpiosDesc": fnConcatenaValoresObjeto(objMpios),
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
            var sumaRegistroOportuno = 0;
            var sumaRegistroExtemporaneo = 0;             
            var minimoSubRegistro = 999999;
            var maximoSubRegistro = 0;
            var minimoRegistro = 999999;
            var maximoRegistro = 0;
            var minimoRegistroExt = 999999;
            var maximoRegistroExt = 0;


            $.each(obj, function (key, value) {
                var IdMunicipio = value.IdMunicipio;
                objMpiosMapas[IdMunicipio] = value;

                $("#bodySubregistroMapa").prepend('<tr class=' + IdMunicipio + '><td>' + IdMunicipio + '</td><td>' + value.MpioDesc + '</td><td>' + value.TotalSubregistro + '</td><td>' + value.TotalRegistroOportuno + '</td><td>' + value.TotalRegistroExtemporaneo + '</td></tr>');
                                
                sumaSubRegistro = sumaSubRegistro + parseInt(value.TotalSubregistro);
                sumaRegistroOportuno = sumaRegistroOportuno + parseInt(value.TotalRegistroOportuno);
                sumaRegistroExtemporaneo = sumaRegistroExtemporaneo + parseInt(value.TotalRegistroExtemporaneo);
                objMpiosMapas[IdMunicipio].TotalSubregistro = parseInt(value.TotalSubregistro);
                objMpiosMapas[IdMunicipio].TotalRegistroOportuno = parseInt(value.TotalRegistroOportuno);
                objMpiosMapas[IdMunicipio].TotalRegistroExtemporaneo = parseInt(value.TotalRegistroExtemporaneo);

                if (parseInt(value.TotalSubregistro) < minimoSubRegistro) {
                    minimoSubRegistro = parseInt(value.TotalSubregistro);
                }
                if (parseInt(value.TotalSubregistro) > maximoSubRegistro) {
                    maximoSubRegistro = parseInt(value.TotalSubregistro);
                }
                if (parseInt(value.TotalRegistroOportuno) < minimoRegistro) {
                    minimoRegistro = parseInt(value.TotalRegistroOportuno);
                }
                if (parseInt(value.TotalRegistroOportuno) > maximoRegistro) {
                    maximoRegistro = parseInt(value.TotalRegistroOportuno);
                }
                if (parseInt(value.TotalRegistroExtemporaneo) < minimoRegistroExt) {
                    minimoRegistroExt = parseInt(value.TotalRegistroExtemporaneo);
                }
                if (parseInt(value.TotalRegistroExtemporaneo) > maximoRegistroExt) {
                    maximoRegistroExt = parseInt(value.TotalRegistroExtemporaneo);
                }
                 
            });
            objTotales.sumaSubRegistro = sumaSubRegistro;
            objTotales.sumaRegistroOportuno = sumaRegistroOportuno;  
            objTotales.sumaRegistroExtemporaneo = sumaRegistroExtemporaneo;  
            objTotales.minimoSubRegistro = minimoSubRegistro;
            objTotales.maximoSubRegistro = maximoSubRegistro;
            objTotales.minimoRegistro = minimoRegistro;
            objTotales.maximoRegistro = maximoRegistro;
            objTotales.minimoRegistroExt = minimoRegistroExt;
            objTotales.maximoRegistroExt = maximoRegistroExt;

            $("#footSubRegistroMapa").prepend('<tr><th>TOTAL</th><th></th><th>' + sumaSubRegistro + '</th><th>' + sumaRegistroOportuno + '</th><th>' + sumaRegistroExtemporaneo + '</th></tr>');

            $.each(obj, function (key, value) {
                var IdMunicipio = value.IdMunicipio;
               
                var porcentajeSubregistro = parseFloat(parseInt(value.TotalSubregistro) * 100 / objTotales.maximoSubRegistro);

                if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.maximoSubRegistro / 3)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#317f43', (100 - porcentajeSubregistro));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.maximoSubRegistro / 3 * 2)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#ffff00', (100 - porcentajeSubregistro));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalSubregistro) <= parseFloat(objTotales.maximoSubRegistro)) {
                    objMpiosMapas[IdMunicipio].color = PorcentajeColor('#ff0000', (100 - porcentajeSubregistro));
                } 


                var porcentajeRegistro = parseFloat(parseInt(value.TotalRegistroOportuno) * 100 / objTotales.maximoRegistro);

                if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroOportuno) <= parseFloat(objTotales.maximoRegistro / 3)) {
                    objMpiosMapas[IdMunicipio].colorOportuno = PorcentajeColor('#ff0000', (100 - porcentajeRegistro));                    
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroOportuno) <= parseFloat(objTotales.maximoRegistro / 3 * 2)) {
                    objMpiosMapas[IdMunicipio].colorOportuno = PorcentajeColor('#ffff00', (100 - porcentajeRegistro));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroOportuno) <= parseFloat(objTotales.maximoRegistro)) {
                    objMpiosMapas[IdMunicipio].colorOportuno = PorcentajeColor('#317f43', (100 - porcentajeRegistro));                    
                } 

                var porcentajeRegistroExt = parseFloat(parseInt(value.TotalRegistroExtemporaneo) * 100 / objTotales.maximoRegistroExt);

                if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroExtemporaneo) <= parseFloat(objTotales.maximoRegistroExt / 3)) {
                    objMpiosMapas[IdMunicipio].colorExt = PorcentajeColor('#317f43', (100 - porcentajeRegistroExt));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroExtemporaneo) <= parseFloat(objTotales.maximoRegistroExt / 3 * 2)) {
                    objMpiosMapas[IdMunicipio].colorExt = PorcentajeColor('#ffff00', (100 - porcentajeRegistroExt));
                } else if (parseInt(objMpiosMapas[IdMunicipio].TotalRegistroExtemporaneo) <= parseFloat(objTotales.maximoRegistroExt)) {
                    objMpiosMapas[IdMunicipio].colorExt = PorcentajeColor('#ff0000', (100 - porcentajeRegistroExt));
                } 

                objMpiosMapas[IdMunicipio].porcentajeSubregistro = porcentajeSubregistro.toFixed(2);
                objMpiosMapas[IdMunicipio].porcentajeRegistro = porcentajeRegistro.toFixed(2);
                objMpiosMapas[IdMunicipio].porcentajeRegistroExt = porcentajeRegistroExt.toFixed(2);
                mapVector.regions[IdMunicipio].element.setStyle({'fill': '#CDD6D5'});
                mapVector.regions[IdMunicipio].element.config.style.selected.fill = objMpiosMapas[IdMunicipio].color;
                mapVector.setSelectedRegions(IdMunicipio);

            });           

        }
    }

    var hCols = [];
    fnCrearTabla('SubRegistroMapaTabla', hCols, false);

    var table = $("#SubRegistroMapaTabla").DataTable();
    table.columns().visible(false);
    table.columns([0, 1, 2]).visible(true);
}

function fnCrearTablasEdadSubregistro() {
    var hCols = [];
    fnCrearTabla('SubRegistroTabla', hCols, false);
    fnCrearTabla('OportunosTabla', hCols, false);
    fnCrearTabla('ExtemporaneosTabla', hCols, false);
        
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);

    $('#REdadOpo').appendTo('#edadMadreOpoContainer');
    $('#REdadSub').appendTo('#edadMadreSubContainer');
    $('#REdadExt').appendTo('#edadMadreExtContainer');

    $("#divFechaReporte").text($("#valFechaReporte").val());
    $("#divMesesReporte").text("Meses: " + $("#valMesesReporte").val());
    $("#divAniosReporte").text("Años: " + $("#valAniosReporte").val());
    $("#divMpiosReporte").text("Municipios: " + $("#valMpiosReporte").val());


 
}

function fnCrearTablasEdoCivilSubregistro() {
    var hCols = [];
    fnCrearTabla('EdoCivSubTabla', hCols, false);
    fnCrearTabla('EdoCivOporTabla', hCols, false);
    fnCrearTabla('EdoCivExtTabla', hCols, false);

    $('#ResReporteEdoCivOpor').appendTo('#estadoCivilRegistradosTableContainer');
    $('#ResReporteEdoCivSub').appendTo('#estadoCivilSubregistradosTableContainer');
    $('#ResReporteEdoCivExt').appendTo('#estadoCivilRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasNumNacSubregistro() {
    var hCols = [];
    fnCrearTabla('NumNacSubTabla', hCols, false);
    fnCrearTabla('NumNacOporTabla', hCols, false);
    fnCrearTabla('NumNacExtTabla', hCols, false);

    $('#ResReporteNumNacOpor').appendTo('#numeroNacidosVivosRegistradosTableContainer');
    $('#ResReporteNumNacSub').appendTo('#numeroNacidosVivosSubregistradosTableContainer');
    $('#ResReporteNumNacExt').appendTo('#numeroNacidosVivosRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}



function fnCrearTablasEscolSubregistro() {
    var hCols = [];
    fnCrearTabla('EscolSubTabla', hCols, false);
    fnCrearTabla('EscolOporTabla', hCols, false);
    fnCrearTabla('EscolExtTabla', hCols, false);

    $('#ResReporteEscolOpor').appendTo('#escolaridadMadreRegistradosTableContainer');
    $('#ResReporteEscolSub').appendTo('#escolaridadMadreSubregistradosTableContainer');
    $('#ResReporteEscolExt').appendTo('#escolaridadMadreRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

function fnCrearTablasSexoSubregistro() {
    var hCols = [];
    fnCrearTabla('SexoSubTabla', hCols, false);
    fnCrearTabla('SexoOporTabla', hCols, false);
    fnCrearTabla('SexoExtTabla', hCols, false);

    $('#ResReporteSexoOpor').appendTo('#sexoRecienNacidoRegistradosTableContainer');
    $('#ResReporteSexoSub').appendTo('#sexoRecienNacidoSubregistradosTableContainer');
    $('#ResReporteSexoExt').appendTo('#sexoRecienNacidoRegistrosExtemporaneosTableContainer');

    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}
