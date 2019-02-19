
$(window).on('load', function () {
    $("#loader").toggleClass("hiddElement");
});

$(window).on('unload', function () {
    fnWaitForPost();
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

function fnMessage(title, message, finalFunction) {
    var idDiv = $('#AlertaMensaje');
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
}

function fnShowDiv(div, showHide, isClass) {
    var selector;
    if (isClass !== undefined && isClass === true) {
        selector = $("." + div);
    } else {
        selector = $("#" + div);
    }

    if (showHide === 1)
        selector.toggleClass('hiddElement', false);
    if (showHide === 0)
        selector.toggleClass('hiddElement', true);
}


function fnWaitForLoading(func) {
    var loader = $("#loader");
    fnShowDiv(loader.attr('id'), 1);
    loader.fadeOut(5000, function () {
        func();
        loader.removeAttr('style');
        fnShowDiv(loader.attr('id'), 0);        
    });
}

function fnWaitForPost() {
    fnShowDiv("loader", 1);   
}


function fnCompleteWait() {
    fnShowDiv("loader", 0);
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