﻿/* Script for: Consultar
 */
var objUsuario;
var CONST_ROL_ANALISTA = 3;

$(function () {

    if ($("#objUsuario").val() != undefined && $("#objUsuario").val().length > 0) {
        objUsuario = JSON.parse($("#objUsuario").val());
        $("#etiquetaSesionUsuarioDesc").text(objUsuario.UsuarioDesc);
        $("#etiquetaSesionCorreoE").text(objUsuario.CorreoE);

        if (objUsuario.Rol.RolId == CONST_ROL_ANALISTA) {
            $("#opcionImportarArchivos").hide();
            $("#estiloDivisionMenu").hide();
        }
    } else {
        $("#botonCirculoSesion").hide();
        $("#menuGeneral").hide();        
    }
    
    //Initialize Select2 Elements
    $('.select2').select2({
        tags: "true",
        placeholder: "Selección...",
        allowClear: true
    });

    /* Unregistered table */
    $('#noRegistradosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Registered table */
    $('#registradosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Subregistered table */
    $('#extemporaneosTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Summary table */
    var summaryData = [
        ['008', 'Escobedo', '13', '12', '0'],
        ['009', 'Francisco I.Madero', '23', '24', '1'],
        ['010', 'Frontera', '2', '1', '0'],
        ['011', 'General Cepeda', '9', '18', '35'],
        ['012', 'Guerrero', '22', '16', '3']
    ];
    $('#resumenTable').DataTable({
        'data': summaryData,
        'scrollX': true,
        'searching': false,
        'ordering': false,
        'paging': false,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    // Initialize components
    $('.treeview').blur(function () {
        $(this).removeClass('active');
    });

    $('.treeview').click(function () {
        $(this).addClass('active');
    });

    $('#loadBtn').click(function () {
        $('#infoModal').show();
    });

    $('#consultarDatosProcesadosBtn').click(function () {
        $('#searchingResult').show();
    });

});