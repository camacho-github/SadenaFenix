$(function () {
    //Only needed for the filename of export files.
    //Normally set in the title tag of your page.document.title = 'Simple DataTable';
    //Define hidden columns
    var hCols = [3, 4];
    // DataTable initialisation
    $('#OficialiasTabla').DataTable({
        "bFilter": false,
        "dom": "<'row'<'col-sm-4'B><'col-sm-2'l><'col-sm-6'p<br/>i>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-12'p<br/>i>>",
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
                var totCols = $('#OficialiasTabla thead th').length;
                var hiddenCols = hCols.length;
                var shownCols = totCols - hiddenCols;
                return 'Columnas (' + shownCols + ' de ' + totCols + ')';
            },
            prefixButtons: [{
                extend: 'colvisGroup',
                text: 'Mostrar todo',
                show: ':hidden'
            }, {
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
            "searchPlaceholder": "Search records",
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
            $('#OficialiasTabla').on('column-visibility.dt', function (e, settings, column, state) {
                var visCols = $('#OficialiasTabla thead tr:first th').length;
                //Below: The minus 2 because of the 2 extra buttons Show all and Restore
                var tblCols = $('.dt-button-collection li[aria-controls=OficialiasTabla] a').length - 2;
                $('.buttons-colvis[aria-controls=OficialiasTabla] span').html('Columnas (' + visCols + ' de ' + tblCols + ')');
                e.stopPropagation();
            });
        }
    });
});