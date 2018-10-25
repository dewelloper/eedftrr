using Atlas.Efes.ServiceTest.IntegrationServiceProxy;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace Atlas.Efes.ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"C:\Users\arif\Desktop\a.xml");
            //XmlSchemaParser.ParseApplicationResponse(doc.InnerXml);

            var m = new EfesIntegrationServiceClient().GetApplicationResponse("b6fc6260-332d-4e4c-8c42-a972577ccb15");

            return;
            InvoiceInfo invoice = new InvoiceInfo()
            {
                AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo()
                {
                    Party = new PartyInfo()
                    {

                    }
                },
                LineCountNumeric = 1,
                CustomizationID = "TR1.2",
                DocumentCurrencyCode = new DocumentCurrencyCodeInfo() { Value = "TRY", },
                CopyIndicator = false,
                IssueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ProfileID = "TEMELFATURA",
                InvoiceTypeCode = "SATIS",
                UUID = Guid.NewGuid().ToString().ToUpper(),
                UBLVersionID = "2.1",
            };


            List<PartyIdentificationInfo> list = new List<PartyIdentificationInfo>();
            list.Add(new PartyIdentificationInfo()
            {
                ID = new IDContainerInfo()
                {
                    Value = "1020409972",
                    schemeID = "VKN",
                }
            });

            invoice.AccountingCustomerPartyInfo.Party.PartyIdentifications = list.ToArray();

            invoice.AccountingCustomerPartyInfo.Party.PartyName = new PartyNameInfo()
            {
                Name = "ATLAS YAZILIM VE BILISIM HIZMETLERI",
            };

            invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme = new PartyTaxSchemeInfo();
            invoice.AccountingCustomerPartyInfo.Party.PartyTaxScheme.TaxScheme = new TaxSchemeInfo();

            invoice.AccountingCustomerPartyInfo.Party.PostalAddress = new PostalAddressInfo()
            {
                Country = new CountryIndicatorInfo()
                {
                    Name = "TURKIYE",
                },
                CityName = "Istanbul",
                CitySubdivisionName = "ESENLER",
            };

            invoice.AccountingCustomerPartyInfo.Party.Contact = new ContactIndicatorInfo()
            {
                ElectronicMail = "arif@gmail.com",
                Telefax = null,
                Telephone = null,
            };


            invoice.TaxTotalInfo = new TaxTotalInfo();
            invoice.TaxTotalInfo.TaxAmount = new AmountContainerInfo();
            invoice.TaxTotalInfo.TaxAmount.Value = 2;
            invoice.TaxTotalInfo.TaxAmount.currencyID = invoice.DocumentCurrencyCode.Value;

            List<TaxSubTotalInfo> taxes = new List<TaxSubTotalInfo>();
            taxes.Add(new TaxSubTotalInfo()
            {
                TaxAmount = new AmountContainerInfo() { Value = 2, currencyID = invoice.DocumentCurrencyCode.Value, },
                TaxCategory = new TaxCategoryInfo() { TaxScheme = new TaxSchemeInfo() { Name = "KDV", TaxTypeCode = "0015", } },
                Percent = 18,
            });

            invoice.TaxTotalInfo.TaxSubTotals = taxes.ToArray();

            List<InvoiceLineInfo> lines = new List<InvoiceLineInfo>();
            lines.Add(new InvoiceLineInfo()
            {
                ID = "1",
                InvoicedQuantity = new UnitCodeContainerInfo() { unitCode = "NIU", Value = 1, },
                LineExtensionAmount = new AmountContainerInfo() { Value = 2, currencyID = invoice.DocumentCurrencyCode.Value, },
                PriceInfo = new PriceInfo()
                {
                    PriceAmount = new AmountContainerInfo()
                    {
                        Value = 2,
                        currencyID = invoice.DocumentCurrencyCode.Value,
                    }
                },
                Item = new ItemInfo()
                {
                    Name = "EKMEK",
                },
                TaxTotal = new TaxTotalInfo()
                {
                    TaxAmount = new AmountContainerInfo() { Value = 2, currencyID = invoice.DocumentCurrencyCode.Value },
                    TaxSubTotals = taxes.ToArray(),
                }
            });

            invoice.LegalMonetaryTotal = new LegalMonetaryTotalInfo()
            {
                LineExtensionAmount = new AmountContainerInfo() { Value = 2, currencyID = invoice.DocumentCurrencyCode.Value, },
                AllowanceTotalAmount = new AmountContainerInfo() { Value = 0, currencyID = invoice.DocumentCurrencyCode.Value, },
                PayableAmount = new AmountContainerInfo() { Value = 4, currencyID = invoice.DocumentCurrencyCode.Value, },
                TaxExclusiveAmount = new AmountContainerInfo() { Value = 2, currencyID = invoice.DocumentCurrencyCode.Value, },
                TaxInclusiveAmount = new AmountContainerInfo() { Value = 4, currencyID = invoice.DocumentCurrencyCode.Value, },
            };
            invoice.InvoiceLineInfos = lines.ToArray();

            ApplicationResponse applicationResponse = null;
            string instanceIdentifier = null;
            using (var client = new EfesIntegrationServiceClient())
            {
                OperationContextScope scope = new OperationContextScope(client.InnerChannel);
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();

                requestMessage.Headers["ApiKey"] = "Gf6agSYcwSuHtJWR47kAy3NBX/Gg9SUKesHQ50KXIns=";
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                var result = client.SendDocument(invoice);
                instanceIdentifier = result.InstanceIdentifier;
                applicationResponse = client.GetApplicationResponse(instanceIdentifier);
            }
        }

    }
}
