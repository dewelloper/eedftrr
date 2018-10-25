using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.Model
{
    public abstract class OperationResultBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
