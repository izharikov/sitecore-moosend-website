using System.Collections.Generic;

namespace Foundation.Moosend.Models.Forms
{
/* {
   "list":"8de9649c-2e47-4aaa-a563-93d2063eff77",
   "mapping":{
      "Email":"85cae71a-bf16-4eba-8e7b-fa2035b61bf5",
      "CustomField":"",
      "Name":"ec5f7dec-425a-408d-b4ac-325f423422e5"
   }
}
 */
    public class AddUserToMailingListData
    {
        public string List { get; set; }
        public IDictionary<string, string> Mapping { get; set; } = new Dictionary<string, string>();
    }
}