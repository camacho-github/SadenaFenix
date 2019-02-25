var objOficina = {
    OId: 0,
    TipoId: 0,
    MpioId: 0,
    LocId: 0
};

var objMapaConfiguracion;
if ($('#Longitud').val() != '' && $('#Latitud').val() != '') {
    objMapaConfiguracion = {
        lat: parseFloat($('#Latitud').val()),
        lng: parseFloat($('#Longitud').val()),
        zoom: 17,
        disableDoubleClickZoom: true,
        direccionMarca: $("input#MpioDesc").val() + " " + $("input#LocDesc").val()
    }
}

$(function () {

    var $selectTipo = $('#TipoLista');
    $selectTipo.attr('data-live-search', true);
    $selectTipo.selectpicker(
    {
        width: '100%',
        title: '- [selecciona el tipo de Oficina] -',
        limit: 1,
        size: 6
    });


    $selectTipo.on('change', function () {
        var nombreTexto = $(this).find("option:selected").text();
        var selected = $('#TipoLista option:selected').val();
        objOficina.TipoId = selected;   

        if ($("input#TipoDesc").exists()) {
            $("input#TipoDesc").val(nombreTexto);

            $('div.selectTipo').toggleClass("hiddElement", "true");
            $("input#TipoDesc").toggleClass("hiddElement", "false");
        }    
    });

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
        var nombreTexto = $(this).find("option:selected").text();
        fnCargaMapaCoincidencias(nombreTexto + ", Coahuila de Zaragoza, México");

        if ($("input#MpioDesc").exists()) {
            $("input#MpioDesc").val(nombreTexto);

            $('div.selectMpio').toggleClass("hiddElement", "true");
            $("input#MpioDesc").toggleClass("hiddElement", "false");

            $("input#LocDesc").val("");
        }

        var selected = $('#MunicipioLista option:selected').val();
        $("#LocalidadLista").children("optgroup[label='" + selected + "']").removeAttr('disabled');

        var arrNotSelected = $('#MunicipioLista option:not(:selected)');
        for (var i = 0; i < arrNotSelected.length; i++) {
            $("#LocalidadLista").children("optgroup[label='" + arrNotSelected[i].value + "']").prop("disabled", "disabled");
        }

        objOficina.MpioId = selected;
        fnLoadLocalidadLista();
        $("#LocalidadLista").focus();

        ///////////////////////
    });

    $('#LocalidadLista').on('change', function () {
        var nombreTexto = $(this).find("option:selected").text();
        var selected = $('#LocalidadLista option:selected').val();
        objOficina.LocId = selected;

        if ($("input#LocDesc").exists()) {
            $("input#LocDesc").val(nombreTexto);

            $('div.selectLoc').toggleClass("hiddElement", "true");
            $("input#LocDesc").toggleClass("hiddElement", "false");
        }
    });

    if (!$("input#LocDesc").exists()) {
        fnLoadLocalidadLista();
        $('#LocalidadLista').prop('disabled', true);
    }

    $('#BotonCrearOficina').click(function () {
        if (fnValidarGuardarOficina() == true) {
            $('#BotonCrearOficina').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
            fnGuardarOficina();
        }
    });

    $('#BotonActualizarOficina').click(function () {
        if (fnValidarGuardarOficina() == true) {
            $('#BotonActualizarOficina').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
            fnGuardarOficina();
        }
    });

    $('#BotonEliminarOficina').click(function () {
        
        $('#BotonEliminarOficina').attr('disabled', 'disabled').attr('class', 'buttonContinueInactive');//disable the button for a while the ajax is running			
        fnEliminarOficina();
       
    });    

    $("input#MpioDesc").on('focus', function (event) {
        $(this).toggleClass("hiddElement", "true");
        $('div.selectMpio').toggleClass("hiddElement", "false");

        $("#MunicipioLista").focus();
    });

    $("input#LocDesc").on('focus', function (event) {
        $(this).toggleClass("hiddElement", "true");
        $('div.selectLoc').toggleClass("hiddElement", "false");

        $("#LocalidadLista").focus();
    }); 

    $('.toggleChecks').change(function () {
        var valor = $(this).prop('checked');
        var id = $(this).prop('id');
        if (valor == "true" || valor == true) {
            objOficina[id] = 1;
        } else {
            objOficina[id] = 0;
        }
    })
        
    fnIniciarBotonesBinarios();
});


function fnIniciarBotonesBinarios(){
    $('.toggleChecks').bootstrapToggle();

    $('.toggleChecks').prop('checked', false).change();
}

function fnLoadLocalidadLista() {
    var $selectLoc = $('#LocalidadLista');

    if ($("input#LocDesc").exists()) {
        $("input#LocDesc").toggleClass("hiddElement", "true");
        $('div.selectLoc').toggleClass("hiddElement", "false");
        $selectLoc.toggleClass("hiddElement", "false");
    }    

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

    if ($("input#LocDesc").exists()) {
        $("input#LocDesc").toggleClass("hiddElement", "true");
        $('div.selectLoc').toggleClass("hiddElement", "false");
        $selectLoc.toggleClass("hiddElement", "false");
    }
      
}

function fnValidarGuardarOficina() {
    var valid = true;
    var advertenciaVacios = false;

    if (objOficina.TipoId <= 0) {
        $('#valMessageTipo').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageTipo').text("");
    }

    if (parseInt($('#OficinaId').val()) <= 0) {
        $('#OficinaId').parent().children('span').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#OficinaId').parent().children('span').text("");
    }

    if ($('#TipoInstitucion').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#Institucion').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#Latitud').val() == '' && $('#Latitud').val().length < 8) {
        advertenciaVacios = true;
    }

    if ($('#Latitud').val() == '' && $('#Latitud').val().length < 8) {
        advertenciaVacios = true;
    }

    if ($('#Region').val() == '') {
        advertenciaVacios = true;
    }

    if ($("input#MpioDesc").exists()) {
        objOficina.MpioId = $("input#MpioDesc").attr("identificador");
    }

    if ($("input#LocDesc").exists()) {
        objOficina.LocId = $("input#LocDesc").attr("identificador");
    }

    if (objOficina.MpioId <= 0) {
        $('#valMessageMpio').text("El valor es requerido para continuar");
        valid = false;
    } else {
        $('#valMessageMpio').text("");
    }
    
    if ($('#Calle').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#Numero').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#Colonia').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#CP').val() == '' && $('#CP').val().length < 5) {
        advertenciaVacios = true;
    }

    if ($('#EntreCalles').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#HorarioAtencion').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#OficialNombre').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#OficialApellidos').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#Telefono').val() == '') {
        advertenciaVacios = true;
    }

    if ($('#CorreoE').val() == '') {
        advertenciaVacios = true;
    }

    if (valid == false) {
        fnMessage("Validación de información", "Los datos ingresados son incorrectos", undefined, "alert-warning");
        return false;
    } else if(advertenciaVacios == true){
        fnAdvertenciaVacios();
        return false;
    }

    return valid;
}

function fnAdvertenciaVacios() {
    var buttonsList = {},
        msgQuestion = "Algunos datos no fueron capturados, ¿desea continuar con el guardado sin estos valores?";

    buttonsList["SI"] = function () {
        fnWaitForPost();      
        if (objOficina.OId == 0) {
            fnGuardarOficina();
        } else {
            fnActualizarOficina();
        }         
     };
    buttonsList["No"] = function () {
        fnShowDiv("ConfirmacionMensaje", 0);
    };
    fnMensajeBotonesLista("Confirmación", msgQuestion, buttonsList);
}

function fnObtenerJsonOficina() {

        if ($('#OId').exists() && $('#OId').val() > 0) {
            objOficina.OId = $('#OId').val();
        }
    
        objOficina.OficinaId = $('#OficinaId').val();
        objOficina.TipoInstitucion = $('#TipoInstitucion').val();
        objOficina.Institucion = $('#Institucion').val();
        objOficina.Latitud = $('#Latitud').val();
        objOficina.Longitud = $('#Longitud').val(); 
        objOficina.Region = $('#Region').val();
        objOficina.Calle = $('#Calle').val();
        objOficina.Numero = $('#Numero').val();
        objOficina.Colonia = $('#Colonia').val();
        objOficina.CP = $('#CP').val();
        objOficina.EntreCalles = $('#EntreCalles').val();
        objOficina.HorarioAtencion = $('#HorarioAtencion').val();
        objOficina.OficialNombre = $('#OficialNombre').val();
        objOficina.OficialApellidos = $('#OficialApellidos').val();
        objOficina.Telefono = $('#Telefono').val();
        objOficina.CorreoE = $('#CorreoE').val();
        objOficina.InvEscritorios = $('#InvEscritorios').val();
        objOficina.InvSillas = $('#InvSillas').val();
        objOficina.InvArchiveros = $('#InvArchiveros').val();
        objOficina.InvCompPriv = $('#InvCompPriv').val();
        objOficina.InvCompGob = $('#InvCompGob').val();
        objOficina.InvEscanPriv = $('#InvEscanPriv').val();
        objOficina.InvEscanGob = $('#InvEscanGob').val();
        objOficina.InvImpPriv = $('#InvImpPriv').val();
        objOficina.InvImpGob = $('#InvImpGob').val();

    return JSON.stringify(objOficina);
}

function fnGuardarOficina() {
    var objArray = {
        "jsonOficina": fnObtenerJsonOficina()
    },
        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('GuardarOficina', params);

        if (data !== "" && data !== null) {
            
            if (data.respuesta !== null) {
                fnMessage("Operación correcta", "La información fue exitósamente almacenada", fnIrConsulta);
            } else{
                fnMessage("UPS! =(", "La información no fue guardada, favor de intentar nuevamente");
            }
        }
    };

    fnWaitForLoading(fnComplete);
}

function fnEliminarOficina () {
    var objArray = {
        "oId": $('#OId').val()
    }

    params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('../EliminarOficina', params);

        if (data !== "" && data !== null) {
            if (data.respuesta !== null) {
                fnMessage("Operación correcta", "La información fue exitósamente eliminada", fnIrConsultaDeActualizacion);
            } else {
                fnMessage("UPS! =(", "La información no fue eliminada, favor de intentar nuevamente");
            }
        }
    };

    fnWaitForLoading(fnComplete);

}

function fnActualizarOficina() {
    var objArray = {
        "jsonOficina": fnObtenerJsonOficina()
    },

        params = fnParamsString(objArray);

    var fnComplete = function () {
        var data = fnGetJSONResponse('ActualizarOficina', params);

        if (data !== "" && data !== null) {
            if (data.respuesta !== null) {
                fnMessage("Operación correcta", "La información fue exitósamente actualizada", fnIrConsultaDeActualizacion);
            } else {
                fnMessage("UPS! =(", "La información no fue guardada, favor de intentar nuevamente");
            }
        }
    };

    fnWaitForLoading(fnComplete);
}


function fnAsignarPosicion(myLatLng){
    var lat = myLatLng.lat();
    var lng = myLatLng.lng();

    $('#Latitud').val(lat);
    $('#Longitud').val(lng);
}

function fnNombreMarca() {    
    return "Oficina No." + $('#OficinaId').val();    
}


function fnIrConsulta() {
    fnWaitForPost();
    $("#callConsultaOficinas").click();    
}

function fnIrConsultaDeActualizacion() {
    fnWaitForPost();
    $("#callConsultaOficinas").click(); 
}



