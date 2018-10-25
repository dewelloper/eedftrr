/// <reference path="jquery.js" />
/// <reference path="jquery-ui.js" />
/// <reference path="base.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.modal.js" />
/// <reference path="jquery.textchange.js" />
/// <reference path="typeahead.bundle.js" />
/// <reference path="app.core.js" />
/// <reference path="app.functions.js" />


var invoiceLineItems = [];
var invoiceTaxParties = [];

var loadPopupBefore = false;

function appBaseLoad() {
    //loadInvoiceData();
    loadCities();
    loadCountries();
    loadDatePickers("txtInvoiceDate");
    loadEInvoiceUser();

    $("#newButton").click(function () {
        var invoiceLineItem = {};
        addInvoiceLine("New", invoiceLineItem);
        return false;
    });;
}

function appEditLoad() {
    $("#txtQuantity,#txtBaseAmount").bind('textchange', function () {
        onCalculateInvoiceLineAmount();
    });

    $("#btnSave").click(function () {
        var uuid = getParameterValues('UUID');

        var mode = "";
        if (isNullOrEmpty(uuid)) {
            mode = "IsNew";
        }
        createInvoiceVS2(mode);
    });

    $("#txtQuantity,#txtBaseAmount,#txtTaxPercent").bind('textchange', function () {
        onCalculateInvoiceTaxAmount();
    });

    $('#invoiceLineModal').on('hidden.bs.modal', function () {
        onInvoiceLineCallBack();
    })

    $("#txtTaxPercent").bind('textchange', function () {
        var taxPercent = getValue("txtTaxPercent");
        if (parseInt(taxPercent) == 0) {
            $("#txtTaxReason").prop('disabled', false);
        }
        setValue("txtInvoiceTaxPercent", toDecimalFormat(getValue("txtTaxPercent")));
    });
}


function loadInvoiceData() {

    $.ajax
    ({
        type: 'POST',
        url: '../GenericHelper.aspx/LoadInvoiceType',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var response = result.d;

            var toAppend = '';
            $.each(response, function (i, o) {
                toAppend += '<option value=' + o.Value + '>' + o.Key + '</option>';
            });

            $('#invoiceScenarios').append(toAppend);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR + textStatus + errorThrown;
            showUIMessage("error", "ApplicationError", errorMessage);
        }
    });



    $.ajax
    ({
        type: 'POST',
        url: '../GenericHelper.aspx/LoadInvoiceExchangeRate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var response = result.d;

            var toAppend = '';
            $.each(response, function (i, o) {
                toAppend += '<option value=' + o.Value + '>' + o.Key + '</option>';
            });

            $('#exchangeRates').append(toAppend);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $.ajax
    ({
        type: 'POST',
        url: '../GenericHelper.aspx/LoadInvoiceStatus',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var response = result.d;

            var toAppend = '';
            $.each(response, function (i, o) {
                toAppend += '<option value=' + o.Value + '>' + o.Key + '</option>';
            });

            $('#invoiceStatus').append(toAppend);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR + textStatus + errorThrown;
            showUIMessage("error", "ApplicationError", errorMessage);
        }
    });

}

function loadCities() {
    $("#cities").typeahead
    ({
        source: function (query, process) {
            return $.ajax({
                type: "POST",
                url: '../GenericHelper.aspx/GetCities',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var resultList = result.map(function (item) {
                        var aItem = { id: item.ID, name: item.Name };
                        return JSON.stringify(aItem);
                    });
                    return process(resultList);
                }
            });
        },
        matcher: function (obj) {
            var item = JSON.parse(obj);
            return ~item.name.toLowerCase().indexOf(this.query.toLowerCase())
        },
        sorter: function (items) {
            var beginswith = [], caseSensitive = [], caseInsensitive = [], item;
            while (aItem = items.shift()) {
                var item = JSON.parse(aItem);
                if (!item.name.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(JSON.stringify(item));
                else if (~item.name.indexOf(this.query)) caseSensitive.push(JSON.stringify(item));
                else caseInsensitive.push(JSON.stringify(item));
            }

            return beginswith.concat(caseSensitive, caseInsensitive)
        },
        highlighter: function (obj) {
            var item = JSON.parse(obj);
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&')
            return item.name.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                return '<strong>' + match + '</strong>'
            })
        },
        updater: function (obj) {
            var item = JSON.parse(obj);
            $('#IdControl').attr('value', item.id);
            return item.name;
        }
    });
}


function loadCountries() {
    $("#countries").typeahead
    ({
        source: function (query, process) {
            return $.ajax({
                type: "POST",
                url: '../GenericHelper.aspx/GetCountries',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var resultList = result.map(function (item) {
                        var aItem = { id: item.ID, name: item.Name };
                        return JSON.stringify(aItem);
                    });
                    return process(resultList);
                }
            });
        },
        matcher: function (obj) {
            var item = JSON.parse(obj);
            return ~item.name.toLowerCase().indexOf(this.query.toLowerCase())
        },
        sorter: function (items) {
            var beginswith = [], caseSensitive = [], caseInsensitive = [], item;
            while (aItem = items.shift()) {
                var item = JSON.parse(aItem);
                if (!item.name.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(JSON.stringify(item));
                else if (~item.name.indexOf(this.query)) caseSensitive.push(JSON.stringify(item));
                else caseInsensitive.push(JSON.stringify(item));
            }

            return beginswith.concat(caseSensitive, caseInsensitive)
        },
        highlighter: function (obj) {
            var item = JSON.parse(obj);
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&')
            return item.name.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                return '<strong>' + match + '</strong>'
            })
        },
        updater: function (obj) {
            var item = JSON.parse(obj);
            $('#countryID').attr('value', item.id);
            return item.name;
        }
    });
}

function createInvoiceVS2(mode) {
    var invoice = {};

    invoice.UUID = getValue("txtETTN");
    invoice.ID = getValue("txtInvoiceNumber");
    invoice.ProfileID = getValue("invoiceScenarios");
    invoice.InvoiceTypeCode = getValue("invoiceStatus");
    invoice.IssueDate = getValue("txtInvoiceDate");
    invoice.DocumentCurrencyCode = {};
    invoice.DocumentCurrencyCode.Value = getValue("exchangeRates");
    invoice.TaxCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.PaymentAlternativeCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.LineCountNumeric = 1;
    invoice.UBLVersionID = "2.1";
    invoice.PaymentCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.PricingCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.CustomizationID = getValue("txtCustomId");

    invoice.TaxExchangeRate = {};
    invoice.TaxExchangeRate.SourceCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.TaxExchangeRate.TargetCurrencyCode = "TRY";
    invoice.TaxExchangeRate.CalculationRate = toDecimalFormat(getValue("txtExchangeRate"));
    invoice.TaxExchangeRate.IssueDate = "2014-01-01";

    invoice.PaymentExchangeRate = {};
    invoice.PaymentExchangeRate.SourceCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.PaymentExchangeRate.TargetCurrencyCode = "TRY";
    invoice.PaymentExchangeRate.CalculationRate = toDecimalFormat(getValue("txtExchangeRate"));
    invoice.PaymentExchangeRate.IssueDate = "2014-01-01";


    invoice.PricingExchangeRate = {};
    invoice.PricingExchangeRate.SourceCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.PricingExchangeRate.TargetCurrencyCode = "TRY";
    invoice.PricingExchangeRate.CalculationRate = toDecimalFormat(getValue("txtExchangeRate"));
    invoice.PricingExchangeRate.IssueDate = "2014-01-01";


    invoice.PaymentAlternativeExchangeRate = {};
    invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode = invoice.DocumentCurrencyCode.Value;
    invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode = "TRY";
    invoice.PaymentAlternativeExchangeRate.CalculationRate = toDecimalFormat(getValue("txtExchangeRate"));
    invoice.PaymentAlternativeExchangeRate.IssueDate = "2014-01-01";

    invoice.AccountingCustomerPartyInfo = {};
    invoice.AccountingCustomerPartyInfo.Party = {};
    invoice.AccountingCustomerPartyInfo.Party.Alias = getValue("txtAlias");
    invoice.AccountingCustomerPartyInfo.Party.WebsiteURI = getValue("txtWebsite");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress = {};
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Room = getValue("txtDoorNumber");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName = getValue("txtBuildingName");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber = getValue("txtBuildingNo");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.StreetName = getValue("txtStreet");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CityName = getValue("cities");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName = getValue("txtQuarter");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.PostalZone = getValue("txtPostalCode");
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country = {};
    invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country.Name = getValue("countries");

    invoice.AccountingCustomerPartyInfo.Party.Contact = {};
    invoice.AccountingCustomerPartyInfo.Party.Contact.Telefax = getValue("txtTelefax");
    invoice.AccountingCustomerPartyInfo.Party.Contact.ElectronicMail = getValue("txtEmail");
    invoice.AccountingCustomerPartyInfo.Party.Contact.Telephone = getValue("txtPhone");

    invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme = {};
    invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme = {};
    invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme.Name = getValue("txtTaxOffice");


    var customerType = "VKN";

    invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications = [];
    var partyIdentification = {};
    partyIdentification.ID = {};

    var vkn_tckn = getValue("txtIdentification");

    if (vkn_tckn.length == 10) {
        partyIdentification.ID.Value = vkn_tckn;
        partyIdentification.ID.schemeID = "VKN";
    }
    if (vkn_tckn.length == 11) {
        partyIdentification.ID.Value = vkn_tckn;
        partyIdentification.ID.schemeID = "TCKN";
        customerType = "TCKN";
    }

    invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.push(partyIdentification);

    if (!isNullOrEmpty(getValue("txtCommercialNumber"))) {
        var partyIdentification = {};
        partyIdentification.ID = {};
        partyIdentification.ID.Value = getValue("txtCommercialNumber");
        partyIdentification.ID.Value = "TICARETSICILNO";
        invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.push(partyIdentification);
    }


    if (customerType == "VKN") {
        invoice.AccountingCustomerPartyInfo.Party.PartyName = {};
        invoice.AccountingCustomerPartyInfo.Party.PartyName.Name = getValue("txtName");
    }

    if (customerType == "TCKN") {
        invoice.AccountingCustomerPartyInfo.Party.Person = {};
        invoice.AccountingCustomerPartyInfo.Party.Person.FirstName = getValue("txtName");
        invoice.AccountingCustomerPartyInfo.Party.Person.FamilyName = getValue("txtSurname");
    }

    if (invoiceLineItems.length > 0) {
        invoice.InvoiceLineInfos = [];
        var obj = invoiceLineItems;
        for (var i = 0; i < obj.length; i++) {
            var invoiceLineInfo = {};
            invoiceLineInfo.ID = i + 1;
            invoiceLineInfo.Item = {};
            invoiceLineInfo.Item.Name = obj[i]["ItemName"];
            invoiceLineInfo.PriceInfo = {};
            invoiceLineInfo.PriceInfo.PriceAmount = {};
            invoiceLineInfo.PriceInfo.PriceAmount.Value = toDecimalFormat(obj[i]["InvoiceLineAmount"]);
            invoiceLineInfo.PriceInfo.PriceAmount.currencyID = invoice.DocumentCurrencyCode.Value;
            invoiceLineInfo.LineExtensionAmount = {};
            invoiceLineInfo.LineExtensionAmount.Value = toDecimalFormat(obj[i]["BaseAmount"]);
            invoiceLineInfo.LineExtensionAmount.currencyID = invoice.DocumentCurrencyCode.Value;
            invoiceLineInfo.InvoicedQuantity = {};
            invoiceLineInfo.InvoicedQuantity.Value = toDecimalFormat(obj[i]["Quantity"]);
            invoiceLineInfo.InvoicedQuantity.unitCode = obj[i]["UnitCode"];

            if (toDecimalFormat(obj[i]["FactorNumeric"]) == 0) {
                invoiceLineInfo.AllowanceChargeInfo = {};
                invoiceLineInfo.AllowanceChargeInfo.ChargeIndicator = false;
                invoiceLineInfo.AllowanceChargeInfo.AllowanceChargeReason = "";
                invoiceLineInfo.AllowanceChargeInfo.MultiplierFactorNumeric = 0;
                invoiceLineInfo.AllowanceChargeInfo.Amount = {};
                invoiceLineInfo.AllowanceChargeInfo.Amount.Value = 0;
                invoiceLineInfo.AllowanceChargeInfo.BaseAmount = {};
                invoiceLineInfo.AllowanceChargeInfo.BaseAmount.Value = 0;
            }


            invoiceLineInfo.TaxTotal = {};
            invoiceLineInfo.TaxTotal.TaxAmount = {};
            invoiceLineInfo.TaxTotal.TaxAmount.Value = toDecimalFormat(obj[i].taxItems[0]["TaxItemAmount"]);
            invoiceLineInfo.TaxTotal.TaxAmount.currencyID = invoice.DocumentCurrencyCode.Value;

            invoiceLineInfo.TaxTotal.TaxSubTotals = [];
            var taxSubTotal = {};
            taxSubTotal.TaxAmount = {};
            taxSubTotal.TaxAmount.Value = toDecimalFormat((obj[i].taxItems[0]["TaxItemAmount"]));
            taxSubTotal.Percent = toDecimalFormat((obj[i].taxItems[0]["TaxItemPercent"]));
            taxSubTotal.TaxCategory = {};
            taxSubTotal.TaxCategory.TaxScheme = {};
            taxSubTotal.TaxCategory.TaxScheme.Name = obj[i].taxItems[0]["TaxItemName"];
            taxSubTotal.TaxCategory.TaxScheme.TaxTypeCode = obj[i].taxItems[0]["TaxItemCode"];

            invoiceLineInfo.TaxTotal.TaxSubTotals.push(taxSubTotal);

            invoice.InvoiceLineInfos.push(invoiceLineInfo);


        }

        invoice.LegalMonetaryTotal = {};
        invoice.LegalMonetaryTotal.LineExtensionAmount = {};
        invoice.LegalMonetaryTotal.LineExtensionAmount.Value = toDecimalFormat(getValue("txtLineExtensionAmount"));

        invoice.LegalMonetaryTotal.TaxExclusiveAmount = {};
        invoice.LegalMonetaryTotal.TaxExclusiveAmount.Value = toDecimalFormat(getValue("txtTaxExclusiveAmount"));

        invoice.LegalMonetaryTotal.AllowanceTotalAmount = {};
        invoice.LegalMonetaryTotal.AllowanceTotalAmount.Value = toDecimalFormat(getValue("txtAllowanceTotalAmount"));

        invoice.LegalMonetaryTotal.TaxInclusiveAmount = {};
        invoice.LegalMonetaryTotal.TaxInclusiveAmount.Value = toDecimalFormat(getValue("txtTaxInclusiveAmount"));

        invoice.LegalMonetaryTotal.PayableAmount = {};
        invoice.LegalMonetaryTotal.PayableAmount.Value = toDecimalFormat(getValue("txtTotalPaymentAmount"));
    }

    var success = function (result) {


        var response = result.d;
        if (response.IsSuccess) {

            alert("Kayıt Başarılı");

            if (mode == "IsNew") {
                window.location = 'CreateInvoice.aspx?UUID=' + response.UUID + '&DocumentID=' + response.DocumentID;
            }
        }
        else {
            showUIMessage('error', response.Message);
        }
    };

    var data = JSON.stringify
    ({
        invoice: invoice,
    });
    callAJAX("CreateInvoiceVS2", data, success);
}


function onExecuteOnlySelected() {
    $("input:checkbox").on('click', function () {
        var $box = $(this);
        if ($box.is(":checked")) {
            var group = "input:checkbox[name='" + $box.attr("name") + "']";
            $(group).prop("checked", false);
            $box.prop("checked", true);
        }
        else {
            $box.prop("checked", false);
        }
    });
}

function getInvoiceByUUID(uuid) {
    var invoiceHeader =
        {
            "UUID": uuid,
        };

    var data = JSON.stringify
    ({
        invoiceHeaders: invoiceHeader,
    });

    var onSuccess = function (result) {
        if (!result.d.HasError) {
            setInvoice(result.d.Result);
        }
    };
    callAJAX("getInvoiceByUUID", data, onSuccess);
}

function setInvoice(invoice) {
    setValue("txtETTN", invoice.UUID);
    setValue("txtInvoiceNumber", invoice.ID);
    setValue("txtInvoiceDate", invoice.IssueDate);
    setValue("txtCustomId", invoice.CustomizationID);
    setValue("invoiceStatus", invoice.InvoiceTypeCode);
    setValue("invoiceScenarios", invoice.ProfileID);
    setValue("exchangeRates", invoice.DocumentCurrencyCode.Value);
    var currencyCode = invoice.DocumentCurrencyCode.Value;

    if (currencyCode != "TRY") {
        setValue("txtExchangeRate", 0);
    }
    else {
        setValue("txtExchangeRate", 0);
    }



    setValue("txtAlias", invoice.AccountingCustomerPartyInfo.Party.Alias);

    var customerType = invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications[0].ID.schemeID;
    var identification = invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications[0].ID.Value;
    setValue("txtIdentification", identification);

    if (customerType == "VKN") {
        setValue("txtName", invoice.AccountingCustomerPartyInfo.Party.PartyName.Name);
        setDisableProperty("txtSurname", true);
    }
    else {
        setValue("txtName", invoice.AccountingCustomerPartyInfo.Party.Person.FirstName);
        setValue("txtSurname", invoice.AccountingCustomerPartyInfo.Party.Person.FamilyName);
    }

    if (invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.length > 1) {
        setValue("txtCommercialNumber", invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications[1].ID.Value);
    }

    setValue("countries", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country.Name);
    setValue("cities", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CityName);
    setValue("txtPhone", invoice.AccountingCustomerPartyInfo.Party.Contact.Telephone);
    setValue("txtTelefax", invoice.AccountingCustomerPartyInfo.Party.Contact.Telefax);
    setValue("txtEmail", invoice.AccountingCustomerPartyInfo.Party.Contact.ElectronicMail);
    setValue("txtWebsite", invoice.AccountingCustomerPartyInfo.Party.WebsiteURI);
    setValue("txtTaxOffice", invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme.Value);
    setValue("txtStreet", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.StreetName);
    setValue("txtQuarter", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName);
    setValue("txtBuildingName", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName);
    setValue("txtBuildingNo", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber);
    setValue("txtDoorNumber", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Room);
    setValue("txtTown", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Region);
    setValue("txtPostalCode", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.PostalZone);

    var invoiceLineCount = invoice.InvoiceLineInfos.length;

    if (invoiceLineCount > 0) {
        invoiceLineItems = [];
        var obj = invoice.InvoiceLineInfos;
        for (var i = 0; i < obj.length; i++) {
            var invoiceLineItem = {};
            invoiceLineItem.Id = appIdLoan();
            invoiceLineItem.ItemName = obj[i].Item.Name;
            invoiceLineItem.UnitCode = obj[i].InvoicedQuantity.unitCode;
            invoiceLineItem.Quantity = obj[i].InvoicedQuantity.Value;
            invoiceLineItem.BaseAmount = obj[i].LineExtensionAmount.Value;
            invoiceLineItem.FactorNumeric = obj[i].AllowanceChargeInfo.MultiplierFactorNumeric;
            invoiceLineItem.TaxAmount = obj[i].TaxTotal.TaxAmount.Value;
            invoiceLineItem.FactorAmount = obj[i].AllowanceChargeInfo.Amount.Value;
            invoiceLineItem.InvoiceLineAmount = obj[i].PriceInfo.PriceAmount.Value;
            invoiceLineItem.TaxPercent = obj[i].TaxTotal.TaxSubTotals[0].Percent;
            invoiceLineItem.TaxReason = obj[i].TaxTotal.TaxSubTotals[0].TaxCategory.TaxExemptionReason;
            invoiceLineItems.push(invoiceLineItem);

            invoiceLineItem.taxItems = [];
            var taxItem = {};
            taxItem.TaxItemPercent = obj[i].TaxTotal.TaxSubTotals[0].Percent;
            taxItem.TaxItemCode = obj[i].TaxTotal.TaxSubTotals[0].TaxCategory.TaxScheme.TaxTypeCode;
            taxItem.TaxItemName = obj[i].TaxTotal.TaxSubTotals[0].TaxCategory.TaxScheme.Name;
            taxItem.TaxItemAmount = obj[i].TaxTotal.TaxSubTotals[0].TaxAmount.Value;
            invoiceLineItem.taxItems.push(taxItem);
        }

        pushObjectToTable(invoiceLineItems);

        setValue("txtAllowanceTotalAmount", invoice.LegalMonetaryTotal.AllowanceTotalAmount.Value);
        setValue("txtLineExtensionAmount", invoice.LegalMonetaryTotal.LineExtensionAmount.Value);
        setValue("txtTaxExclusiveAmount", invoice.LegalMonetaryTotal.TaxExclusiveAmount.Value);
        setValue("txtTaxInclusiveAmount", invoice.LegalMonetaryTotal.TaxInclusiveAmount.Value);
        setValue("txtTotalPaymentAmount", invoice.LegalMonetaryTotal.PayableAmount.Value);
        //Setting LegalMonetaryTotal...

        onExecuteOnlySelected();
    }

}


function loadEInvoiceUser() {
    $("#txtAlias").typeahead
    ({
        source: function (query, process) {
            return $.ajax({
                type: "POST",
                url: '../GenericHelper.aspx/GetEInvoiceUsers',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var resultList = result.map(function (item) {
                        var aItem = { id: item.Identifier, name: item.Name, alias: item.Alias, };
                        return JSON.stringify(aItem);
                    });
                    return process(resultList);
                }
            });
        },
        matcher: function (obj) {
            var item = JSON.parse(obj);
            return ~item.name.toLowerCase().indexOf(this.query.toLowerCase())
        },
        sorter: function (items) {
            var beginswith = [], caseSensitive = [], caseInsensitive = [], item;
            while (aItem = items.shift()) {
                var item = JSON.parse(aItem);
                if (!item.name.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(JSON.stringify(item));
                else if (~item.name.indexOf(this.query)) caseSensitive.push(JSON.stringify(item));
                else caseInsensitive.push(JSON.stringify(item));
            }

            return beginswith.concat(caseSensitive, caseInsensitive)
        },
        highlighter: function (obj) {
            var item = JSON.parse(obj);
            var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&')
            return item.name.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                return '<strong>' + match + '</strong>';
            })
        },
        updater: function (obj) {
            var item = JSON.parse(obj);
            $('#txtAlias').attr('value', item.alias);
            $("#txtIdentification").attr('value', item.id);
            $("#txtName").attr('value', item.name);

            if (item.id.length == 10) {
                $("#txtSurname").prop('disabled', true);
            }

            return item.alias;
        }
    });
}

function addInvoiceLine(loadMode, invoiceLineItem) {

    $("#invoiceLineModal").modal();

    if (loadPopupBefore == false) {
        loadBaseLookupItems("GetUnitCodes", "unitCodes");
        loadBaseLookupItems("GetTaxTypes", "taxes");
    }

    $("#btnSaveInvoiceLine").on('click', function () {
        onSaveInvoiceLine(loadMode, invoiceLineItem);
    });

    if (loadMode == "Edit") {
        setValue("txtItemName", invoiceLineItem[0]["ItemName"]);
        setValue("txtQuantity", invoiceLineItem[0]["Quantity"]);
        setValue("unitCodes", invoiceLineItem[0]["UnitCode"]);
        setValue("txtBaseAmount", invoiceLineItem[0]["BaseAmount"]);
        setValue("txtFactorNumeric", invoiceLineItem[0]["FactorNumeric"]);
        setValue("txtFactorAmount", invoiceLineItem[0]["FactorAmount"]);
        setValue("txtTaxPercent", invoiceLineItem[0]["TaxPercent"]);
        setValue("txtTaxAmount", invoiceLineItem[0]["TaxAmount"]);
        setValue("txtTaxReason", invoiceLineItem[0]["TaxReason"]);
        setValue("txtInvoiceLineAmount", invoiceLineItem[0]["InvoiceLineAmount"]);
    }

    loadPopupBefore = true;
    return false;
}

function onSaveInvoiceLine(loadMode, obj) {

    var itemName = getValue("txtItemName");
    var quantity = getValue("txtQuantity");
    var baseAmount = getValue("txtBaseAmount");

    if (isNullOrEmpty(itemName)) {
        alert("Hizmet Adı Girmelisiniz");
        //showUIMessage("info", "Hizmet Ekleme", "Hizmet Adını Giriniz");
        return false;
    }

    if (parseInt(quantity) == 0) {
        alert("Miktar 0 Olamaz");
        return false;
    }

    if (parseInt(baseAmount) == 0) {
        alert("Birim Fiyat 0 Olamaz");
        return false;
    }



    if (loadMode == "New") {
        var invoiceLineItem = {};
        invoiceLineItem.Id = appIdLoan();
        invoiceLineItem.ItemName = getValue("txtItemName");
        invoiceLineItem.UnitCode = getValue("unitCodes");
        invoiceLineItem.Quantity = getValue("txtQuantity");
        invoiceLineItem.BaseAmount = getValue("txtBaseAmount");
        invoiceLineItem.FactorNumeric = getValue("txtFactorNumeric");
        invoiceLineItem.TaxAmount = getValue("txtTaxAmount");
        invoiceLineItem.FactorAmount = getValue("txtFactorAmount");
        invoiceLineItem.InvoiceLineAmount = getValue("txtInvoiceLineAmount");
        invoiceLineItem.TaxPercent = getValue("txtTaxPercent");
        invoiceLineItem.TaxReason = getValue("txtTaxReason");
        setValue("txtInvoiceTaxAmount", toDecimalFormat(getValue("txtTaxAmount")));
        invoiceLineItem.TaxAmount = getValue("txtTaxAmount");

        var taxItems = [];
        var tax = {};
        tax.LineID = invoiceLineItem.Id;
        tax.TaxItemCode = "0015";
        tax.TaxItemName = "KDV";
        tax.TaxItemPercent = getValue("txtInvoiceTaxPercent");
        tax.TaxItemAmount = getValue("txtInvoiceTaxAmount");
        taxItems.push(tax);
        invoiceLineItem.taxItems = taxItems;

        invoiceLineItems.push(invoiceLineItem);
    }

    if (loadMode == "Edit") {
        setValue("txtInvoiceTaxAmount", toDecimalFormat(getValue("txtTaxAmount")));
        for (var i = 0; i < invoiceLineItems.length; i++) {
            if (invoiceLineItems[i]["Id"] == obj[0]["Id"]) {
                invoiceLineItems[i]["Id"] = obj[0]["Id"];
                invoiceLineItems[i]["ItemName"] = getValue("txtItemName");
                invoiceLineItems[i]["UnitCode"] = getValue("unitCodes");
                invoiceLineItems[i]["Quantity"] = getValue("txtQuantity");
                invoiceLineItems[i]["BaseAmount"] = getValue("txtBaseAmount");
                invoiceLineItems[i]["FactorNumeric"] = getValue("txtFactorNumeric");
                invoiceLineItems[i]["TaxAmount"] = getValue("txtTaxAmount");
                invoiceLineItems[i]["FactorAmount"] = getValue("txtFactorAmount");
                invoiceLineItems[i]["InvoiceLineAmount"] = getValue("txtInvoiceLineAmount");
                invoiceLineItems[i]["TaxPercent"] = getValue("txtTaxPercent");
                invoiceLineItems[i]["TaxReason"] = getValue("txtTaxReason");

                invoiceLineItems[i].taxItems[0]["LineID"] = obj[0]["Id"];
                invoiceLineItems[i].taxItems[0]["TaxItemCode"] = "0015";
                invoiceLineItems[i].taxItems[0]["TaxItemName"] = "KDV";
                invoiceLineItems[i].taxItems[0]["TaxItemPercent"] = getValue("txtInvoiceTaxPercent");
                invoiceLineItems[i].taxItems[0]["TaxItemAmount"] = getValue("txtInvoiceTaxAmount");
            }
        }
    }

    $('#invoiceLineModal').modal('hide');
    $('#btnSaveInvoiceLine').unbind('click');
}

function onCalculateInvoiceLineAmount() {
    var quantity = getValue("txtQuantity");
    var baseAmount = getValue("txtBaseAmount");

    var invoiceLineAmount = quantity * baseAmount;
    setValue("txtInvoiceLineAmount", toDecimalFormat(invoiceLineAmount));
}

function onCalculateInvoiceTaxAmount() {
    var quantity = getValue("txtQuantity");
    var baseAmount = getValue("txtBaseAmount");
    var taxPercent = getValue("txtTaxPercent");
    var taxAmount = quantity * baseAmount * taxPercent / 100;
    setValue("txtTaxAmount", toDecimalFormat(taxAmount));
}

function onInvoiceLineCallBack() {
    var items = invoiceLineItems;
    pushObjectToTable(items);
}

function removeSelectedInvoiceLine() {
    
    var selected = $('#invoiceLines').find('input[type="checkbox"]:checked').each(function () {
        var id = this.id;
        var result = confirm("Seçili Hizmeti Silmek İstiyormusunuz ?");
        if (result) {
            var obj = jQuery.grep(invoiceLineItems, function (value) {
                return value.Id != id;
            });
            invoiceLineItems = obj;
            pushObjectToTable(invoiceLineItems);
        }
    });


    if (selected.length == 0) {
        alert("Silmek Istediginiz Hizmeti Seciniz");
    }
}

function editSelectedInvoiceLine()
{

    var selected = $('#invoiceLines').find('input[type="checkbox"]:checked').each(function () {
        var id = this.id;
        var invoice = jQuery.grep(invoiceLineItems, function (e) {
            return e.Id == id;
        });
        addInvoiceLine("Edit", invoice);
    });


    if (selected.length == 0) {
        alert("Düzenlemek Istediginiz Hizmeti Seciniz");
    }
}

function pushObjectToTable(obj) {
    $("#invoiceLineBody").empty();

    var lineExtensionAmount = 0;
    var allowanceTotalAmount = 0;
    var taxExlusiveAmount = 0;
    var taxInclusiveAmount = 0;
    var paymentAmount = 0;

    for (var i = 0; i < obj.length; i++) {
        var tr = "<tr>";

        var invoiceLineId = obj[i]["Id"];
        var checkItem = "<td>" + "<input name='x'  type='checkbox' id='" + invoiceLineId + "' />" + "</td>";
        var name = "<td>" + obj[i]["ItemName"] + "</td>";
        var unitCode = "<td>" + obj[i]["UnitCode"] + "</td>";
        var quantity = "<td>" + obj[i]["Quantity"] + "</td>";
        var baseAmount = "<td>" + obj[i]["BaseAmount"] + "</td>";
        var invoiceAmount = "<td>" + obj[i]["InvoiceLineAmount"] + "</td>";
        var taxPercent = "<td>" + obj[i]["TaxPercent"] + "</td>";
        var taxAmount = "<td>" + obj[i]["TaxAmount"] + "</td>";
        var factorPercent = "<td>" + obj[i]["FactorNumeric"] + "</td>";
        var factorAmount = "<td>" + obj[i]["FactorAmount"] + "</td>";
        var html = tr + checkItem + name + unitCode + quantity + baseAmount + invoiceAmount + taxPercent + taxAmount + factorPercent + factorAmount;
        $("#invoiceLines").append(html);

        lineExtensionAmount = parseFloat(obj[i]["InvoiceLineAmount"]) + parseFloat(lineExtensionAmount);
        allowanceTotalAmount = parseFloat(obj[i]["FactorAmount"]) + parseFloat(allowanceTotalAmount);
        taxExlusiveAmount = lineExtensionAmount;// parseFloat(lineExtensionAmount) + parseFloat(taxExlusiveAmount);

        var refTaxAmount = 0;
        for (var j = 0; j < obj[i].taxItems.length; j++) {
            refTaxAmount = parseFloat(obj[i].taxItems[j]["TaxItemAmount"]) + parseFloat(refTaxAmount);
        }


        taxInclusiveAmount = parseFloat(taxInclusiveAmount) + parseFloat(refTaxAmount) + parseFloat(obj[i]["InvoiceLineAmount"]);
    }

    setValue("txtLineExtensionAmount", toDecimalFormat(lineExtensionAmount));
    setValue("txtAllowanceTotalAmount", toDecimalFormat(allowanceTotalAmount));
    setValue("txtTaxExclusiveAmount", toDecimalFormat(taxExlusiveAmount));
    setValue("txtTaxInclusiveAmount", toDecimalFormat(taxInclusiveAmount));
    setValue("txtTotalPaymentAmount", toDecimalFormat(taxInclusiveAmount));
}
