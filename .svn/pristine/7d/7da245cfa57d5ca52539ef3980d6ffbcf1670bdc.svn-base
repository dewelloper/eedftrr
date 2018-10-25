<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="Atlas.Efes.Portal.CreateInvoice" %>

<%@ Import Namespace="Atlas.Efes.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            var uuid = getParameterValues('UUID');

            if (!isNullOrEmpty(uuid)) {
                getInvoiceByUUID(uuid);
            }
            else {
                setValue("txtETTN", '<%=ETTN%>');
                setValue("txtInvoiceNumber", '<%=InvoiceNumber%>');
            }

            appBaseLoad();
            appEditLoad();

            $("#txtPhone").mask('0(999)-999-9999');
            $("#txtTelefax").mask('0(999)-999-9999');
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab_1" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-file-code-o"></i>Fatura Bilgileri</a></li>
                <li class=""><a href="#tab_2" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-list-alt"></i>Sipariş Bilgileri</a></li>
                <li class=""><a href="#tab_3" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-credit-card"></i>Ödeme Bilgileri</a></li>
                <li class=""><a href="#tab_3" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-file-zip-o"></i>Belgeler</a></li>
            </ul>
            <div class="tab-content" style="background: #f2f2f2">
                <div class="tab-pane active" id="tab_1">
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <i class="fa fa-fw fa-file-code-o"></i>
                                    <h3 class="box-title">Genel Fatura Bilgileri</h3>

                                    <div class="box-tools pull-right">
                                        <a class="pull-right btn btn-default" id="btnSave">Kaydet<i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ETTN</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" id="txtETTN" disabled="disabled" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Date %></label>
                                                    <div class="col-sm-9">
                                                        <input type="datetime" value="<%=DateTime.Now.ToString("dd.MM.yyyy") %>" class="form-control" id="txtInvoiceDate" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Number %></label>
                                                    <div class="col-sm-9">
                                                        <input type="text" class="form-control" id="txtInvoiceNumber" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Scenario %></label>
                                                    <div class="col-sm-9">
                                                        <select class="form-control" id="invoiceScenarios">
                                                            <option value="TEMELFATURA">TEMEL FATURA</option>
                                                            <option value="TICARIFATURA">TICARI FATURA</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6" style="border-left: 1px solid #E2D5D5">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Custom_Id %></label>
                                                    <div class="col-sm-9">
                                                        <input type="text" class="form-control" id="txtCustomId" value="TR1.2" disabled="disabled" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Type %></label>
                                                    <div class="col-sm-9">
                                                        <select class="form-control" id="invoiceStatus">
                                                            <option value="SATIS">SATIS</option>
                                                            <option value="IADE">IADE</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Currency %></label>
                                                    <div class="col-sm-9">
                                                        <select class="form-control" id="exchangeRates">
                                                            <option value="TRY">TURK LIRASI</option>
                                                            <option value="EUR">AVRO</option>
                                                            <option value="USD">DOLAR</option>
                                                            <option value="GBP">POUND</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label"><%=Languages.Invoice_Exchange_Rate %></label>
                                                    <div class="col-sm-9">
                                                        <input type="text" class="form-control" onkeypress="return isNumberKey(event);" value="0" id="txtExchangeRate" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-danger">
                                <div class="box-header with-border">
                                    <i class="fa fa-fw fa-envelope"></i>
                                    <h3 class="box-title">Fatura Alıcı Bilgileri</h3>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Alias</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" style="background-color: #E1F1FF" placeholder="Ünvana Göre Alıcı Arayınız" id="txtAlias" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">VKN/TCKN</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" maxlength="11" id="txtIdentification" />
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Ad</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" id="txtName" />
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Soyad</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" id="txtSurname" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Sicil No</label>
                                                    <div class="col-sm-9">
                                                        <input class="form-control" id="txtCommercialNumber" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6" style="border-left: 1px solid #E2D5D5">
                                        <div class="form-horizontal">
                                            <div class="box-body">

                                                <div class="row">
                                                    <div class="col-md-6">

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Ulke</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="countries" />
                                                                <input type="hidden" value="0" id="countryID" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label" for="cities">Şehir</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" name="cities" class="form-control" id="cities" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Telefon</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="txtPhone" />
                                                            </div>
                                                        </div>


                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Fax</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="txtTelefax" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Email</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="txtEmail" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Website</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="txtWebsite" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Vergi Dairesi</label>
                                                            <div class="col-sm-8">
                                                                <input type="text" class="form-control" id="txtTaxOffice" />
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Cadde</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtStreet" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Mahalle/Ilce</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtQuarter" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Bina Adı</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtBuildingName" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Bina No</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtBuildingNo" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Kapı No</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtDoorNumber" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Kasaba / Köy</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtTown" />
                                                            </div>
                                                        </div>


                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Posta Kodu</label>
                                                            <div class="col-sm-8">
                                                                <input class="form-control" id="txtPostalCode" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box">
                                <div class="box-header">
                                    <i class="fa fa-fw fa-truck"></i>
                                    <h3 class="box-title">Hizmet Bilgisi</h3>
                                    <div class="pull-right box-tools">

                                        <button class="btn btn-default btn-sm" id="editButton" onclick="editSelectedInvoiceLine();return false;"><i class="fa fa-fw fa-edit"></i></button>
                                        <button class="btn btn-default btn-sm" id="removeButton" onclick="removeSelectedInvoiceLine();return false;"><i class="fa fa-times"></i></button>
                                        <button class="btn btn-default btn-sm" id="newButton">
                                            <i class="fa fa-fw fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <table id="invoiceLines" class="table table-bordered">
                                        <thead>
                                            <tr role="row">
                                                <th>#</th>
                                                <th>Mal-Hizmet</th>
                                                <th>Birim</th>
                                                <th>Miktar</th>
                                                <th>Birim Fiyat</th>
                                                <th>Hizmet Tutarı</th>
                                                <th>KDV Oranı %</th>
                                                <th>KDV Tutarı</th>
                                                <th>Iskonto Oranı %</th>
                                                <th>Iskonto Tutarı</th>
                                            </tr>
                                        </thead>
                                        <tbody id="invoiceLineBody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <i class="fa fa-fw fa-money"></i>
                                    <h3 class="box-title">Genel Toplam Bilgileri</h3>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-horizontal">
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">Toplam Ana Tutar</label>
                                                        <div class="col-sm-7">
                                                            <input class="form-control" id="txtLineExtensionAmount" disabled="disabled" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">İndirimli Toplam</label>
                                                        <div class="col-sm-7">
                                                            <input class="form-control" id="txtTaxExclusiveAmount" disabled="disabled" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">Iskonto Toplam</label>
                                                        <div class="col-sm-7">
                                                            <input class="form-control" id="txtAllowanceTotalAmount" disabled="disabled" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">Vergi Toplam</label>
                                                        <div class="col-sm-7">
                                                            <input class="form-control" id="txtTaxInclusiveAmount" disabled="disabled" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">Ödenecek Toplam</label>
                                                        <div class="col-sm-7">
                                                            <input class="form-control" id="txtTotalPaymentAmount" disabled="disabled" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane" id="tab_2">
                </div>

                <div class="tab-pane" id="tab_3">
                </div>


                <%--AddInvoiceLine--%>
                <div class="modal fade" id="invoiceLineModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="top: 50px; position: relative">
                            <div class="modal-header" style="background-color: #E8E5E1">
                                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                <h4 class="modal-title" id="myModalLabel">Hizmet Ekle</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_general" data-toggle="tab" aria-expanded="true">GENEL</a></li>
                                            <li class=""><a href="#tab_Tax" data-toggle="tab" aria-expanded="false">VERGILER</a></li>
                                            <li class=""><a href="#tab_Other" data-toggle="tab" aria-expanded="false">DIGER</a></li>
                                        </ul>
                                        <input type="hidden" id="popupLoadMode" value="New" />
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_general">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Hizmet</label>
                                                                    <div class="col-sm-8">
                                                                        <input class="form-control" required="required" id="txtItemName" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Birim</label>
                                                                    <div class="col-sm-8">
                                                                        <select class="form-control" id="unitCodes">
                                                                        </select>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Iskonto Oranı %</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" class="form-control" value="0" id="txtFactorNumeric" />
                                                                    </div>
                                                                </div>


                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">KDV Oranı %</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" class="form-control" value="18.00" id="txtTaxPercent" />
                                                                    </div>
                                                                </div>


                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Muafiyet Nedeni</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" class="form-control" disabled="disabled" id="txtTaxReason" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Miktar</label>
                                                                    <div class="col-sm-8">
                                                                        <input class="form-control" onkeydown="return onlyNumber(event);" id="txtQuantity" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Birim Fiyat</label>
                                                                    <div class="col-sm-8">
                                                                        <input class="form-control" id="txtBaseAmount" onkeypress="return isNumberKey(event);" value="0.00" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Iskonto Tutarı</label>
                                                                    <div class="col-sm-8">
                                                                        <input class="form-control" id="txtFactorAmount" value="0" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">KDV Tutarı</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" class="form-control" onkeypress="return isNumberKey(event);" id="txtTaxAmount" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Hizmet Tutarı</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" class="form-control" value="0" disabled="disabled" id="txtInvoiceLineAmount" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_Tax">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">Vergi Türü</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="text" disabled="disabled" class="form-control" value="0015-Gercek Usulde Katma Deger" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label class="col-sm-7 control-label">Vergi Oranı%</label>
                                                                    <div class="col-sm-5">
                                                                        <input type="text" class="form-control" value="18.00" disabled="disabled" id="txtInvoiceTaxPercent" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-3">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label class="col-sm-7 control-label">Vergi Tutarı</label>
                                                                    <div class="col-sm-5">
                                                                        <input type="text" class="form-control" value="0.00" disabled="disabled" id="txtInvoiceTaxAmount" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_Other">
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                                <button type="button" class="btn btn-primary" id="btnSaveInvoiceLine">Kaydet</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
