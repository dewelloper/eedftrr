﻿using Atlas.Efes.OracleClient.OracleServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Atlas.Efes.OracleClient
{
    public class OracleDataClient : IDisposable
    {


        public OracleDataClient()
        {

        }

        public ServiceResult Execute(string procedureName, List<Params> parameters, List<string> cursorParameters)
        {
            Service1Client client = new Service1Client();
            OperationContextScope scope = new System.ServiceModel.OperationContextScope(client.InnerChannel);

            OraHeader header = new OraHeader()
            {
                username = "FreeAdmUs",
                password = "HuK70185189KklP",
                Company = "efaturadb",
            };

            MessageHeader messageHeader = System.ServiceModel.Channels.MessageHeader.CreateHeader("affiliateApprover", "http://ticketservice.atlasyazilim.com.tr", "");
            string addionatinalInfo = "efaturausr";
            System.ServiceModel.Channels.MessageHeader extraHeader = System.ServiceModel.Channels.MessageHeader.CreateHeader("company", "http://ticketservice.atlasyazilim.com.tr", addionatinalInfo);
            System.ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add(extraHeader);

            return client.GetServiceResultMulti(procedureName, parameters != null ? parameters.ToArray() : null, cursorParameters.ToArray());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }

    public class OraHeader
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Company { get; set; }
    }
}
