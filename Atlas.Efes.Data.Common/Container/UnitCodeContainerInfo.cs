﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.Container
{
    public class UnitCodeContainerInfo
    {
        [XmlText]
        public decimal Value { get; set; }

        [XmlAttribute("unitCode")]
        public string unitCode = "NIU";
    }
}
