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

        public CustomAddingSubscribersRequest(string Email = null, string Name = null, object CustomFields = null) :
            base(Email, Name, CustomFields)
        {
        }

        [DataMember(EmitDefaultValue = false, Name = "Tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}