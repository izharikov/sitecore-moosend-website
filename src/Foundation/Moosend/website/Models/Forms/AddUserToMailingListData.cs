using System.Collections.Generic;

namespace Foundation.Moosend.Models.Forms
{
    public class AddUserToMailingListData
    {
        public string List { get; set; }
        public IDictionary<string, string> Mapping { get; set; } = new Dictionary<string, string>();
    }
}