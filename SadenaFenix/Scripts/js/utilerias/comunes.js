
$(function () {
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
});

function fnGetJSONResponse(actionName, parameters) {
    var httpRequest = fnPrepareHttpRequest(actionName);
    httpRequest.send(parameters);
    var content = httpRequest.responseText;
    return fnParseJSONResponse(content);
}

function fnPrepareHttpRequest(actionName) {
    var httpRequest = fnGenReqObj();
    httpRequest.open('POST', actionName, false);
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded; charset=UTF-8");

    return httpRequest;
}

/**get a new request */
function fnGenReqObj() {
    var httpRequest = null;
    if (window.XMLHttpRequest)
        httpRequest = new XMLHttpRequest();
    else if (window.ActiveXObject) {
        try {
            httpRequest = new ActiveXObject("MSXML2.XMLHTTP");
        } catch (e) {
            httpRequest = new AXObject("Microsoft.XMLHTTP");
        }
    }
    return httpRequest;
}

function fnParseJSONResponse(content) {
    var jsonResponse = "";

    try {
        jsonResponse = $.parseJSON(content);
    } catch (e) {
        jsonResponse = "";
    }

    return jsonResponse;
}



function fnParamsString(objArray) {
    var paramsArray = "";
    var index = 0;
    $.each(objArray, function (key, value) {
        if (index === 0) {
            paramsArray += key + "=" + value;
        } else {
            paramsArray += "&" + key + "=" + value;
        }
        index++;
    });
    return paramsArray;
}

function fnMessage(title, message, finalFunction, clase) {
    var idDiv = $('#AlertaMensaje');

    if (clase != undefined) {
        idDiv.toggleClass("alert-success", false);
        idDiv.toggleClass(clase, true);
    }

    idDiv.stop(true, true);
    idDiv.removeAttr('style');
    idDiv.find('#Titulo').text(title);
    idDiv.find('#Mensaje').text(message);
    idDiv.fadeOut(10000, "linear", function () {
        if (finalFunction !== undefined)
            finalFunction();
    });
    idDiv.find('#CierreMensaje').on("click", function () {
        idDiv.stop(true, true);
    });

    $('html, body').animate({
        scrollTop: 0
    }, 100);
}

function fnMensajeBotonesLista(tittle, message, btnList, clase) {
    var idDiv = $('#ConfirmacionMensaje'),
        containerDiv = $('#ConfirmacionMensaje #buttonsContainer'),
        transparentLayer = $('#transparentBackLayer');

    if (clase != undefined) {
        idDiv.toggleClass("alert-warning", false);
        idDiv.toggleClass(clase, true);
    }

    idDiv.find('#Titulo').text(tittle);
    idDiv.find('#Mensaje').text(message);

    containerDiv.empty();
    $.each(btnList, function (key, value) {
        var btn = $('<button/>',
            {
                text: key,
                class: containerDiv.attr('classToChildren'),
                click: function () {
                    transparentLayer.toggleClass("hiddElement", true);
                    idDiv.toggleClass("hiddElement", true);
                    value();
                }
            });

        containerDiv.append(btn);
    });

    idDiv.find('#closeMessage').on("click", function () {
        idDiv.toggleClass("hiddElement", true);
    });

    idDiv.toggleClass("hiddElement", false);
    transparentLayer.toggleClass("hiddElement", false);

    $('html, body').animate({
        scrollTop: 0
    }, 100);
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



$.fn.exists = function () {
    return this.val() !== undefined;
};

$.fn.isEmpty = function () {
    return this.val() === "";
};

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};