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
    public class LookupDataContext : BaseDataContext
    {
        private static readonly LookupDataContext _instance = new LookupDataContext();

        public static LookupDataContext Instance
        {
            get
            {
                return _instance;
            }
        }

        public ResponseService<List<LookupInfo>> GetLookupByKey(string key)
        {
            ResponseService<List<LookupInfo>> response = new ResponseService<List<LookupInfo>>();
            OracleDataClient oracleDataClient = new OracleDataClient();

            List<Params> parameters = new List<Params>();
            parameters.Add(new Params()
            {
                ParamName = "P_LookupTypeName",
                ParamVal = key
            });


            List<string> cursorParameters = CreateOracleRefParameters();

            ServiceResult serviceResult = oracleDataClient.Execute("SP_GetLookupByTypeName", parameters, cursorParameters);

            if ((int)serviceResult.Result > 0)
            {
                DataTable dataTable = serviceResult.ResultSet.Tables["P_ErrorRefCursor"];
                if (dataTable.Rows[0][0].ToString() == "0")
                {
                    DataTable resultDataTable = serviceResult.ResultSet.Tables["P_DataRefCursor"];
                    if (resultDataTable.Rows.Count > 0)
                    {
                        response.Result = new List<LookupInfo>();
                        foreach (DataRow item in resultDataTable.Rows)
                        {
                            LookupInfo lookupInfo = new LookupInfo()
                            {
                                Key = item.Get<string>("KEY"),
                                Value = item.Get<string>("VALUE"),
                            };
                            response.Result.Add(lookupInfo);
                        }
                    }

                }
            }


            return response;
        }


        public List<CityInfo> GetCities()
        {
            List<CityInfo> cities = new List<CityInfo>();
            OracleDataClient oracleDataClient = new OracleDataClient();

            ServiceResult serviceResult = oracleDataClient.Execute("SP_GetCities", null, CreateOracleRefParameters());

            string errorMessage = string.Empty;
            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    CityInfo cityInfo = new CityInfo();
                    cityInfo.ID = item.Get<int>("ID");
                    cityInfo.Name = item.Get<string>("NAME");
                    cityInfo.CounrtyID = item.Get<int>("COUNTRYID");
                    cities.Add(cityInfo);
                }
            }

            return cities;
        }

        public List<CountryInfo> GetCountries()
        {
            List<CountryInfo> countries = new List<CountryInfo>();

            ServiceResult serviceResult = null;
            using (OracleDataClient client = new OracleDataClient())
            {
                serviceResult = client.Execute("SP_GetCountries", null, CreateOracleRefParameters());
            }

            string errorMessage = string.Empty;
            DataTable dataTable = GetDataResult(serviceResult, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    CountryInfo countryInfo = new CountryInfo();
                    countryInfo.ID = item.Get<int>("ID");
                    countryInfo.Name = item.Get<string>("NAME");
                    countries.Add(countryInfo);
                }
            }

            return countries;
        }
    }
}
