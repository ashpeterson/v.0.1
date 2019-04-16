using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.DataObjects
{
    public class Sync : EntityData
    {
        public string GroupId { get; set; }
        public string StringField { get; set; }
        public int IntField { get; set; }
        public double DoubleField { get; set; }
        public DateTimeOffset DateTimeField { get; set; }
    }
}