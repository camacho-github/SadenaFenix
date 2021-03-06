﻿/* General script for: Sadena
 */
var CONST_ROL_ANALISTA = 3;
var CONST_ROL_ADMINISTRADOR = 2;
var CONST_ROL_SUPERADMINISTRADOR = 1;

$(window).on('unload', function () {
    fnWaitForPost();
});

$(window).on('load', function () {
    fnCompleteWait();
});

/* On ready */
$(function () {

    /* Session */
    if ($("#objUsuario").val() != undefined && $("#objUsuario").val().length > 0) {
        objUsuario = JSON.parse($("#objUsuario").val());
        objUsuario.Contrasenia = "";
        $("#etiquetaSesionUsuarioDesc").text(objUsuario.UsuarioDesc);
        $("#etiquetaSesionCorreoE").text(objUsuario.CorreoE);
        /* Hide options. */
        if (objUsuario.Rol.RolId == CONST_ROL_ANALISTA) {
            $("#opcionImportarArchivos").hide();
            $("#estiloDivisionMenuNac").hide();
            $(".opcAdministradorOficinas").hide();
            $("#opcionConfigDiasExtemporaneos").hide();  
            $("#opcionesAdministracionSistema").hide();
        }
        if (objUsuario.Rol.RolId == CONST_ROL_ADMINISTRADOR) {
            $("#opcionesAdministracionSistema").hide();
        }        
        if (objUsuario.Rol.RolId == CONST_ROL_SUPERADMINISTRADOR) {
            $("#opcionesNacimientos").hide();
            $("#opcionesGeoreferenciacion").hide();
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
        window.location.href = "/Reportes/SeleccionarConsulta?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
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
        if ($("#ingresarSadena").val() != undefined) {
            window.location.href = "/Home/About";
        } else {
            window.location.href = "/Home/About?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
        }
        
    });

    $("#callContacto").click(function () {
        if ($("#ingresarSadena").val() != undefined) {
            window.location.href = "/Home/Contact";
        } else {
            window.location.href = "/Home/Contact?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
        }
    });

    $("#callConfigDiasExtemporaneos").click(function () {
        window.location.href = "/Configuraciones/DiasExtemporaneos?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callAnalisisSIC").click(function () {
        window.location.href = "/AnalisisSIC/SeleccionarConsultaSIC?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });    

    $("#callCargarUsuarios").click(function () {
        window.location.href = "/Acceso/CargaCatalogos?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callCrearUsuario").click(function () {
        window.location.href = "/Usuarios/CrearUsuario?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callConsultarUsuarios").click(function () {
        window.location.href = "/Usuarios/UsuariosConsulta?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callBitacoraUsuarios").click(function () {
        window.location.href = "/Usuarios/BitacoraUsuarios?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
    });

    $("#callSalir").click(function () {
        window.location.href = "/Acceso/Salir?userJson=" + encodeURIComponent(JSON.stringify(objUsuario));
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

function fnShowDiv(div, showHide, isClass) {
    var selector;
    if (isClass !== undefined && isClass === true) {
        selector = $("." + div);
    } else {
        selector = $("#" + div);
    }

    if (showHide === 1)
        selector.toggleClass('hiddElement', false);
    if (showHide === 0)
        selector.toggleClass('hiddElement', true);
}

function fnWaitForPost() {
    fnShowDiv("loader", 1);
}

function fnCompleteWait() {
   fnShowDiv("loader", 0);
}

function fnWaitForLoading(func) {
    var loader = $("#loader");
    fnShowDiv(loader.attr('id'), 1);
    loader.fadeOut(100, function () {
        func();
        loader.removeAttr('style');
        fnShowDiv(loader.attr('id'), 0);
    });
}

function fnWaitForLoading2(func) {
    var loader = $("#loader2");
    fnShowDiv(loader.attr('id'), 1);
    loader.fadeOut(500, function () {
        fnShowDiv("loader", 0);
        func();
        loader.removeAttr('style');
        fnShowDiv(loader.attr('id'), 0);
    });
}

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