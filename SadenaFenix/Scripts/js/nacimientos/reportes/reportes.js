/* Script for: Reportes
 */
var objReporteEdadSubregistro;

/* On ready */
$(function () {

    /* Initialize Select2 */
    $('.select2').select2({
        tags: "true",
        placeholder: "Selección...",
        allowClear: true
    });

    $('#coahuilaMap').vectorMap({
        map: 'CoahuilaDeZaragozaMap',
        backgroundColor: '#F6F2F2',
        borderColor: '#818181',
        borderOpacity: 0.25,
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
                fill: '#92B5B1',
                "stroke-width": "0.5",
                stroke: "black"
            },
            selected: {
                fill: '#F4A582',
                "stroke-width": "0.5",
                stroke: "red"
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

    $('#ConsultarReporteEdadSubregistro').click(function () {
        //provisionalmente para evitar que se consulte varias veces
        if (objReporteEdadSubregistro == undefined) {

            $.ajax({
                type: "POST",
                url: "ConsultarReporteEdadSubregistro",
                data: JSON.stringify(objUsuario),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    if (objReporteEdadSubregistro == undefined) {
                        objReporteEdadSubregistro = {
                            cabeceros: data.ColCabeceros,
                            filas: data.ColFilas
                        };
                    }
                },
                error: function (request, status, error) {
                    alert(request);
                }
            });

        }
    });

});