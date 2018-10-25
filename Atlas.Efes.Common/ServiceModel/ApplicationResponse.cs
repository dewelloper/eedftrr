using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.ServiceModel
{
    public class ApplicationResponse
    {
        public string InstanceIdentifier { get; set; }

        public string ApplicationCode { get; set; }

        public string Description { get; set; }

        public string ResponseXml { get; set; }
    }
}
