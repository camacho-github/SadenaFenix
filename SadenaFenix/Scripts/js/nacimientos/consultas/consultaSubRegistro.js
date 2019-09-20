
$(function () {
       

    $("#consultarDatosProcesadosBtn").click(function () {        
        fnConsultarDatos();
    });

    $("#tablaContenedor").on("click", ".description-block", function () {
        if (!$(this).hasClass("active")) {
            $(".description-block").removeClass("active");
            $(this).addClass("active");
            var link = $(this).attr("aKey");
            $("#" + link).click();
            
        }        
        //$(this).find(".description-block-review-link").click();
        
        //$(this).find(a).trigger("click");
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
    
    fnGetAndSetTemplate('/SubRegistro/SubRegistroInformacion', params,
        "tablaContenedor", fnCrearTablasSubregistro);    
}

function fnCrearTablasSubregistro() {
    var hCols = [2, 3, 8, 10, 12, 20, 22];
    fnCrearTabla('SubRegistroTabla', hCols);
    hCols = [3, 4, 9, 11, 13, 21, 23];
    fnCrearTabla('OportunosTabla', hCols)
    fnCrearTabla('ExtemporaneosTabla', hCols)
    hCols = [2,3, 4, 9, 11, 13, 21, 23];
    fnCrearTabla('DuplicadosTabla', hCols)

    hCols = [0];
    fnCrearTabla('ReporteMpiosTabla', hCols)
    $("#resumenTotalesLink").click();
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

