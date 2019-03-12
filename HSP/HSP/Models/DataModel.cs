
using System;
using System.Collections.Generic;
using System.Text;
using HSP.Abstractions;

namespace HSP.Models
{
     public class DataModel : TableData
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}
