using Atlas.Efes.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Atlas.Efes.Common.GibModel
{
    [DataContract]
    public class PaymentTermsInfo
    {
        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public decimal PenaltySurchargePercent { get; set; }

        [DataMember]
        public AmountContainerInfo Amount { get; set; }
    }
}
