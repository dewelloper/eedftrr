using Atlas.Efes.Common.GibModel;
using Atlas.Efes.Common.Model;
using Atlas.Efes.DataContext;
using Atlas.Efes.Portal.Constants;
using Atlas.Efes.Portal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Atlas.Efes.Portal;
using Atlas.Efes.Core.Extensions;
using Atlas.Efes.Common.Container;
using System.Xml;
using System.Text;
using System.IO;
using System.Globalization;
namespace Atlas.Efes.Portal
{
    public partial class GenericHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Clear();



                Response.End();
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static CityInfo[] GetCities()
        {
            List<CityInfo> cities = null;

            cities = CacheFabricHelper.Get<List<CityInfo>>(LifeTimeKeys.CITY_CACHE_KEY);

            if (cities == null)
            {
                cities = LookupDataContext.Instance.GetCities();
                CacheFabricHelper.Add(LifeTimeKeys.CITY_CACHE_KEY, cities);
            }

            return cities.ToArray();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static LookupInfo[] GetUnitCodes()
        {
            List<LookupInfo> unitCodes = null;

            unitCodes = CacheFabricHelper.Get<List<LookupInfo>>(LifeTimeKeys.UNIT_CODES);

            if (unitCodes == null)
            {
                unitCodes = LookupDataContext.Instance.GetLookupByKey("UnitCodes").Result;
                CacheFabricHelper.Add(LifeTimeKeys.UNIT_CODES, unitCodes);
            }

            return unitCodes.ToArray();
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static LookupInfo[] GetTaxTypes()
        {
            List<LookupInfo> taxTypes = null;
            taxTypes = CacheFabricHelper.Get<List<LookupInfo>>(LifeTimeKeys.TAX_TYPES);

            if (taxTypes == null)
            {
                taxTypes = LookupDataContext.Instance.GetLookupByKey("Taxes").Result;
                CacheFabricHelper.Add(LifeTimeKeys.TAX_TYPES, taxTypes);
            }

            return taxTypes.ToArray();
        }




        [WebMethod]
        public static List<LookupInfo> LoadInvoiceType()
        {
            ResponseService<List<LookupInfo>> response = LookupDataContext.Instance.GetLookupByKey("Invoice_Type");

            if (!response.HasError)
            {
                return response.Result;
            }
            else
            {
                return null;
            }
        }



        [WebMethod]
        public static List<LookupInfo> LoadInvoiceExchangeRate()
        {
            ResponseService<List<LookupInfo>> response = LookupDataContext.Instance.GetLookupByKey("ExchangeRate");

            if (!response.HasError)
            {
                return response.Result;
            }
            else
            {
                return null;
            }
        }


        [WebMethod]
        public static List<LookupInfo> LoadInvoiceStatus()
        {
            ResponseService<List<LookupInfo>> response = LookupDataContext.Instance.GetLookupByKey("Invoice_Status");

            if (!response.HasError)
            {
                return response.Result;
            }
            else
            {
                return null;
            }
        }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static CountryInfo[] GetCountries()
        {
            List<CountryInfo> countries = null;

            countries = CacheFabricHelper.Get<List<CountryInfo>>(LifeTimeKeys.COUNTRY_CACHE_KEY);

            if (countries == null)
            {
                countries = LookupDataContext.Instance.GetCountries();
                CacheFabricHelper.Add(LifeTimeKeys.COUNTRY_CACHE_KEY, countries);
            }

            return countries.ToArray();
        }


        [WebMethod]
        [ScriptMethod]
        public static EInvoiceUserInfo[] GetEInvoiceUsers()
        {
            List<EInvoiceUserInfo> userList = null;

            userList = CacheFabricHelper.Get<List<EInvoiceUserInfo>>(LifeTimeKeys.EINVOICE_USERS);

            if (userList == null)
            {
                //userList = LookupDataContext.Instance.GetCountries();
                userList = new List<EInvoiceUserInfo>();
                XmlDocument document = new XmlDocument();
                string path = AppDomain.CurrentDomain.BaseDirectory + "bin" + @"\" + "userList.xml";
                string xml = File.ReadAllText(path);
                string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (xml.StartsWith(_byteOrderMarkUtf8))
                {
                    xml = xml.Remove(0, _byteOrderMarkUtf8.Length);
                }

                document.LoadXml(xml.Substring(xml.IndexOf(Environment.NewLine)));

                XmlNodeList nodeList = document.SelectNodes("UserList//User");

                foreach (XmlNode item in nodeList)
                {
                    var f = item.ChildNodes[0];

                    EInvoiceUserInfo userInfo = new EInvoiceUserInfo();
                    userInfo.Identifier = item.ChildNodes[0].InnerText;
                    userInfo.Alias = item.ChildNodes[1].InnerText;
                    userInfo.Name = item.ChildNodes[2].InnerText;
                    userInfo.Type = item.ChildNodes[3].InnerText;

                    userList.Add(userInfo);
                }

                CacheFabricHelper.Add(LifeTimeKeys.EINVOICE_USERS, userList);


            }

            return userList.ToArray();
        }

        [WebMethod]
        public static CreateInvoiceResult SaveInvoice(Dictionary<string, string> invoiceParameters,
                                                      Dictionary<string, string> customerParameters,
                                                      object invoiceLineItems,
                                                      Dictionary<string, string> legalMonetaryTotals)
        {
            InvoiceInfo invoice = new InvoiceInfo();
            invoice.UUID = invoiceParameters.GetValueByKey("UUID");
            invoice.ProfileID = invoiceParameters.GetValueByKey("ProfileID").ToCombineString();
            invoice.CopyIndicator = bool.Parse(invoiceParameters.GetValueByKey("CopyIndicator"));
            invoice.CustomizationID = invoiceParameters.GetValueByKey("CustomizationID");
            invoice.ID = invoiceParameters.GetValueByKey("ID");
            invoice.IssueDate = invoiceParameters.GetValueByKey("IssueDate").ToGibDocumentDate();
            invoice.LineCountNumeric = int.Parse(invoiceParameters.GetValueByKey("LineCountNumeric"));
            invoice.CustomizationID = invoiceParameters.GetValueByKey("CustomizationID");
            invoice.UBLVersionID = invoiceParameters.GetValueByKey("UBLVersionID");

            string documentCurrency = invoiceParameters.GetValueByKey("DocumentCurrencyCode");
            decimal exchangeRate = invoiceParameters.GetValueByKey("ExchangeRate").ToDecimal();


            invoice.DocumentCurrencyCode = new DocumentCurrencyCodeInfo();
            invoice.DocumentCurrencyCode.Value = documentCurrency;
            invoice.PaymentCurrencyCode = documentCurrency;
            invoice.PricingCurrencyCode = documentCurrency;
            invoice.PaymentAlternativeCurrencyCode = documentCurrency;
            invoice.TaxCurrencyCode = documentCurrency;


            invoice.PaymentTerms = new PaymentTermsInfo
            {
                Note = string.Empty,
                Amount = new AmountContainerInfo()
                {
                    currencyID = documentCurrency,
                    Value = 0,
                },
                PenaltySurchargePercent = 0,
            };


            invoice.PaymentExchangeRate = new PaymentExchangeRateInfo
            {
                CalculationRate = exchangeRate,
                IssueDate = invoice.IssueDate.ToGibDocumentDate(),
                SourceCurrencyCode = documentCurrency,
                TargetCurrencyCode = "TRY",
            };

            invoice.PaymentAlternativeExchangeRate = new PaymentAlternativeExchangeRateInfo
            {
                CalculationRate = exchangeRate,
                IssueDate = invoice.IssueDate.ToGibDocumentDate(),
                SourceCurrencyCode = documentCurrency,
                TargetCurrencyCode = "TRY",
            };

            invoice.TaxExchangeRate = new TaxExchangeRateInfo
            {
                CalculationRate = exchangeRate,
                IssueDate = invoice.IssueDate.ToGibDocumentDate(),
                SourceCurrencyCode = documentCurrency,
                TargetCurrencyCode = "TRY",
            };

            invoice.PricingExchangeRate = new PriceExchangeRateInfo
            {
                CalculationRate = exchangeRate,
                IssueDate = invoice.IssueDate.ToGibDocumentDate(),
                SourceCurrencyCode = documentCurrency,
                TargetCurrencyCode = "TRY",
            };

            invoice.AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo();
            invoice.AccountingCustomerPartyInfo.Party = new PartyInfo();
            invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();

            string schemaID = customerParameters.GetValueByKey("SchemaID");
            string name = customerParameters.GetValueByKey("Name");
            string surname = customerParameters.GetValueByKey("Surname");

            if (schemaID == "VKN")
            {
                invoice.AccountingCustomerPartyInfo.Party.PartyName = new PartyNameInfo();
                invoice.AccountingCustomerPartyInfo.Party.PartyName.Name = name;
            }

            if (schemaID == "TCKN")
            {
                invoice.AccountingCustomerPartyInfo.Party.PartyName = new PartyNameInfo();
                invoice.AccountingCustomerPartyInfo.Party.PartyName.Name = string.Empty;

                invoice.AccountingCustomerPartyInfo.Party.Person = new PersonInfo();
                invoice.AccountingCustomerPartyInfo.Party.Person.FirstName = name;
                invoice.AccountingCustomerPartyInfo.Party.Person.FamilyName = surname;
            }

            PartyIdentificationInfo partyIdentification = new PartyIdentificationInfo();
            partyIdentification.ID = new IDContainerInfo();
            partyIdentification.ID.schemeID = customerParameters.GetValueByKey("SchemaID");
            partyIdentification.ID.Value = customerParameters.GetValueByKey("VKN_TCKN");

            invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.Add(partyIdentification);
            string commercialNumber = customerParameters.GetValueByKey("CommercialNumber");

            if (!string.IsNullOrEmpty(commercialNumber))
            {
                partyIdentification = new PartyIdentificationInfo();
                partyIdentification.ID = new IDContainerInfo();
                partyIdentification.ID.schemeID = "TICARETSICILNO";
                partyIdentification.ID.Value = commercialNumber;
                invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.Add(partyIdentification);
            }

            string country = customerParameters.GetValueByKey("Country");
            string city = customerParameters.GetValueByKey("City");
            string phone = customerParameters.GetValueByKey("Phone");
            string email = customerParameters.GetValueByKey("Email");
            string telefax = customerParameters.GetValueByKey("Telefax");

            if (!string.IsNullOrEmpty(telefax))
            {
                telefax = telefax.ToFixPhone();
            }

            if (!string.IsNullOrEmpty(phone))
            {
                phone = phone.ToFixPhone();
            }

            string street = customerParameters.GetValueByKey("Street");
            string quarter = customerParameters.GetValueByKey("Quarter");
            string websiteUri = customerParameters.GetValueByKey("WebsiteURI");
            string buildingName = customerParameters.GetValueByKey("BuildingName");
            string buildingNumber = customerParameters.GetValueByKey("BuildingNumber");
            string doorNumber = customerParameters.GetValueByKey("DoorNumber");
            string postalZone = customerParameters.GetValueByKey("PostalZone");
            string alias = customerParameters.GetValueByKey("Alias");
            string town = customerParameters.GetValueByKey("Town");
            string taxOffice = customerParameters.GetValueByKey("TaxOffice");

            invoice.AccountingCustomerPartyInfo.Party.PostalAddress = new PostalAddressInfo();
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country = new CountryIndicatorInfo();
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country.Name = country;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CityName = city;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.StreetName = street;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName = quarter;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName = buildingName;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber = buildingNumber;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.PostalZone = postalZone;
            invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Room = doorNumber;
            invoice.AccountingCustomerPartyInfo.Party.Contact = new ContactIndicatorInfo()
            {
                ElectronicMail = email,
                Telefax = telefax,
                Telephone = phone,
            };

            invoice.AccountingCustomerPartyInfo.Party.WebsiteURI = websiteUri;

            invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme = new PartyTaxSchemeInfo()
            {
                TaxScheme = new TaxSchemeInfo()
                {
                    Name = taxOffice,
                }
            };

            object[] invoiceLines = invoiceLineItems as object[];
            List<object> invoiceList = invoiceLines.ToList();

            if (invoiceList.Count > 0)
            {
                int counter = 0;
                List<InvoiceTaxIndicatorInfo> taxIndicators = new List<InvoiceTaxIndicatorInfo>();

                foreach (var item in invoiceList)
                {
                    counter = counter + 1;
                    Dictionary<string, object> dicItem = (Dictionary<string, object>)item;

                    if (invoice.InvoiceLineInfos == null)
                    {
                        invoice.InvoiceLineInfos = new List<InvoiceLineInfo>();
                    }

                    InvoiceLineInfo invoiceLineInfo = new InvoiceLineInfo();
                    invoiceLineInfo.ID = counter.ToString();
                    invoiceLineInfo.InvoicedQuantity = new UnitCodeContainerInfo()
                    {
                        unitCode = dicItem.GetValueByKey("UnitCode"),
                        Value = dicItem.GetValueByKey("Quantity").ToInt(),
                    };
                    invoiceLineInfo.Item = new ItemInfo()
                    {
                        Name = dicItem.GetValueByKey("ItemName"),
                    };

                    invoiceLineInfo.LineExtensionAmount = new AmountContainerInfo()
                    {
                        Value = dicItem.GetValueByKey("InvoiceLineAmount").ToDecimal(),
                    };

                    invoiceLineInfo.TaxTotal = new TaxTotalInfo()
                    {
                        TaxAmount = new AmountContainerInfo()
                        {
                            Value = dicItem.GetValueByKey("TaxAmount").ToDecimal(),
                        },
                    };

                    decimal allowance = dicItem.GetValueByKey("FactorNumeric").ToDecimal();
                    invoiceLineInfo.AllowanceChargeInfo = new AllowanceChargeInfo();

                    if (allowance == 0)
                    {
                        invoiceLineInfo.AllowanceChargeInfo.ChargeIndicator = false;
                        invoiceLineInfo.AllowanceChargeInfo.AllowanceChargeReason = string.Empty;
                        invoiceLineInfo.AllowanceChargeInfo.MultiplierFactorNumeric = dicItem.GetValueByKey("FactorNumeric").ToDecimal();
                        invoiceLineInfo.AllowanceChargeInfo.Amount = new AmountContainerInfo()
                        {
                            Value = 0,
                        };
                    }

                    object taxItems = dicItem.Where(f => f.Key == "taxItems").FirstOrDefault().Value;
                    object[] taxItemLines = taxItems as object[];

                    foreach (var taxItem in taxItemLines.ToList())
                    {
                        Dictionary<string, object> dicTaxItem = taxItem as Dictionary<string, object>;

                        invoiceLineInfo.TaxTotal = new TaxTotalInfo()
                        {
                            TaxAmount = new AmountContainerInfo()
                            {
                                Value = dicTaxItem.GetValueByKey("TaxItemAmount").ToDecimal(),
                            },
                        };

                        if (invoiceLineInfo.TaxTotal.TaxSubTotals == null)
                        {
                            invoiceLineInfo.TaxTotal.TaxSubTotals = new List<TaxSubTotalInfo>();
                        }

                        TaxSubTotalInfo taxSubtotal = new TaxSubTotalInfo()
                        {
                            Percent = dicTaxItem.GetValueByKey("TaxItemPercent").ToDecimal(),
                            TaxAmount = new AmountContainerInfo()
                            {
                                Value = dicTaxItem.GetValueByKey("TaxItemAmount").ToDecimal(),
                            },
                            TaxCategory = new TaxCategoryInfo()
                            {
                                TaxScheme = new TaxSchemeInfo()
                                {
                                    Name = dicTaxItem.GetValueByKey("TaxItemName"),
                                    TaxTypeCode = dicTaxItem.GetValueByKey("TaxItemCode"),
                                }
                            }
                        };

                        taxIndicators.Add(new InvoiceTaxIndicatorInfo()
                        {
                            Percent = taxSubtotal.Percent,
                            TaxItemAmount = taxSubtotal.TaxAmount.Value,
                            TaxItemCode = taxSubtotal.TaxCategory.TaxScheme.TaxTypeCode,
                            TaxItemName = taxSubtotal.TaxCategory.TaxScheme.Name,
                            Prefix = string.Format("{0}_{1}", taxSubtotal.Percent, taxSubtotal.TaxCategory.TaxScheme.TaxTypeCode),
                        });

                        invoiceLineInfo.TaxTotal.TaxSubTotals.Add(taxSubtotal);
                    }

                    invoiceLineInfo.PriceInfo = new PriceInfo()
                    {
                        PriceAmount = new AmountContainerInfo()
                        {
                            Value = dicItem.GetValueByKey("BaseAmount").ToDecimal(),
                        }
                    };

                    invoice.InvoiceLineInfos.Add(invoiceLineInfo);
                }

                invoice.LegalMonetaryTotal = new LegalMonetaryTotalInfo()
                {
                    AllowanceTotalAmount = new AmountContainerInfo()
                    {
                        Value = legalMonetaryTotals.GetValueByKey("AllowanceTotalAmount").ToDecimal(),
                    },
                    LineExtensionAmount = new AmountContainerInfo()
                    {
                        Value = legalMonetaryTotals.GetValueByKey("LineExtensionAmount").ToDecimal(),
                    },
                    PayableAmount = new AmountContainerInfo()
                    {
                        Value = legalMonetaryTotals.GetValueByKey("PayableAmount").ToDecimal(),
                    },
                    TaxInclusiveAmount = new AmountContainerInfo()
                    {
                        Value = legalMonetaryTotals.GetValueByKey("TaxInclusiveAmount").ToDecimal(),
                    },
                    TaxExclusiveAmount = new AmountContainerInfo()
                    {
                        Value = legalMonetaryTotals.GetValueByKey("TaxExclusiveAmount").ToDecimal(),
                    }
                };

                //InvoiceTaxTotal Calculating.....

                if (taxIndicators.Count > 0)
                {
                    invoice.TaxTotalInfo = new TaxTotalInfo()
                    {
                        TaxAmount = new AmountContainerInfo()
                        {
                            Value = taxIndicators.Sum(f => f.TaxItemAmount),
                        },
                    };

                    if (invoice.TaxTotalInfo.TaxSubTotals == null)
                    {
                        invoice.TaxTotalInfo.TaxSubTotals = new List<TaxSubTotalInfo>();
                    }

                    List<IGrouping<string, InvoiceTaxIndicatorInfo>> groups = taxIndicators.GroupBy(f => f.Prefix).ToList();

                    foreach (var item in groups)
                    {
                        var groupItem = item.FirstOrDefault();

                        TaxSubTotalInfo info = new TaxSubTotalInfo
                        {
                            Percent = groupItem.Percent,
                            TaxAmount = new AmountContainerInfo()
                            {
                                Value = groupItem.TaxItemAmount,
                            },
                            TaxCategory = new TaxCategoryInfo()
                            {
                                TaxScheme = new TaxSchemeInfo()
                                {
                                    Name = groupItem.TaxItemName,
                                    TaxTypeCode = groupItem.TaxItemCode,
                                }
                            }
                        };

                        invoice.TaxTotalInfo.TaxSubTotals.Add(info);
                    }
                }
            }

            UserInfo userInfo = SessionHelper.GetFromSession<UserInfo>(LifeTimeKeys.User_Information);
            CreateInvoiceResult result = InvoiceDataContext.Instance.CreateInvoice(invoice, userInfo);

            if (result.IsSuccess)
            {
                result.Message = Atlas.Efes.Resources.ErrorMessages.CREATE_INVOICE_SUCCESS;
            }

            return result;

        }

        [WebMethod]
        public static ResponseService<UserInfo> LoginUser(Dictionary<string, string> parameters)
        {
            string username = parameters.GetValueByKey("Username");
            string password = parameters.GetValueByKey("Password");

            ResponseService<UserInfo> response = UserDataContext.Instance.Login(username, password);

            if (!response.HasError)
            {
                SessionHelper.AddToSession(LifeTimeKeys.User_Information, response.Result);
            }


            return response;
        }

        [WebMethod]
        public static ResponseService<InvoiceInfo> GetInvoiceByUUID(Dictionary<string, string> invoiceHeaders)
        {
            string uuid = invoiceHeaders.GetValueByKey("UUID");
            return InvoiceDataContext.Instance.GetInvoiceByUUID(uuid);
        }


        [WebMethod]
        public static CreateInvoiceResult CreateInvoiceVS2(InvoiceInfo invoice)
        {
            UserInfo userInfo = SessionHelper.GetFromSession<UserInfo>(LifeTimeKeys.User_Information);
            CreateInvoiceResult result = InvoiceDataContext.Instance.CreateInvoice(invoice, userInfo);
            return result;
        }
    }
}