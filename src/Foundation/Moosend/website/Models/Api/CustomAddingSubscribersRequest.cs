using System.Collections.Generic;
using System.Runtime.Serialization;
using Moosend.Wrappers.CSharpWrapper.Model;
using Newtonsoft.Json;

namespace Foundation.Moosend.Models.Api
{
    public class CustomAddingSubscribersRequest : AddingSubscribersRequest
    {
        [JsonConstructor]
        protected CustomAddingSubscribersRequest()
        {
        }

        public CustomAddingSubscribersRequest(string email = null, string name = null, object customFields = null) :
            base(email, name, customFields)
        {
        }

        [DataMember(EmitDefaultValue = false, Name = "Tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}