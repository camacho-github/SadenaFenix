var objMapaConfiguracion = undefined;

$(function () {
    var hCols = [1, 3, 5, 6, 7, 8, 9, 15, 16, 17, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39];
    $('#OficinasTabla').DataTable({
        "bFilter": false,
        "dom": 'Blfrtip',
        "paging": true,
        "searching": true,
        "autoWidth": true,
        "columnDefs": [{
            "visible": false,
            "targets": hCols
        }],
        "buttons": [{
            extend: 'colvis',
            collectionLayout: 'three-column',
            text: function () {
                var totCols = $('#OficinasTabla thead th').length;
                var hiddenCols = hCols.length;
                var shownCols = totCols - hiddenCols;
                return 'Columnas (' + shownCols + ' de ' + totCols + ')';
            },
            prefixButtons: [{
                extend: 'colvisGroup',
                text: 'Mostrar todo',
                show: ':hidden'
            },
            {
                extend: 'colvisGroup',
                text: 'Ocultar todo',
                hide: ':visible'
            },
            {
                extend: 'colvisRestore',
                text: 'Restaurar'
            }]
        }, {
            text: 'Imprimir',
            extend: 'print',
            footer: false,
            exportOptions: {
                columns: ':visible'
            }
        },
        {
            extend: 'collection',
            text: 'Exportar',
            buttons: [{
                text: 'Excel',
                extend: 'excelHtml5',
                footer: false,
                exportOptions: {
                    columns: ':visible'
                }
            }, {
                text: 'CSV',
                extend: 'csvHtml5',
                fieldSeparator: ';',
                exportOptions: {
                    columns: ':visible'
                }
            }, {
                text: 'PDF Vertical',
                extend: 'pdfHtml5',
                message: '',
                exportOptions: {
                    columns: ':visible'
                }
            }, {
                text: 'PDF Horizontal',
                extend: 'pdfHtml5',
                message: '',
                orientation: 'landscape',
                exportOptions: {
                    columns: ':visible'
                }
            }]
        }]
        , language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "searchPlaceholder": "Buscar coincidencias",
            "search": "",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
        , "initComplete": function (settings, json) {
            // Adjust hidden columns counter text in button -->
            $('#OficinasTabla').on('column-visibility.dt', function (e, settings, column, state) {
                var visCols = $('#OficinasTabla thead tr:first th').length;
                //Below: The minus 2 because of the 2 extra buttons Show all and Restore
                var tblCols = $('.dt-button-collection li[aria-controls=OficinasTabla] a').length - 3;
                $('.buttons-colvis[aria-controls=OficinasTabla] span').html('Columnas (' + visCols + ' de ' + tblCols + ')');
                e.stopPropagation();
            });
        }

    });
    $('.dataTables_length').addClass('bs-select');


    $('.eliminarAction').click(function () {
        var oid = $(this).attr('oid');        
        fnAdvertenciaEliminarOficina(oid);
    }); 
   
});

function fnAdvertenciaEliminarOficina(oid) {
    var buttonsList = {},
        msgQuestion = "El registro seleccionado será eliminado permanentemente, ¿desea continuar?";

    buttonsList["SI"] = function () {
        fnWaitForPost();
        fnEliminarOficina(oid);
    };
    buttonsList["No"] = function () {
        fnShowDiv("ConfirmacionMensaje", 0);
    };
    fnMensajeBotonesLista("Confirmación", msgQuestion, buttonsList);
}

function fnEliminarOficina(oid) {
    var objArray = {
        "oId": oid
    }

    params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('EliminarOficina', params);

        if (data !== "" && data !== null) {
            if (data.respuesta !== null) {
                fnMessage("Operación correcta", "La información fue exitosamente eliminada", fnIrConsulta);
            } else {
                fnMessage("UPS! =(", "La información no fue eliminada, favor de intentar nuevamente");
            }
        }
    };

    fnWaitForLoading(fnComplete);

}

function fnIrConsulta() {
    fnWaitForPost();
    $("#callConsultaOficinas").click();
}