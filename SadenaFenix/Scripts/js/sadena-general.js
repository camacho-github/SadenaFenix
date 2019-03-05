/* General script for: Sadena
 */
var CONST_ROL_ANALISTA = 3;

/* On ready */
$(function () {

    /* Session */
    if ($("#objUsuario").val() != undefined && $("#objUsuario").val().length > 0) {
        objUsuario = JSON.parse($("#objUsuario").val());
        $("#etiquetaSesionUsuarioDesc").text(objUsuario.UsuarioDesc);
        $("#etiquetaSesionCorreoE").text(objUsuario.CorreoE);
        /* Hide options. */
        if (objUsuario.Rol.RolId == CONST_ROL_ANALISTA) {
            $("#opcionImportarArchivos").hide();
            $("#estiloDivisionMenu").hide();
            $("#opcAdministradorOficinas").hide();
        }
    } else {
        $("#botonCirculoSesion").hide();
        $("#menuGeneral").hide();
    }

    /* Menu options */
    $("#callImportarArchivo").click(function () {
        window.location.href = "/Archivos/SeleccionarArchivos?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callReportes").click(function () {
        window.location.href = "/Reportes/Reportes?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callConsultar").click(function () {
        window.location.href = "/SubRegistro/SeleccionarConsulta?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $(".callConsultaOficinas").click(function () {
        window.location.href = "/Oficinas/OficinasConsulta?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callCrearOficina").click(function () {
        window.location.href = "/Oficinas/CrearOficina?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callActualizarOficina").click(function () {
        window.location.href = "/Oficinas/ActualizarOficina?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callAcercaDe").click(function () {
        window.location.href = "/Home/About?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callContacto").click(function () {
        window.location.href = "/Home/Contact?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });


    /* Loaders 
    $(document).ajaxStart(function () {

        $("body").addClass("modal");
        $('.modal').fadeIn(500);

    });
    $(document).ajaxStop(function () {

        $("body").removeClass("modal");
        $('.modal').fadeOut(500);

    });	*/

});

/* Showing loading 
function charginModal() {
    $('body').loadingModal('destroy');
    $('body').loadingModal({
        text: 'Cargando...',
        animation: 'wave'
    });
}
function modalCargaDinamico(selector) {
    $(selector).loadingModal('destroy');
    $(selector).loadingModal({
        text: 'Cargando...',
        animation: 'wave'
    });
}
function afterSend() {
    $('body').loadingModal('hide');
}
function modalCierraDinamico(selector) {
    $(selector).loadingModal('hide');
}

*/