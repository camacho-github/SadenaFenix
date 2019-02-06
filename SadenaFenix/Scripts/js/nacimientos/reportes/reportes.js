/* Script for: Reportes
 */
$(function () {

    $('#coahuilaMap').vectorMap({
        map: 'CoahuilaDeZaragozaMap',
        backgroundColor: '#a5bfdd',
        borderColor: 'black',
        //borderOpacity: 0.5,
        borderWidth: 2,
        color: '#f4f3f0',
        enableZoom: true,
        hoverColor: '#c9dfaf',
        hoverOpacity: null,
        normalizeFunction: 'linear',
        scaleColors: ['#b6d6ff', '#005ace'],
        selectedColor: '#c9dfaf',
        selectedRegion: true,
        showTooltip: true,
        regionsSelectable: true,
        markerStyle: {
            initial: {
                fill: '#4DAC26'
            },
            selected: {
                fill: '#CA0020'
            }
        },
        regionStyle: {
            initial: {
                fill: '#B8E186'
            },
            selected: {
                fill: '#F4A582'
            }
        }, onRegionSelected: function (event, code) {
            alert("has seleccionado el municipio " + code + " que corresponde a " + objMap[code].name);
        },
    });
   

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