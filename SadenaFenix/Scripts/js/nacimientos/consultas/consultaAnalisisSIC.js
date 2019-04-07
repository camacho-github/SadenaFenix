
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
    
    fnGetAndSetTemplate('/AnalisisSIC/AnalisisSICInformacion', params,
        "tablaContenedor", fnCrearTablasAnalisisSIC);    
}

function fnCrearTablasAnalisisSIC() {
    var hCols = [];
    fnCrearTabla('relacionFolioTabla', hCols);
    fnCrearTabla('relacionFechaTabla', hCols)
    fnCrearTabla('duplicadosTabla', hCols)
    fnCrearTabla('sinSinacTabla', hCols)

    $("#relacionFolioLink").click();
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

