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
    public class UserDataContext : BaseDataContext
    {
        private static readonly UserDataContext _instance = new UserDataContext();

        public static UserDataContext Instance
        {
            get
            {
                return _instance;
            }
        }


        public ResponseService<UserInfo> Login(string username, string password)
        {
            ResponseService<UserInfo> response = new ResponseService<UserInfo>();

            List<Params> parameters = new List<Params>();
            parameters.AddOracleParameters("P_Username", username);
            parameters.AddOracleParameters("P_Password", password);

            ServiceResult serviceResult = null;
            using (OracleDataClient client = new OracleDataClient())
            {
                serviceResult = client.Execute("SP_LOGIN", parameters, CreateOracleRefParameters());
            }

            string errorMessage = string.Empty;
            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                DataRow dataRow = dataTable.Rows[0];
                UserInfo userInfo = new UserInfo();
                userInfo.Username = dataRow.Get<string>("USERNAME");
                userInfo.Password = dataRow.Get<string>("PASSWORD");
                userInfo.UserID = dataRow.Get<int>("ID");
                userInfo.CustomerID = dataRow.Get<int>("CUSTOMERID");
                response.Result = userInfo;
                
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
