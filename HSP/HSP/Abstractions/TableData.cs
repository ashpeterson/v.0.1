using System;
using System.Collections.Generic;
using System.Text;

namespace HSP.Abstractions
{
    /***********************************************************************
     These fields are required for offline sync capabilities 
     like incremental sync and conflict resolution. The fields
     are provided by an abstract base class on the client called TableData
    ************************************************************************/

    public abstract class TableData
    {
        public string Id { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public byte[] Version { get; set; }
    }
}
