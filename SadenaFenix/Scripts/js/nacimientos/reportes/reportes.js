/* Script for: Reportes
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
        enableZoom: true,
        hoverColor: '#c9dfaf',
        hoverOpacity: null,
        normalizeFunction: 'linear',
        //scaleColors: ['#b6d6ff', '#005ace'],
        selectedColor: '#586967',
        selectedRegion: true,
        showTooltip: true,
        regionsSelectable: true,
        labels:
        {
            regions:
            {
                render: function (code) {
                    return objMap[code].label;
                },
                offsets: function (code) {
                    switch (parseInt(code)) {
                        case 34:
                            return [-10, 10];
                            break;
                        case 23:
                            return [-10, 10];
                            break;
                        case 2:
                            return [-10, 0];
                            break;
                        case 38:
                            return [-5, 0];
                            break;
                        case 20:
                            return [5, 5];
                            break;
                        case 14:
                            return [5, 0];
                            break;
                        case 31:
                            return [5, 10];
                            break;
                        case 19:
                            return [-3, 0];
                            break;
                        case 7:
                            return [0, -3];
                            break;
                        case 9:
                            return [0, -10];
                            break;
                        case 35:
                            return [-7, -1];
                            break;
                        case 33:
                            return [-5, 0];
                            break;
                        case 4:
                            return [-2, -2];
                            break;
                        case 18:
                            return [0, -2];
                            break;
                        case 16:
                            return [0, 1];
                            break;
                        case 21:
                            return [0, -2];
                            break;
                        case 10:
                            return [1, -1];
                            break;
                        case 13:
                            return [2, 0];
                            break;
                        case 8:
                            return [-2, -1];
                            break;
                        case 28:
                            return [2, 0];
                            break;
                        default:
                        // code block
                    }


                    if (parseInt(code) == 34) {
                        return [-10, -10]
                    }
                }
            }
        },
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
                //fill: '#586967',
                "stroke-width": "0.5",
                stroke: "red"
            }        
        },
        regionLabelStyle: {
            initial: {
                fill: '#B90E32',
                'font-size': '10',
            },
            hover: {
                fill: 'black',
                'font-size': '14',
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
        }, onRegionTipShow: function (e, el, code) {

            if (objMpiosMapas[code] != undefined) {
                $(el).append($("<br/>"));
                $(el).append($("<br/>"));
                $(el).append($("<span/>", {
                    'html': ' Subregistro:(' + objMpiosMapas[code].TotalSubregistro + ')'
                }));
            }
        }

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

function saveImage() {
    var oSerializer = new XMLSerializer();
    var sXML = oSerializer.serializeToString(document.querySelector("#coahuilaMap svg"));

    var canvas = document.createElement('canvas');
    var context = canvas.getContext('2d');

    canvg(document.getElementById('canvas'), sXML, { ignoreMouse: true, ignoreAnimation: true })
    var imgData = canvas.toDataURL("image/png");
    window.location = imgData.replace("image/png", "image/octet-stream");

}

function fnSizeObject(obj) {
    var count = 0;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) {
            count++;
        }
    }

    return count;
}