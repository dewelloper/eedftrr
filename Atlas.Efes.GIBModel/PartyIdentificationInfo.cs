using Atlas.Efes.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Atlas.Efes.GIBModel
{

    [XmlRoot(ElementName = "PartyIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
    public class PartyIdentificationInfo : BasePropertyChanged
    {
        private IDContainerInfo id;
        public IDContainerInfo ID
        {
            get
            {
                if (id == null)
                {
                    id = new IDContainerInfo();
                }
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("ID");
            }
        }
    }
}
