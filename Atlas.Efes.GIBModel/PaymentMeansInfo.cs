using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.GIBModel
{
    public class PaymentMeansInfo
    {
        public string PaymentMeansCode { get; set; }
        public string PaymentChannelCode { get; set; }
        public string PaymentDueDate { get; set; }
        public string InstructionNote { get; set; }

        public PayeeFinancialAccountInfo PayeeFinancialAccount { get; set; }
    }
}
