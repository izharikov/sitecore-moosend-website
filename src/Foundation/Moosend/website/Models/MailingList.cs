using System.Collections.Generic;

namespace Foundation.Moosend.Models
{
    public class MailingList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CustomProperty> CustomProperties { get; set; }
    }

    public class CustomProperty
    {
        public string Name { get; set; }
    }
}