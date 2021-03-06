﻿using Atlas.Efes.OracleClient.OracleServiceProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Atlas.Efes.DataContext
{
    public abstract class BaseDataContext
    {
        internal List<string> CreateOracleRefParameters()
        {
            List<string> list = new List<string>();

            list.Add("P_DataRefCursor");
            list.Add("P_ErrorRefCursor");

            return list;
        }

        internal ServiceResult ExecuteOracleClient(Action<ServiceResult> ActionExecutor)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                ActionExecutor(serviceResult);
            }
            catch (Exception ex)
            {
                serviceResult.ErrMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }

            return serviceResult;
        }

        internal DataTable GetDataResult(ServiceResult serviceResult, out string errorMessage)
        {
            DataTable dataTable = null;
            errorMessage = string.Empty;
            if ((int)serviceResult.Result > 0)
            {
                DataTable errorDataTable = serviceResult.ResultSet.Tables["P_ErrorRefCursor"];
                if (errorDataTable.Rows[0][0].ToString() == "0")
                {
                    dataTable = serviceResult.ResultSet.Tables["P_DataRefCursor"];
                }
                else
                {
                    errorMessage = errorDataTable.Rows[0][1].ToString();
                }
            }
            else
            {
                errorMessage = serviceResult.ErrMessage;
            }

            return dataTable;
        }
    }
}
