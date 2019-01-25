// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    /* Unregistered table */
    $('#unregisteredTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Registered table */
    $('#registeredTable').DataTable({
        'scrollX': true,
        'searching': true,
        'ordering': true,
        'paging': true,
        'lengthChange': false,
        'info': false,
        'autoWidth': true
    });

    /* Subregistered table */
    $('#subregisteredTable').DataTable({
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
    $('#summaryTable').DataTable({
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

    $('#searchBtn').click(function () {
        $('#searchingResult').show();
    });



    $('#loadBtn').click(function () {
        $('#infoModal').show();
    });

});