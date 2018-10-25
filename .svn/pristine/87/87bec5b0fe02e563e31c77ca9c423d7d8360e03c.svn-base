using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Atlas.Efes.Common.GibModel
{
    [DataContract]
    public class StandardBusinessDocumentHeaderInfo
    {
        [DataMember]
        /// <summary>
        /// Header Version must be equal 1.2
        /// </summary>
        public string HeaderVersion = "1.0";

        [DataMember]
        public SenderInfo Sender { get; set;}

        [DataMember]
        public ReceiverInfo Receiver { get; set;}

        [DataMember]
        public DocumentIdentificationInfo DocumentIdentification { get; set; } 
    }
}
