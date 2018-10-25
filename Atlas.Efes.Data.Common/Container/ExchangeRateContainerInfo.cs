using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.Container
{
    public abstract class ExchangeRateContainerInfo
    {
        public string SourceCurrencyCode { get; set; }
        public string TargetCurrencyCode { get; set; }
        public decimal CalculationRate { get; set; }
        public string IssueDate { get; set; }
    }
}
