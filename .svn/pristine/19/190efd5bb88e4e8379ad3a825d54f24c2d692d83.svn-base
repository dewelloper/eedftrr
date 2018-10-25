

/// <reference path="jquery.js"></reference>
/// <reference path="jqueryui.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.modal.js" />


function showBaseMessage(message) {
    alert(message);
}

function isNullOrEmpty(value) {
    if (value == "" || value == '' || value == null || value == undefined) {
        return true;
    }
    return false;
}

function onlyNumber(e) {
    var keyCode = event.keyCode;
    if ((keyCode < 46 || keyCode > 57) && keyCode != 8 && keyCode != 9 && keyCode != 0 && keyCode != 47 && (keyCode < 96 || keyCode > 105)) {
        return false;
    }
}

function appIdLoan() {
    return Math.round(new Date().getTime() + (Math.random() * 100));
}


function toUIDate(date) {
    var splittedValues = date.split('-');
    return splittedValues[2] + "." + splittedValues[1] + "." + splittedValues[0];
}

function showUIMessage(type, message) {
    modal({
        type: type,
        title: "",
        text: message,
        autoclose: false,
        closeClick: false,
    });
}

function onlyLetters(event) {
    var inputValue = event.which;
    // allow letters and whitespaces only.
    if ((inputValue > 47 && inputValue < 58) && (inputValue != 32)) {
        event.preventDefault();
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode == 46) {
        var inputValue = $("#inputfield").val()
        if (inputValue.indexOf('.') < 1) {
            return true;
        }
        return false;
    }
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}


function setUIFocus(element) {
    var id = "#" + element;
    $(id).focus();
}

function setBorder(element) {
    var id = "#" + element;
    $(id).css('border', '1px solid red');
}

function removeBorder(element) {
    var id = "#" + element;
    $(id).css('border', '');
}


function validateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};


function toDecimalFormat(value) {
    return parseFloat(("" + value).replace(',', '').replace(' ', '')).toFixed(2);
}

function getValue(id) {
    var prefix = "#" + id;
    return $(prefix).val();
}

function setValue(id, value) {
    var prefix = "#" + id;
    return $(prefix).val(value);
}

function ShowDiv(id) {
    return $(id).show();
}

function HideDiv(id) {
    return $(id).hide();
}

function hideDivElement(id) {
    id = "#" + id;
    $(id).hide();
}

function getId(id) {
    var masterViewPrefix = "";
    var ajaxPrefix = "#";
    return masterViewPrefix + ajaxPrefix + id;
}

function getInputValue(id) {
    var controlId = "#" + id;
    return $(controlId).val();
}

var createCookie = function (name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = document.cookie.length;
            }
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}


function intToBoolean(value) {
    if (value == "1") {
        return true;
    }
    else {
        return false;
    }
}

function booleanToInt(value) {
    if (value == true) {
        return "1";
    }
    else {
        return "0";
    }
}



function showBaseJSMessage(type, title, message) {
    showJSMessage(type, title, message, false);
}

function showJSMessage(type, title, message, oldVersion) {

    if (oldVersion == false) {
        message = message.replace('\n', '<br/>').replace('\n', '<br/>').replace('\n', '<br/>');
        Lobibox.alert(type, { msg: message, title: title });
    }
    else {
        alert(message);
    }
}


function setSearchText(message) {
    $("#loadingMessage").html(message);
}


function getParameterValues(key) {
    var pageURL = window.location.search.substring(1);
    var urlQS = pageURL.split('&');
    for (var i = 0; i < urlQS.length; i++) {
        var paramName = urlQS[i].split('=');
        if (paramName[0] == key) {
            var value = paramName[1].replace('%20', ' ').replace('%26', '&').replace('+', ' ');
            return value;
        }
    }
}



function loadDatePickers(startDateId, minDateParam) {

    var dateToday = new Date();

    $("#" + startDateId).datepicker
    ({

    })

    if (minDateParam) {
        $("#" + startDateId).datepicker("option", "minDate", dateToday);
    }
    $("#" + startDateId).datepicker("option", "dateFormat", "dd.mm.yy");
}

