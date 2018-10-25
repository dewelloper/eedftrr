using Atlas.Efes.Common.GibModel;
using Atlas.Efes.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atlas.Efes.OracleClient;
using Atlas.Efes.OracleClient.OracleServiceProxy;
using System.Data;
using Atlas.Efes.Core.Extensions;
using Atlas.Efes.Common.Container;

namespace Atlas.Efes.DataContext
{
    public class InvoiceDataContext : BaseDataContext
    {

        private static readonly InvoiceDataContext _instance = new InvoiceDataContext();

        public static InvoiceDataContext Instance
        {
            get
            {
                return _instance;
            }
        }
        public CreateInvoiceResult CreateInvoice(InvoiceInfo invoice, UserInfo userInfo)
        {
            CreateInvoiceResult result = new CreateInvoiceResult();

            StringBuilder builder = new StringBuilder();
            builder.Append("<Invoice>");

            builder.AppendFormat("<InvoiceID>{0}</InvoiceID>", invoice.DocumentID == null ? "0" : invoice.DocumentID);
            builder.AppendFormat("<UBLVersionID>{0}</UBLVersionID>", invoice.UBLVersionID);
            builder.AppendFormat("<CustomizationID>{0}</CustomizationID>", invoice.CustomizationID);
            builder.AppendFormat("<CopyIndicator>{0}</CopyIndicator>", invoice.CopyIndicator == false ? 0 : 1);
            builder.AppendFormat("<UUID>{0}</UUID>", invoice.UUID);
            builder.AppendFormat("<ProfileID>{0}</ProfileID>", invoice.ProfileID);
            builder.AppendFormat("<ID>{0}</ID>", invoice.ID);
            builder.AppendFormat("<IssueDate>{0}</IssueDate>", invoice.IssueDate);
            builder.AppendFormat("<IssueTime>{0}</IssueTime>", invoice.IssueTime);
            builder.AppendFormat("<InvoiceTypeCode>{0}</InvoiceTypeCode>", invoice.InvoiceTypeCode);
            builder.AppendFormat("<DocumentCurrencyCode>{0}</DocumentCurrencyCode>", invoice.DocumentCurrencyCode.Value);
            builder.AppendFormat("<TaxCurrencyCode>{0}</TaxCurrencyCode>", invoice.TaxCurrencyCode);
            builder.AppendFormat("<PricingCurrencyCode>{0}</PricingCurrencyCode>", invoice.PricingCurrencyCode);
            builder.AppendFormat("<PaymentCurrencyCode>{0}</PaymentCurrencyCode>", invoice.PaymentCurrencyCode);
            builder.AppendFormat("<PaymentAlternativeCurrencyCode>{0}</PaymentAlternativeCurrencyCode>", invoice.PaymentAlternativeCurrencyCode);


            //AccountSupplierParty....
            ResponseService<CustomerInfo> getCustomerResponse = CustomerDataContext.Instance.GetCustomerByHeader(userInfo.Username, userInfo.Password);

            if (getCustomerResponse.HasError)
            {
                result.Message = getCustomerResponse.Message;
                result.IsSuccess = false;
                return result;
            }

            CustomerInfo customerInfo = getCustomerResponse.Result;

            builder.AppendFormat("<UserInfo>");
            builder.AppendFormat("<UserID>{0}</UserID>", userInfo.UserID);
            builder.AppendFormat("<CustomerID>{0}</CustomerID>", customerInfo.ID);
            builder.AppendFormat("</UserInfo>");


            invoice.AccountingSupplierPartyInfo = new AccountingSupplierPartyInfo();
            invoice.AccountingSupplierPartyInfo.Party = new PartyInfo();
            invoice.AccountingSupplierPartyInfo.Party.PartyName = new PartyNameInfo();
            invoice.AccountingSupplierPartyInfo.Party.PartyName.Name = customerInfo.Title;
            invoice.AccountingSupplierPartyInfo.Party.PartyTaxScheme = new PartyTaxSchemeInfo();
            invoice.AccountingSupplierPartyInfo.Party.PartyTaxScheme.TaxScheme = new TaxSchemeInfo()
            {
                Name = customerInfo.TaxOffice,
            };
            invoice.AccountingSupplierPartyInfo.Party.WebsiteURI = customerInfo.WebsiteURI;
            invoice.AccountingSupplierPartyInfo.Party.PostalAddress = new PostalAddressInfo()
            {
                BuildingName = customerInfo.AddressInfo.BuildingName,
                BuildingNumber = customerInfo.AddressInfo.BuildingNumber,
                CityName = customerInfo.AddressInfo.City,
                Country = new CountryIndicatorInfo()
                {
                    Name = customerInfo.AddressInfo.Country,
                },
                CitySubdivisionName = customerInfo.AddressInfo.District,
                PostalZone = customerInfo.AddressInfo.PostalCode,
                Region = customerInfo.AddressInfo.Region,
                Room = customerInfo.AddressInfo.Room,
                StreetName = customerInfo.AddressInfo.Street,
            };

            invoice.AccountingSupplierPartyInfo.Party.Contact = new ContactIndicatorInfo()
            {
                ElectronicMail = customerInfo.ContactInfo.ElectronicMail,
                Telefax = customerInfo.ContactInfo.Telefax,
                Telephone = customerInfo.ContactInfo.Telephone,
            };

            if (!string.IsNullOrEmpty(customerInfo.Identification))
            {
                if (invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications == null)
                {
                    invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();
                }

                invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications.Add(new PartyIdentificationInfo()
                {
                    ID = new IDContainerInfo()
                    {
                        Value = customerInfo.Identification,
                        schemeID = customerInfo.CustomerType == CustomerTypeInfo.VKN ? "VKN" : "TCKN",
                    }
                });
                invoice.AccountingSupplierPartyInfo.Party.Alias = invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications[0].ID.Value;
            }

            if (!string.IsNullOrEmpty(customerInfo.MersisNo))
            {
                if (invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications == null)
                {
                    invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();
                }

                invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications.Add(new PartyIdentificationInfo()
                {
                    ID = new IDContainerInfo()
                    {
                        Value = customerInfo.MersisNo,
                        schemeID = "MERSISNO"
                    }
                });
            }

            if (!string.IsNullOrEmpty(customerInfo.CommercialNumber))
            {
                if (invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications == null)
                {
                    invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();
                }

                invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications.Add(new PartyIdentificationInfo()
                {
                    ID = new IDContainerInfo()
                    {
                        Value = customerInfo.CommercialNumber,
                        schemeID = "TICARETSICILNO"
                    }
                });
            }

            invoice.AccountingSupplierPartyInfo.Party.Person = new PersonInfo();
            invoice.AccountingSupplierPartyInfo.Party.Person.FirstName = string.Empty;
            invoice.AccountingSupplierPartyInfo.Party.Person.FamilyName = string.Empty;


            builder.AppendFormat("<AccountingSupplierParty>");

            builder.AppendFormat("<Alias>{0}</Alias>", invoice.AccountingSupplierPartyInfo.Party.Alias);
            builder.AppendFormat("<WebsiteURI>{0}</WebsiteURI>", invoice.AccountingSupplierPartyInfo.Party.WebsiteURI);
            builder.AppendFormat("<PartyName>{0}</PartyName>", invoice.AccountingSupplierPartyInfo.Party.PartyName.Name);
            builder.AppendFormat("<CountryName>{0}</CountryName>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.Country.Name);
            builder.AppendFormat("<Room>{0}</Room>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.Room);
            builder.AppendFormat("<StreetName>{0}</StreetName>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.StreetName);
            builder.AppendFormat("<BuildingName>{0}</BuildingName>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingName);
            builder.AppendFormat("<BuildingNumber>{0}</BuildingNumber>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingNumber);
            builder.AppendFormat("<CitySubdivisionName>{0}</CitySubdivisionName>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.CitySubdivisionName);
            builder.AppendFormat("<CityName>{0}</CityName>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.CityName);
            builder.AppendFormat("<SchemeName>{0}</SchemeName>", invoice.AccountingSupplierPartyInfo.Party.PartyTaxScheme.TaxScheme.Name);
            builder.AppendFormat("<Telephone>{0}</Telephone>", invoice.AccountingSupplierPartyInfo.Party.Contact.Telephone);
            builder.AppendFormat("<ElectronicMail>{0}</ElectronicMail>", invoice.AccountingSupplierPartyInfo.Party.Contact.ElectronicMail);
            builder.AppendFormat("<Telefax>{0}</Telefax>", invoice.AccountingSupplierPartyInfo.Party.Contact.Telefax);
            builder.AppendFormat("<PostalZone>{0}</PostalZone>", invoice.AccountingSupplierPartyInfo.Party.PostalAddress.PostalZone);
            builder.AppendFormat("<AccountType>{0}</AccountType>", invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications[0].ID.schemeID == "VKN" ? 1 : 2);
            builder.AppendFormat("<FirstName>{0}</FirstName>", invoice.AccountingSupplierPartyInfo.Party.Person.FirstName);
            builder.AppendFormat("<FamilyName>{0}</FamilyName>", invoice.AccountingSupplierPartyInfo.Party.Person.FamilyName);

            foreach (var item in invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications)
            {
                builder.AppendFormat("<PartyIdentification>");
                builder.AppendFormat("<ID>{0}</ID>", item.ID.Value);
                builder.AppendFormat("<SCHEMAID>{0}</SCHEMAID>", item.ID.schemeID);
                builder.AppendFormat("</PartyIdentification>");
            }

            builder.AppendFormat("</AccountingSupplierParty>");


            //Donttt Remember Here Honey....
            invoice.AccountingCustomerPartyInfo.Party.Person = new PersonInfo();
            invoice.AccountingCustomerPartyInfo.Party.Person.FirstName = string.Empty;
            invoice.AccountingCustomerPartyInfo.Party.Person.FamilyName = string.Empty;


            builder.AppendFormat("<AccountingCustomerParty>");

            if (!string.IsNullOrEmpty(invoice.AccountingCustomerPartyInfo.Party.Alias))
            {
                builder.AppendFormat("<Alias>{0}</Alias>", invoice.AccountingCustomerPartyInfo.Party.Alias);
            }
            else
            {
                //...GetAlias...
                builder.AppendFormat("<Alias>{0}</Alias>", invoice.AccountingCustomerPartyInfo.Party.Alias);
            }

            builder.AppendFormat("<WebsiteURI>{0}</WebsiteURI>", invoice.AccountingCustomerPartyInfo.Party.WebsiteURI);
            builder.AppendFormat("<PartyName>{0}</PartyName>", invoice.AccountingCustomerPartyInfo.Party.PartyName.Name);
            builder.AppendFormat("<CountryName>{0}</CountryName>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country.Name);
            builder.AppendFormat("<Room>{0}</Room>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Room);
            builder.AppendFormat("<StreetName>{0}</StreetName>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.StreetName);
            builder.AppendFormat("<BuildingName>{0}</BuildingName>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName);
            builder.AppendFormat("<BuildingNumber>{0}</BuildingNumber>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber);
            builder.AppendFormat("<CitySubdivisionName>{0}</CitySubdivisionName>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName);
            builder.AppendFormat("<CityName>{0}</CityName>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CityName);
            builder.AppendFormat("<SchemeName>{0}</SchemeName>", invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme.Name);
            builder.AppendFormat("<Telephone>{0}</Telephone>", invoice.AccountingCustomerPartyInfo.Party.Contact.Telephone);
            builder.AppendFormat("<ElectronicMail>{0}</ElectronicMail>", invoice.AccountingCustomerPartyInfo.Party.Contact.ElectronicMail);
            builder.AppendFormat("<Telefax>{0}</Telefax>", invoice.AccountingCustomerPartyInfo.Party.Contact.Telefax);
            builder.AppendFormat("<PostalZone>{0}</PostalZone>", invoice.AccountingCustomerPartyInfo.Party.PostalAddress.PostalZone);
            builder.AppendFormat("<AccountType>{0}</AccountType>", invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications[0].ID.schemeID == "VKN" ? 1 : 2);
            builder.AppendFormat("<FirstName>{0}</FirstName>", invoice.AccountingCustomerPartyInfo.Party.Person.FirstName);
            builder.AppendFormat("<FamilyName>{0}</FamilyName>", invoice.AccountingCustomerPartyInfo.Party.Person.FamilyName);

            foreach (var item in invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications)
            {
                builder.AppendFormat("<PartyIdentification>");
                builder.AppendFormat("<ID>{0}</ID>", item.ID.Value);
                builder.AppendFormat("<SCHEMAID>{0}</SCHEMAID>", item.ID.schemeID);
                builder.AppendFormat("</PartyIdentification>");
            }

            builder.AppendFormat("</AccountingCustomerParty>");


            if (invoice.PaymentTerms != null)
            {
                builder.AppendFormat("<PaymentTerms>");
                builder.AppendFormat("<Note>{0}</Note>", invoice.PaymentTerms.Note);
                builder.AppendFormat("<PenaltySurchargePercent>{0}</PenaltySurchargePercent>", invoice.PaymentTerms.PenaltySurchargePercent);
                builder.AppendFormat("<Amount>{0}</Amount>", invoice.PaymentTerms.Amount.Value.ToOracleDecimalFormat());
                builder.AppendFormat("</PaymentTerms>");
            }

            builder.AppendFormat("<PaymentExchangeRate>");
            builder.AppendFormat("<SourceCurrencyCode>{0}</SourceCurrencyCode>", invoice.PaymentExchangeRate.SourceCurrencyCode);
            builder.AppendFormat("<TargetCurrencyCode>{0}</TargetCurrencyCode>", invoice.PaymentExchangeRate.TargetCurrencyCode);
            builder.AppendFormat("<CalculationRate>{0}</CalculationRate>", invoice.PaymentExchangeRate.CalculationRate.ToOracleDecimalFormat());
            builder.AppendFormat("<Date>{0}</Date>", invoice.PaymentExchangeRate.IssueDate);
            builder.AppendFormat("</PaymentExchangeRate>");


            builder.AppendFormat("<TaxExchangeRate>");
            builder.AppendFormat("<SourceCurrencyCode>{0}</SourceCurrencyCode>", invoice.TaxExchangeRate.SourceCurrencyCode);
            builder.AppendFormat("<TargetCurrencyCode>{0}</TargetCurrencyCode>", invoice.TaxExchangeRate.TargetCurrencyCode);
            builder.AppendFormat("<CalculationRate>{0}</CalculationRate>", invoice.TaxExchangeRate.CalculationRate.ToOracleDecimalFormat());
            builder.AppendFormat("<Date>{0}</Date>", invoice.TaxExchangeRate.IssueDate);
            builder.AppendFormat("</TaxExchangeRate>");


            builder.AppendFormat("<PricingExchangeRate>");
            builder.AppendFormat("<SourceCurrencyCode>{0}</SourceCurrencyCode>", invoice.PricingExchangeRate.SourceCurrencyCode);
            builder.AppendFormat("<TargetCurrencyCode>{0}</TargetCurrencyCode>", invoice.PricingExchangeRate.TargetCurrencyCode);
            builder.AppendFormat("<CalculationRate>{0}</CalculationRate>", invoice.PricingExchangeRate.CalculationRate.ToOracleDecimalFormat());
            builder.AppendFormat("<Date>{0}</Date>", invoice.PricingExchangeRate.IssueDate);
            builder.AppendFormat("</PricingExchangeRate>");


            builder.AppendFormat("<PaymentAlternativeExchangeRate>");
            builder.AppendFormat("<SourceCurrencyCode>{0}</SourceCurrencyCode>", invoice.PaymentAlternativeExchangeRate.SourceCurrencyCode);
            builder.AppendFormat("<TargetCurrencyCode>{0}</TargetCurrencyCode>", invoice.PaymentAlternativeExchangeRate.TargetCurrencyCode);
            builder.AppendFormat("<CalculationRate>{0}</CalculationRate>", invoice.PaymentAlternativeExchangeRate.CalculationRate.ToOracleDecimalFormat());
            builder.AppendFormat("<Date>{0}</Date>", invoice.PaymentAlternativeExchangeRate.IssueDate);
            builder.AppendFormat("</PaymentAlternativeExchangeRate>");

            /*
             * Invoice_Tax_Total
             */


            if (invoice.TaxTotalInfo != null)
            {
                builder.Append("<TaxTotal>");
                builder.AppendFormat("<Amount>{0}</Amount>", invoice.TaxTotalInfo.TaxAmount.Value);

                foreach (TaxSubTotalInfo item in invoice.TaxTotalInfo.TaxSubTotals)
                {
                    builder.Append("<TaxSubTotal>");
                    builder.AppendFormat("<Amount>{0}</Amount>", item.TaxAmount.Value.ToOracleDecimalFormat());
                    builder.AppendFormat("<Percent>{0}</Percent>", item.Percent.ToOracleDecimalFormat());
                    builder.AppendFormat("<Name>{0}</Name>", item.TaxCategory.TaxScheme.Name);
                    builder.AppendFormat("<Code>{0}</Code>", item.TaxCategory.TaxScheme.TaxTypeCode);
                    builder.Append("</TaxSubTotal>");
                }

                builder.Append("</TaxTotal>");
            }


            /*
             * 
             *InvoiceLine..
             * 
             */

            if (invoice.InvoiceLineInfos != null && invoice.InvoiceLineInfos.Count > 0)
            {
                foreach (InvoiceLineInfo item in invoice.InvoiceLineInfos)
                {
                    builder.AppendFormat("<InvoiceLine>");
                    builder.AppendFormat("<LineID>{0}</LineID>", item.ID);
                    builder.AppendFormat("<InvoicedQuantity>{0}</InvoicedQuantity>", item.InvoicedQuantity.Value.ToOracleDecimalFormat());
                    builder.AppendFormat("<LineExtensionAmount>{0}</LineExtensionAmount>", item.LineExtensionAmount.Value.ToOracleDecimalFormat());
                    builder.AppendFormat("<ChargeIndicator>{0}</ChargeIndicator>", item.AllowanceChargeInfo.ChargeIndicator.BooleanToInt());
                    builder.AppendFormat("<AllowanceChargeReason>{0}</AllowanceChargeReason>", item.AllowanceChargeInfo.AllowanceChargeReason);
                    builder.AppendFormat("<MultiplierFactorNumeric>{0}</MultiplierFactorNumeric>", item.AllowanceChargeInfo.MultiplierFactorNumeric.ToOracleDecimalFormat());
                    builder.AppendFormat("<UnitCode>{0}</UnitCode>", item.InvoicedQuantity.unitCode);
                    builder.AppendFormat("<AllowanceAmount>{0}</AllowanceAmount>", item.AllowanceChargeInfo.Amount.Value);
                    builder.AppendFormat("<AllowanceBaseAmount>{0}</AllowanceBaseAmount>", item.AllowanceChargeInfo.BaseAmount.Value);


                    builder.AppendFormat("<TaxTotal>");
                    builder.AppendFormat("<Amount>{0}</Amount>", item.TaxTotal.TaxAmount.Value.ToOracleDecimalFormat());
                    builder.AppendFormat("<LineID>{0}</LineID>", item.ID);

                    foreach (TaxSubTotalInfo subItem in item.TaxTotal.TaxSubTotals)
                    {
                        builder.AppendFormat("<TaxSubTotal>");
                        builder.AppendFormat("<Amount>{0}</Amount>", subItem.TaxAmount.Value.ToOracleDecimalFormat());
                        builder.AppendFormat("<Name>{0}</Name>", subItem.TaxCategory.TaxScheme.Name);
                        builder.AppendFormat("<Percent>{0}</Percent>", subItem.Percent.ToOracleDecimalFormat());
                        builder.AppendFormat("<Code>{0}</Code>", subItem.TaxCategory.TaxScheme.TaxTypeCode);
                        builder.AppendFormat("<LineID>{0}</LineID>", item.ID);
                        builder.AppendFormat("</TaxSubTotal>");
                    }
                    builder.AppendFormat("</TaxTotal>");


                    /*
                     * PriceInfo
                     */

                    builder.Append("<PriceInfo>");
                    builder.AppendFormat("<Amount>{0}</Amount>", item.PriceInfo.PriceAmount.Value.ToOracleDecimalFormat());
                    builder.AppendFormat("<LineID>{0}</LineID>", item.ID);
                    builder.Append("</PriceInfo>");


                    /*
                     *ItemInfo 
                     */

                    builder.Append("<ItemInfo>");
                    builder.AppendFormat("<Name>{0}</Name>", item.Item.Name);
                    builder.AppendFormat("<LineID>{0}</LineID>", item.ID);
                    builder.Append("</ItemInfo>");


                    builder.AppendFormat("</InvoiceLine>");
                }
            }

            if (invoice.LegalMonetaryTotal != null)
            {
                builder.Append("<LegalMonetaryTotal>");
                builder.AppendFormat("<LineExtensionAmount>{0}</LineExtensionAmount>", invoice.LegalMonetaryTotal.LineExtensionAmount.Value.ToOracleDecimalFormat());
                builder.AppendFormat("<TaxExclusiveAmount>{0}</TaxExclusiveAmount>", invoice.LegalMonetaryTotal.TaxExclusiveAmount.Value.ToOracleDecimalFormat());
                builder.AppendFormat("<TaxInclusiveAmount>{0}</TaxInclusiveAmount>", invoice.LegalMonetaryTotal.TaxInclusiveAmount.Value.ToOracleDecimalFormat());
                builder.AppendFormat("<AllowanceTotalAmount>{0}</AllowanceTotalAmount>", invoice.LegalMonetaryTotal.AllowanceTotalAmount.Value.ToOracleDecimalFormat());
                builder.AppendFormat("<PayableAmount>{0}</PayableAmount>", invoice.LegalMonetaryTotal.PayableAmount.Value.ToOracleDecimalFormat());
                builder.Append("</LegalMonetaryTotal>");
            }

            builder.Append("</Invoice>");

            ServiceResult serviceResult = null;
            using (OracleDataClient client = new OracleDataClient())
            {
                List<Params> parameters = new List<Params>();
                parameters.AddOracleParameters("P_XML", builder.ToString());

                serviceResult = client.Execute("SP_SAVE_INVOICE_BY_XML", parameters, CreateOracleRefParameters());
            }

            string errorMessage = string.Empty;
            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                result.DocumentID = int.Parse(dataTable.Rows[0][0].ToString());
                result.IsSuccess = true;
                result.UUID = invoice.UUID;
            }
            else
            {
                result.Message = serviceResult.ErrMessage;
            }

            return result;
        }

        public ResponseService<InvoiceInfo> GetInvoiceByUUID(string uuid)
        {
            ResponseService<InvoiceInfo> response = new ResponseService<InvoiceInfo>();

            List<string> cursorParameters = CreateOracleRefParameters();

            cursorParameters.Add("P_IDENTIFICATION_CURSOR");
            cursorParameters.Add("P_INVOICELINE_CURSOR");
            cursorParameters.Add("P_TAXTOTALCURSOR");
            cursorParameters.Add("P_TAXSUBTOTALCURSOR");
            cursorParameters.Add("P_LEGALMONETARYCURSOR");

            List<Params> parameters = new List<Params>();
            parameters.AddOracleParameters("P_UUID", uuid);
            ServiceResult serviceResult = ExecuteOracleClient((param) =>
            {
                ServiceResult executorResult = null;
                using (OracleDataClient client = new OracleDataClient())
                {
                    executorResult = client.Execute("SP_GETINVOICEBY_UUID", parameters, cursorParameters);
                    param.Result = executorResult.Result;
                    param.ResultSet = executorResult.ResultSet;
                    param.ErrMessage = executorResult.ErrMessage;
                }
            });

            string errorMessage = string.Empty;
            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                response.HasError = false;
                DataRow dataRow = dataTable.Rows[0];
                InvoiceInfo invoice = new InvoiceInfo()
                {
                    ID = dataRow.Get<string>("ID"),
                    CustomizationID = dataRow.Get<string>("CUSTOMIZATIONID"),
                    CopyIndicator = int.Parse(dataRow["COPYINDICATOR"].ToString()).IntToBolean(),
                    IssueTime = dataRow.Get<string>("ISSUETIME"),
                    ProfileID = dataRow.Get<string>("PROFILEID"),
                    LineCountNumeric = dataRow.Get<int>("LINECOUNTNUMERIC"),
                    UUID = dataRow.Get<string>("UUID"),
                    IssueDate = dataRow.Get<string>("IssueDate"),
                    DocumentID = dataRow.Get<int>("INVOICEID").ToString(),
                    DocumentCurrencyCode = new DocumentCurrencyCodeInfo()
                    {
                        Value = dataRow.Get<string>("DOCUMENTCURRENCYCODE"),
                    },
                    InvoiceTypeCode = dataRow.Get<string>("INVOICETYPECODE"),
                    TaxCurrencyCode = dataRow.Get<string>("TAXCURRENCYCODE"),
                    PricingCurrencyCode = dataRow.Get<string>("PRICINGCURRENCYCODE"),
                    PaymentAlternativeCurrencyCode = dataRow.Get<string>("PAYMENTALTERNATIVECURRENCYCODE"),
                    PaymentCurrencyCode = dataRow.Get<string>("PAYMENTCURRENCYCODE"),
                };

                invoice.AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo();
                invoice.AccountingCustomerPartyInfo.Party = new PartyInfo();
                invoice.AccountingCustomerPartyInfo.Party.Alias = dataRow.Get<string>("C_ALIAS");
                invoice.AccountingCustomerPartyInfo.Party.Contact = new ContactIndicatorInfo()
                {
                    ElectronicMail = dataRow.Get<string>("C_ELECTRONICMAIL"),
                    Telefax = dataRow.Get<string>("C_TELEFAX"),
                    Telephone = dataRow.Get<string>("C_TELEPHONE"),
                };

                string documentCurrency = invoice.DocumentCurrencyCode.Value;

                invoice.AccountingCustomerPartyInfo.Party.PartyName = new PartyNameInfo();
                invoice.AccountingCustomerPartyInfo.Party.WebsiteURI = dataRow.Get<string>("C_WEBSITEURI");
                invoice.AccountingCustomerPartyInfo.Party.PartyName.Name = dataRow.Get<string>("C_PARTYNAME");

                invoice.AccountingCustomerPartyInfo.Party.PostalAddress = new PostalAddressInfo();
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName = dataRow.Get<string>("C_BUILDINGNAME");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber = dataRow.Get<string>("C_BUILDINGNUMBER");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CityName = dataRow.Get<string>("C_CITYNAME");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName = dataRow.Get<string>("C_CITYSUBDIVISIONNAME");
                //invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName = dataRow.Get<string>("C_CITYSUBDIVISIONNUMBER");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country = new CountryIndicatorInfo()
                {
                    Name = dataRow.Get<string>("C_COUNTRYNAME"),
                };

                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.PostalZone = dataRow.Get<string>("C_POSTALZONE");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Region = dataRow.Get<string>("C_REGION");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Room = dataRow.Get<string>("C_ROOM");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingNumber = dataRow.Get<string>("C_BUILDINGNUMBER");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.BuildingName = dataRow.Get<string>("C_BUILDINGNAME");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.StreetName = dataRow.Get<string>("C_STREETNAME");

                invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme = new PartyTaxSchemeInfo();
                invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme = new TaxSchemeInfo()
                {
                    Name = dataRow.Get<string>("C_SHEMENAME"),
                };

                DataTable identifications = serviceResult.ResultSet.Tables["P_IDENTIFICATION_CURSOR"];

                List<DataRow> collections = identifications.Rows.OfType<DataRow>().ToList();
                var customerParties = collections.Where(f => f.Field<int>("OWNER") == 1).ToList();

                if (customerParties.Count > 0)
                {
                    invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();
                    foreach (DataRow item in customerParties)
                    {
                        PartyIdentificationInfo info = new PartyIdentificationInfo()
                        {
                            ID = new IDContainerInfo()
                            {
                                Value = item.Get<string>("VALUE"),
                                schemeID = item.Get<string>("SCHEMAID"),
                            }
                        };

                        invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications.Add(info);
                    }
                }



                invoice.AccountingSupplierPartyInfo = new AccountingSupplierPartyInfo();
                invoice.AccountingSupplierPartyInfo.Party = new PartyInfo();
                invoice.AccountingSupplierPartyInfo.Party.Alias = dataRow.Get<string>("S_ALIAS");
                invoice.AccountingSupplierPartyInfo.Party.Contact = new ContactIndicatorInfo()
                {
                    ElectronicMail = dataRow.Get<string>("S_ELECTRONICMAIL"),
                    Telefax = dataRow.Get<string>("S_TELEFAX"),
                    Telephone = dataRow.Get<string>("S_TELEPHONE"),
                };

                invoice.AccountingSupplierPartyInfo.Party.PartyName = new PartyNameInfo();
                invoice.AccountingSupplierPartyInfo.Party.WebsiteURI = dataRow.Get<string>("S_WEBSITEURI");
                invoice.AccountingSupplierPartyInfo.Party.PartyName.Name = dataRow.Get<string>("S_PARTYNAME");

                invoice.AccountingSupplierPartyInfo.Party.PostalAddress = new PostalAddressInfo();
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingName = dataRow.Get<string>("S_BUILDINGNAME");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingNumber = dataRow.Get<string>("S_BUILDINGNUMBER");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.CityName = dataRow.Get<string>("S_CITYNAME");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.CitySubdivisionName = dataRow.Get<string>("S_CITYSUBDIVISIONNAME");
                //invoice.AccountingCustomerPartyInfo.Party.PostalAddress.CitySubdivisionName = dataRow.Get<string>("C_CITYSUBDIVISIONNUMBER");
                invoice.AccountingCustomerPartyInfo.Party.PostalAddress.Country = new CountryIndicatorInfo()
                {
                    Name = dataRow.Get<string>("S_COUNTRYNAME"),
                };

                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.PostalZone = dataRow.Get<string>("S_POSTALZONE");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.Region = dataRow.Get<string>("S_REGION");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.Room = dataRow.Get<string>("S_ROOM");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingNumber = dataRow.Get<string>("S_BUILDINGNUMBER");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.BuildingName = dataRow.Get<string>("S_BUILDINGNAME");
                invoice.AccountingSupplierPartyInfo.Party.PostalAddress.StreetName = dataRow.Get<string>("S_STREETNAME");

                invoice.AccountingSupplierPartyInfo.Party.PartyTaxScheme = new PartyTaxSchemeInfo();
                invoice.AccountingSupplierPartyInfo.Party.PartyTaxScheme.TaxScheme = new TaxSchemeInfo()
                {
                    Name = dataRow.Get<string>("S_SHEMENAME"),
                };

                var supplierParties = collections.Where(f => f.Field<int>("OWNER") == 2).ToList();

                if (supplierParties.Count > 0)
                {
                    invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications = new List<PartyIdentificationInfo>();
                    foreach (DataRow item in customerParties)
                    {
                        PartyIdentificationInfo info = new PartyIdentificationInfo()
                        {
                            ID = new IDContainerInfo()
                            {
                                Value = item.Get<string>("VALUE"),
                                schemeID = item.Get<string>("SCHEMAID"),
                            }
                        };

                        invoice.AccountingSupplierPartyInfo.Party.PartyIdentifications.Add(info);
                    }
                }

                DataTable invoiceLineTables = serviceResult.ResultSet.Tables["P_INVOICELINE_CURSOR"];
                DataTable taxTotalTables = serviceResult.ResultSet.Tables["P_TAXTOTALCURSOR"];
                DataTable taxSubtotalTables = serviceResult.ResultSet.Tables["P_TAXSUBTOTALCURSOR"];
                DataTable legalTotalTable = serviceResult.ResultSet.Tables["P_LEGALMONETARYCURSOR"];


                if (invoiceLineTables.Rows.Count > 0)
                {
                    invoice.InvoiceLineInfos = new List<InvoiceLineInfo>();
                    foreach (DataRow item in invoiceLineTables.Rows)
                    {
                        int invoiceLineID = item.Get<int>("PK_ID");

                        InvoiceLineInfo info = new InvoiceLineInfo()
                        {
                            Item = new ItemInfo
                            {
                                Name = item.Get<string>("NAME"),
                            },
                            PriceInfo = new PriceInfo
                            {
                                PriceAmount = new AmountContainerInfo
                                {
                                    Value = item.Get<decimal>("PRICEAMOUNT"),
                                }
                            },
                            ID = item.Get<int>("LINEID").ToString(),
                            LineExtensionAmount = new AmountContainerInfo()
                            {
                                Value = item.Get<decimal>("LINEEXTENSIONAMOUNT"),
                            },
                            InvoicedQuantity = new UnitCodeContainerInfo()
                            {
                                unitCode = item.Get<string>("UNITCODE"),
                                Value = item.Get<decimal>("INVOICEDQUANTITY"),
                            }
                        };

                        info.AllowanceChargeInfo = new AllowanceChargeInfo();
                        info.AllowanceChargeInfo.MultiplierFactorNumeric = item.Get<decimal>("MULTIPLIERFACTORNUMERIC");
                        info.AllowanceChargeInfo.ChargeIndicator = int.Parse(item["CHARGEINDICATOR"].ToString()).IntToBolean();
                        info.AllowanceChargeInfo.AllowanceChargeReason = item.Get<string>("ALLOWANCECHARGEREASON");
                        info.AllowanceChargeInfo.Amount = new AmountContainerInfo
                        {
                            Value = item.Get<decimal>("ALLOWANCEAMOUNT"),
                        };
                        info.AllowanceChargeInfo.BaseAmount = new AmountContainerInfo
                        {
                            Value = item.Get<decimal>("ALLOWANCEBASEAMOUNT"),
                        };

                        List<DataRow> invoiceLineTaxes = taxTotalTables.Rows.OfType<DataRow>().Where(f => f.Field<string>("OWNERTYPE") == "InvoiceLine").Where(f => f.Field<int>("INVOICELINEID") == invoiceLineID).ToList();

                        if (invoiceLineTaxes.Count > 0)
                        {
                            var taxDataRow = invoiceLineTaxes.FirstOrDefault();
                            info.TaxTotal = new TaxTotalInfo();
                            info.TaxTotal.TaxAmount = new AmountContainerInfo();
                            info.TaxTotal.TaxAmount.Value = taxDataRow.Get<decimal>("TAXAMOUNT");
                            int taxTotalID = taxDataRow.Get<int>("ID");

                            List<DataRow> invoiceLineTaxSubTotals = taxSubtotalTables.Rows.OfType<DataRow>().Where(f => f.Field<int>("TAXTOTALID") == taxTotalID).ToList();

                            if (invoiceLineTaxSubTotals.Count > 0)
                            {
                                info.TaxTotal.TaxSubTotals = new List<TaxSubTotalInfo>();
                            }

                            foreach (DataRow subTotalItem in invoiceLineTaxSubTotals)
                            {
                                TaxSubTotalInfo subTotalInfo = new TaxSubTotalInfo()
                                {
                                    Percent = subTotalItem.Get<decimal>("PERCENT"),
                                    TaxAmount = new AmountContainerInfo
                                    {
                                        Value = subTotalItem.Get<decimal>("TAXAMOUNT"),
                                    },
                                    TaxCategory = new TaxCategoryInfo
                                    {
                                        TaxExemptionReason = subTotalItem.Get<string>("TAXEXEMPTIONREASON"),
                                        TaxScheme = new TaxSchemeInfo
                                        {
                                            TaxTypeCode = subTotalItem.Get<string>("TAXSCHEMETAXTYPECODE"),
                                            Name = subTotalItem.Get<string>("TAXSCHEMENAME"),
                                        }
                                    }
                                };

                                info.TaxTotal.TaxSubTotals.Add(subTotalInfo);
                            }
                        }

                        invoice.InvoiceLineInfos.Add(info);
                    }


                    if (legalTotalTable.Rows.Count > 0)
                    {
                        DataRow legalTotalRow = legalTotalTable.Rows[0];

                        invoice.LegalMonetaryTotal = new LegalMonetaryTotalInfo()
                        {
                            AllowanceTotalAmount = new AmountContainerInfo()
                            {
                                Value = legalTotalRow.Get<decimal>("ALLOWANCETOTALAMOUNT"),
                                currencyID = documentCurrency,
                            },
                            LineExtensionAmount = new AmountContainerInfo()
                            {
                                Value = legalTotalRow.Get<decimal>("LINEEXTENSIONAMOUNT"),
                                currencyID = documentCurrency,
                            },
                            PayableAmount = new AmountContainerInfo()
                            {
                                Value = legalTotalRow.Get<decimal>("PAYABLEAMOUNT"),
                                currencyID = documentCurrency,
                            },
                            TaxExclusiveAmount = new AmountContainerInfo()
                            {
                                Value = legalTotalRow.Get<decimal>("TAXEXCLUSIVEAMOUNT"),
                                currencyID = documentCurrency,
                            },
                            TaxInclusiveAmount = new AmountContainerInfo()
                            {
                                Value = legalTotalRow.Get<decimal>("TAXINCLUSIVEAMOUNT"),
                                currencyID = documentCurrency,
                            }
                        };
                    }

                }

                string xml = invoice.SerializeObject<InvoiceInfo>();

                response.Result = invoice;
            }
            else
            {
                response.HasError = true;
                response.Message = errorMessage;
            }


            return response;
        }
    }
}
