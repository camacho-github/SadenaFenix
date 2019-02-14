var objOficialia = {
    OId: 0
};

$(function () {
    //Only needed for the filename of export files.
    //Normally set in the title tag of your page.document.title = 'Simple DataTable';
    //Define hidden columns
    var hCols = [7,9,10,11,12,13,14];
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


    
    // Enable Live Search.  
    var $selectMpio = $('#MunicipioLista');
    $selectMpio.attr('data-live-search', true);

    $selectMpio.selectpicker(
        {
            width: '100%',
            title: '- [selecciona Municipio] -',
            limit: 1,
            size: 6
        });

    $selectMpio.on('change', function () {
        var selected = $('#MunicipioLista option:selected').val();
        $("#LocalidadLista").children("optgroup[label='" + selected + "']").removeAttr('disabled');

        var arrNotSelected = $('#MunicipioLista option:not(:selected)');
        for (var i = 0; i < arrNotSelected.length; i++) {
            $("#LocalidadLista").children("optgroup[label='" + arrNotSelected[i].value + "']").prop("disabled", "disabled");
        }

        objOficialia.MpioId = selected;
        fnLoadLocalidadLista();
        $("#LocalidadLista").focus();
        ///////////////////////
    }); 

    $('#LocalidadLista').on('change', function () {
        var selected = $('#LocalidadLista option:selected').val();
        objOficialia.LocId = selected;
    });

    fnLoadLocalidadLista();
    $('#LocalidadLista').prop('disabled', true);

    $(".ValidacionTexto").on('keypress', function (event) {
        var regExp = /^[a-zA-z0-9 ]{0,245}$/;
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        var text = $(this).val();

        if (!regExp.test(key) || !regExp.test(text)) {
            event.preventDefault();
            return false;
        }
    });


    $("input.ValidacionTelefono").focus(function () {
        $(this).trigger("click");
    });

    $("input.ValidacionTelefono").click(function () {
        if (!$(this).hasClass("disable")) {
            var input = $(this);
            input.val(input.attr("realValue"));
            fnMoveCursorToEnd(input);
        }
    });

    $("input.ValidacionTelefono").on('blur', function (event) {        
        var input = $(this),
            number = input.val();
        input.attr("realValue", input.val());
        fnSetFormatPhoneNumber(input, number);
        input.val($.trim(input.val()));
    });	


    $("input.ValidacionNumero").on('keydown', function (event) {
        var okVal = funOnlyNumberExtended(event);
        return okVal;
    });	

    $('#BotonCrearOficialia').click(function () {
        if (fnValidarCrearOficialia() == true) {
            $('#BotonCrearOficialia').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
            fnGuardarOficialia();
        }
    });
       
});

function fnLoadLocalidadLista() {
    var $selectLoc = $('#LocalidadLista');

    $selectLoc.prop('disabled', false);
    $selectLoc.attr('data-live-search', true);

    $selectLoc.selectpicker(
        {
            width: '100%',
            title: '- [selecciona Localidad] -',
            limit: 1,
            size: 6,
            hideDisabled: true
        });

    $selectLoc.selectpicker('render');

    $selectLoc.selectpicker('refresh');
      
}

function fnSetFormatPhoneNumber(element, number) {
    try {
        if (number !== "" && number.length > 0) {
            var tmp1 = number.substring(0, 3),
                tmp2 = number.substring(3, 6),
                tmp3 = number.substring(6);

            number = "(" + tmp1 + ")" + tmp2 + " - " + tmp3;
        }
        $(element).val(number);
    } catch (e) {
        console.error("Error at setFormat phone number cause: " + e);
    }

}

function fnMoveCursorToEnd(element) {
    try {
        var target = event.currentTarget || event.srcElement || event.target;
        var position = $(element).val().length;
        fnMoveCursor(target, position);
    } catch (e) {
        console.info("No target to move end" + e);
    }

}

function fnMoveCursor(o, newPosition) {
    if (o.createTextRange) {
        var r = o.createTextRange();
        var value = o.value;
        var moveTo = value.length - newPosition;
        r.moveStart('character', newPosition);
        r.moveEnd('character', -moveTo);
        r.select();
    } else {
        o.selectionStart = newPosition;
        o.selectionEnd = newPosition;
    }
}

function funOnlyNumberExtended(event) {
    var whichCode = (window.Event) ? event.which : event.keyCode;
    if (whichCode == 13) {
        return true;
    }
    if (whichCode == 8 || whichCode == 0) {
        return true;
    }
    if ((whichCode >= 48 && whichCode <= 57)
        || (whichCode >= 96 && whichCode <= 105)) {
        return true;
    } else {
        return false;
    }

}

function fnValidarCrearOficialia() {
    var valid = true;

    if ($('#OficialiaId').val().length < 0) {
        $('#OficialiaId').parent().children('span').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#OficialiaId').parent().children('span').text("");
    }

    if ($('#Calle').val() == '') {
        $('#Calle').parent().children('span').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#Calle').parent().children('span').text("");
    }

    if ($('#Numero').val() == '') {
        $('#Numero').parent().children('span').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#Numero').parent().children('span').text("");
    }


    if ($('#Colonia').val() == '') {
        $('#Colonia').parent().children('span').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#Colonia').parent().children('span').text("");
    }


    if ($('#CP').val() == '' && $('#CP').val().length < 5) {
        $('#CP').parent().children('span').text("El valor es requerido para continuar, ingresa un código postal válido");
        valid = false;
    } else {
        $('#CP').parent().children('span').text("");
    }


    if ($('#Latitud').val() == '' && $('#Latitud').val().length < 8) {
        $('#Latitud').parent().children('span').text("El valor es requerido para continuar, ingresa una  ubicación en el mapa");
        valid = false;
    } else {
        $('#Latitud').parent().children('span').text("");
    }

    if ($('#Longitud').val() == '' && $('#Longitud').val().length < 8) {
        $('#Longitud').parent().children('span').text("El valor es requerido para continuar, ingresa una  ubicación en el mapa");
        valid = false;
    } else {
        $('#Longitud').parent().children('span').text("");
    }
    

    if (valid == false) {
        fnMessage("Validación de información", "Los datos ingresados son incorrectos");
        return false;
    } 

    return valid;
}

function fnObtenerJsonOficialia() {

    objOficialia.OficialiaId = $('#OficialiaId').val();
    objOficialia.Calle = $('#Calle').val();
    objOficialia.Numero = $('#Numero').val();
    objOficialia.Colonia = $('#Colonia').val();
    objOficialia.CP = $('#CP').val();
    objOficialia.Latitud = $('#Latitud').val();
    objOficialia.Longitud = $('#Longitud').val();
    objOficialia.Telefono = $('#Telefono').val();
    objOficialia.Nombres = $('#Nombres').val();
    objOficialia.Apellidos = $('#Apellidos').val();
    objOficialia.CorreoE = $('#CorreoE').val();
    objOficialia.Observaciones = $('#Observaciones').val();
    objOficialia.Telefono = $('#Telefono').val();

    return JSON.stringify(objOficialia);
}

function fnGuardarOficialia() {
    var objArray = {
        "jsonOficialia": fnObtenerJsonOficialia()
    },
        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('GuardarOficialia', params);

        if (data !== "" && data !== null) {
            if (data.respuesta !== null) {
                fnMessage("Operación correcta", "La información fue exitosamente almacenada", fnIrConsulta);
            } else{
                fnMessage("UPS! =(", "La información no fue guardada, favor de intentar nuevamente");
            }
        }
    };

    fnWaitForLoading(fnComplete);

}

function fnIrConsulta() {
    fnWaitForPost();
    window.location.assign("OficialiasTabla");
}



