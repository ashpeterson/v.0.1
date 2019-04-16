using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Messages : EntityData
    {
        public string UserId { get; set; }
        public string Text { get; set; }
    }
}