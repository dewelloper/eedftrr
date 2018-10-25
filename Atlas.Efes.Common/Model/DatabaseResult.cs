using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.Model
{
    public class DatabaseResult<T> : DatabaseResult
    {
        public T Result { get; set; }
    }

    public class DatabaseResult : OperationResultBase
    {
        public int DocumentID { get; set; }
    }
}
