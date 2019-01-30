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

    /*************************************************************************************
     The Azure Mobile Apps SDK uses DateTimeOffset instead of DateTime.
     A DateTime object is time zone aware, and time zone definitions
     change over time. The DateTimeOffset does not know anything about 
     time zones. The DateTime representation can change depending on where you are. 
     The DateTimeOffset will never change. This makes it a better choice for these things. 
     You will see dates stored in UTC in your database as a result of this.
     ************************************************************************************/

    public abstract class TableData
    {
        public string Id { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public byte[] Version { get; set; }
    }
}
