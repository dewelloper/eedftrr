﻿/**
 * bootbox.js v2.5.1
 *
 * http://bootboxjs.com/license.txt
 */
var bootbox = window.bootbox || function (k) {
    function h(b, a) { null == a && (a = m); return "string" == typeof i[a][b] ? i[a][b] : a != n ? h(b, n) : b } var m = "en", n = "en", s = !0, r = "static", t = "", j = {}, e = {}, i = {
        en: { OK: "OK", CANCEL: "Cancel", CONFIRM: "OK" }, fr: { OK: "OK", CANCEL: "Annuler", CONFIRM: "D'accord" }, de: { OK: "OK", CANCEL: "Abbrechen", CONFIRM: "Akzeptieren" }, es: { OK: "OK", CANCEL: "Cancelar", CONFIRM: "Aceptar" }, br: { OK: "OK", CANCEL: "Cancelar", CONFIRM: "Sim" }, nl: { OK: "OK", CANCEL: "Annuleren", CONFIRM: "Accepteren" }, ru: {
            OK: "OK", CANCEL: "\u041e\u0442\u043c\u0435\u043d\u0430",
            CONFIRM: "\u041f\u0440\u0438\u043c\u0435\u043d\u0438\u0442\u044c"
        }, it: { OK: "OK", CANCEL: "Annulla", CONFIRM: "Conferma" }
    }; e.setLocale = function (b) { for (var a in i) if (a == b) { m = b; return } throw Error("Invalid locale: " + b); }; e.addLocale = function (b, a) { "undefined" == typeof i[b] && (i[b] = {}); for (var c in a) i[b][c] = a[c] }; e.setIcons = function (b) { j = b; if ("object" !== typeof j || null == j) j = {} }; e.alert = function () {
        var b = "", a = h("OK"), c = null; switch (arguments.length) {
            case 1: b = arguments[0]; break; case 2: b = arguments[0]; "function" == typeof arguments[1] ?
            c = arguments[1] : a = arguments[1]; break; case 3: b = arguments[0]; a = arguments[1]; c = arguments[2]; break; default: throw Error("Incorrect number of arguments: expected 1-3");
        } return e.dialog(b, { label: a, icon: j.OK, callback: c }, { onEscape: c })
    }; e.confirm = function () {
        var b = "", a = h("CANCEL"), c = h("CONFIRM"), f = null; switch (arguments.length) {
            case 1: b = arguments[0]; break; case 2: b = arguments[0]; "function" == typeof arguments[1] ? f = arguments[1] : a = arguments[1]; break; case 3: b = arguments[0]; a = arguments[1]; "function" == typeof arguments[2] ?
            f = arguments[2] : c = arguments[2]; break; case 4: b = arguments[0]; a = arguments[1]; c = arguments[2]; f = arguments[3]; break; default: throw Error("Incorrect number of arguments: expected 1-4");
        } return e.dialog(b, [{ label: a, icon: j.CANCEL, callback: function () { "function" == typeof f && f(!1) } }, { label: c, icon: j.CONFIRM, callback: function () { "function" == typeof f && f(!0) } }])
    }; e.prompt = function () {
        var b = "", a = h("CANCEL"), c = h("CONFIRM"), f = null, u = ""; switch (arguments.length) {
            case 1: b = arguments[0]; break; case 2: b = arguments[0]; "function" ==
            typeof arguments[1] ? f = arguments[1] : a = arguments[1]; break; case 3: b = arguments[0]; a = arguments[1]; "function" == typeof arguments[2] ? f = arguments[2] : c = arguments[2]; break; case 4: b = arguments[0]; a = arguments[1]; c = arguments[2]; f = arguments[3]; break; case 5: b = arguments[0]; a = arguments[1]; c = arguments[2]; f = arguments[3]; u = arguments[4]; break; default: throw Error("Incorrect number of arguments: expected 1-5");
        } var p = k("<form></form>"); p.append("<input autocomplete=off type=text value='" + u + "' />"); var d = e.dialog(p, [{
            label: a,
            icon: j.CANCEL, callback: function () { "function" == typeof f && f(null) }
        }, { label: c, icon: j.CONFIRM, callback: function () { "function" == typeof f && f(p.find("input[type=text]").val()) } }], { header: b }); d.on("shown", function () { p.find("input[type=text]").focus(); p.on("submit", function (a) { a.preventDefault(); d.find(".btn-primary").click() }) }); return d
    }; e.modal = function () {
        var b, a, c, f = { onEscape: null, keyboard: !0, backdrop: r }; switch (arguments.length) {
            case 1: b = arguments[0]; break; case 2: b = arguments[0]; "object" == typeof arguments[1] ?
            c = arguments[1] : a = arguments[1]; break; case 3: b = arguments[0]; a = arguments[1]; c = arguments[2]; break; default: throw Error("Incorrect number of arguments: expected 1-3");
        } f.header = a; c = "object" == typeof c ? k.extend(f, c) : f; return e.dialog(b, [], c)
    }; e.dialog = function (b, a, c) {
        var f = null, e = "", j = [], c = c || {}; null == a ? a = [] : "undefined" == typeof a.length && (a = [a]); for (var d = a.length; d--;) {
            var h = null, i = null, l = null, m = "", n = null; if ("undefined" == typeof a[d].label && "undefined" == typeof a[d]["class"] && "undefined" == typeof a[d].callback) {
                var h =
                0, i = null, q; for (q in a[d]) if (i = q, 1 < ++h) break; 1 == h && "function" == typeof a[d][q] && (a[d].label = i, a[d].callback = a[d][q])
            } "function" == typeof a[d].callback && (n = a[d].callback); a[d]["class"] ? l = a[d]["class"] : d == a.length - 1 && 2 >= a.length && (l = "btn-primary"); h = a[d].label ? a[d].label : "Option " + (d + 1); a[d].icon && (m = "<i class='" + a[d].icon + "'></i> "); i = a[d].href ? a[d].href : "javascript:;"; e += "<a data-handler='" + d + "' class='btn " + l + "' href='" + i + "'>" + m + "" + h + "</a>"; j[d] = n
        } d = ["<div class='bootbox modal' style='overflow:hidden;'>"];
        if (c.header) { l = ""; if ("undefined" == typeof c.headerCloseButton || c.headerCloseButton) l = "<a href='javascript:;' class='close'>&times;</a>"; d.push("<div class='modal-header'>" + l + "<h3>" + c.header + "</h3></div>") } d.push("<div class='modal-body'></div>"); e && d.push("<div class='modal-footer'>" + e + "</div>"); d.push("</div>"); var g = k(d.join("\n")); ("undefined" === typeof c.animate ? s : c.animate) && g.addClass("fade"); (e = "undefined" === typeof c.classes ? t : c.classes) && g.addClass(e); k(".modal-body", g).html(b); g.bind("hidden",
        function () { g.remove() }); g.bind("hide", function () { if ("escape" == f && "function" == typeof c.onEscape) c.onEscape() }); k(document).bind("keyup.modal", function (a) { 27 == a.which && (f = "escape") }); g.bind("shown", function () { k("a.btn-primary:last", g).focus() }); g.on("click", ".modal-footer a, a.close", function (b) { var c = k(this).data("handler"), d = j[c], e = null; "undefined" !== typeof c && "undefined" !== typeof a[c].href || (b.preventDefault(), "function" == typeof d && (e = d()), !1 !== e && (f = "button", g.modal("hide"))) }); null == c.keyboard &&
        (c.keyboard = "function" == typeof c.onEscape); k("body").append(g); g.modal({ backdrop: "undefined" === typeof c.backdrop ? r : c.backdrop, keyboard: c.keyboard }); return g
    }; e.hideAll = function () { k(".bootbox").modal("hide") }; e.animate = function (b) { s = b }; e.backdrop = function (b) { r = b }; e.classes = function (b) { t = b }; return e
}(window.jQuery); window.bootbox = bootbox;