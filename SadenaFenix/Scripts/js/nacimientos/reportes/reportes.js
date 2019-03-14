﻿/* Script for: Reportes
 */
var objReporteEdadSubregistro;
var mapMpios = {};

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
        //color: '#f4f3f0',
        enableZoom: true,
        hoverColor: '#c9dfaf',
        hoverOpacity: null,
        normalizeFunction: 'linear',
        //scaleColors: ['#b6d6ff', '#005ace'],
        selectedColor: '#586967',
        selectedRegion: true,
        showTooltip: true,
        regionsSelectable: true,
        labels: objlabels,
        markerStyle: {
            initial: {
                fill: '#4DAC26'
            },
            selected: {
                fill: '#586967'
            }
        },
        regionStyle: {
            initial: {
                //fill: '#92B5B1',
                "stroke-width": "0.5",
                stroke: "black"
            },
            selected: {
                fill: '#586967',
                "stroke-width": "0.5",
                stroke: "red"
            }        
        },
        onRegionClick: function (event, code) {
            
            if (objMpiosMapas[code] == undefined) {
                //mapVector.regions[code].element.setStyle({
                //    'fill': "#ffffff"
                //});
                event.preventDefault();
               
            }
        },
        onRegionSelected: function (event, code) {
            //alert("has seleccionado el municipio " + code + " que corresponde a " + objMap[code].name);


            //if (fnSizeObject(mapMpios) == 0) {
            //    $("tbody tr").toggleClass("hiddElement", true);
            //    $("tr." + parseInt(code)).toggleClass("hiddElement",false);
            //    mapMpios[parseInt(code)] = parseInt(code);
            //} else {
                if (mapMpios[code] == undefined) {
                    mapMpios[code] = code;
                    $("tr." + code).toggleClass("hiddElement", false);
                } else {
                    delete mapMpios[code];
                    $("tr." + parseInt(code)).toggleClass("hiddElement", true);
                    if (fnSizeObject(mapMpios) == 0) {
                        $("tbody tr").toggleClass("hiddElement", true);
                    }
                }
            //}            
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

function fnSizeObject(obj) {
    var count = 0;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) {
            count++;
        }
    }

    return count;
}