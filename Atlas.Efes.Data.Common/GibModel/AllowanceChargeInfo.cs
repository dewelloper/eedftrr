using Atlas.Efes.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.GibModel
{
    public class AllowanceChargeInfo
    {
        public bool ChargeIndicator { get; set; }
        public string AllowanceChargeReason { get; set; }
        public AmountContainerInfo Amount { get; set; }
        public decimal MultiplierFactorNumeric { get; set; }
        public AmountContainerInfo BaseAmount { get; set; }
    }
}
