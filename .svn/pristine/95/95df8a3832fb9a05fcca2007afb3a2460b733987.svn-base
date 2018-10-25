using Atlas.Efes.Common.Model;
using Atlas.Efes.OracleClient;
using Atlas.Efes.OracleClient.OracleServiceProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Atlas.Efes.DataContext
{
    public class CustomerDataContext : BaseDataContext
    {
        private static readonly CustomerDataContext _instance = new CustomerDataContext();
        public static CustomerDataContext Instance
        {
            get
            {
                return _instance;
            }
        }

        public ResponseService<CustomerInfo> GetCustomerByHeader(string username, string password)
        {
            ResponseService<CustomerInfo> response = new ResponseService<CustomerInfo>();
            List<Params> parameters = new List<Params>();
            parameters.AddOracleParameters("P_Username", username);
            parameters.AddOracleParameters("P_Password", password);

            ServiceResult serviceResult = null;
            using (OracleDataClient client = new OracleClient.OracleDataClient())
            {
                serviceResult = client.Execute("SP_GETCUSTOMER_BY_HEADER", parameters, CreateOracleRefParameters());
            }

            string errorMessage = string.Empty;

            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    CustomerInfo customerInfo = new CustomerInfo();
                    customerInfo.ID = dataRow.Get<int>("ID");
                    customerInfo.CustomerType = dataRow.Get<int>("CUSTOMERTYPE") == 1 ? CustomerTypeInfo.VKN : CustomerTypeInfo.TCKN;
                    customerInfo.Identification = dataRow.Get<string>("VKN_TCKN");
                    customerInfo.MersisNo = dataRow.Get<string>("MERSISNO");
                    customerInfo.ReceiverUnitAddress = dataRow.Get<string>("RECEIVERUNITADDRESS");
                    customerInfo.SenderUnitAddress = dataRow.Get<string>("SENDERUNITADDRESS");
                    customerInfo.Title = dataRow.Get<string>("TITLE");
                    customerInfo.TaxOffice = dataRow.Get<string>("TAXOFFICE");
                    customerInfo.WebsiteURI = dataRow.Get<string>("WEBSITE");
                    customerInfo.AddressInfo = new AddressInfo();
                    customerInfo.AddressInfo.ID = dataRow.Get<int>("CUSTOMERADDRESSID");
                    customerInfo.AddressInfo.BuildingName = dataRow.Get<string>("BUILDINGNAME");
                    customerInfo.AddressInfo.BuildingNumber = dataRow.Get<string>("BUILDINGNUMBER");
                    customerInfo.AddressInfo.Room = dataRow.Get<string>("DOORNUMBER");
                    customerInfo.AddressInfo.City = dataRow.Get<string>("CITY");
                    customerInfo.AddressInfo.Country = dataRow.Get<string>("COUNTRY");
                    customerInfo.AddressInfo.District = dataRow.Get<string>("DISTRICT");
                    customerInfo.AddressInfo.Street = dataRow.Get<string>("STREET");
                    customerInfo.AddressInfo.PostalCode = dataRow.Get<string>("POSTALCODE");
                    customerInfo.AddressInfo.Region = dataRow.Get<string>("REGION");
                    customerInfo.ContactInfo = new ContactInfo();
                    customerInfo.ContactInfo.ID = dataRow.Get<int>("CUSTOMERCONTACTID");
                    customerInfo.ContactInfo.Telefax = dataRow.Get<string>("FAX");
                    customerInfo.ContactInfo.Telephone = dataRow.Get<string>("TELEPHONE");
                    customerInfo.ContactInfo.ElectronicMail = dataRow.Get<string>("EMAIL");
                    response.Result = customerInfo;
                }
                else
                {
                    response.HasError = true;
                    response.Message = "No Data Found";
                }
            }


            return response;
        }
    }
}
