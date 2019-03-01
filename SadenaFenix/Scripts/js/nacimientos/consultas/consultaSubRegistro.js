
$(function () {
       

    $("#consultarDatosProcesadosBtn").click(function () {        
        fnConsultarDatos();
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
    var hCols = [2, 3, 5, 7, 9, 17, 19];
    fnCrearTabla('SubRegistroTabla', hCols);
    fnCrearTabla('OportunosTabla', hCols)
    fnCrearTabla('ExtemporaneosTabla', hCols)

    hCols = [1];
    fnCrearTabla('ReporteMpiosTabla', hCols)
    fnShowDiv("modalConsulta", 0);
    fnShowDiv("loader", 0);
}

