using System.Collections.Generic;
using System.Linq;
using Foundation.Moosend.Models;
using Foundation.Moosend.Models.Api;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Foundation.Moosend.Services
{
    public class MailingListService : IMailingListService
    {
        private readonly IMailingListsApi _mailingListsApi;
        private readonly ISubscribersApi _subscribersApi;
        private readonly IMoosendSettings _moosendSettings;

        public MailingListService(IMoosendSettings moosendSettings, IMailingListsApi mailingListsApi,
            ISubscribersApi subscribersApi)
        {
            _moosendSettings = moosendSettings;
            _mailingListsApi = mailingListsApi;
            _subscribersApi = subscribersApi;
        }

        public IEnumerable<MailingListJsonModel> GetAll()
        {
            var response =
                _mailingListsApi.GettingAllActiveMailingLists(_moosendSettings.Format, _moosendSettings.ApiKey);
            return response.Context.MailingLists.Select(x => new MailingListJsonModel()
            {
                Name = x.Name,
                Id = x.ID,
                CustomProperties = x.CustomFieldsDefinition.Select(y => new CustomProperty()
                {
                    Name = y.Name,
                }),
            });
        }

        public bool AddToMailingList(string mailingList, string email, string name, object customFields, IEnumerable<string> tags)
        {
            var response = _subscribersApi.AddingSubscribersWithHttpInfo(_moosendSettings.Format, mailingList,
                _moosendSettings.ApiKey, new CustomAddingSubscribersRequest(email, name, customFields)
                {
                    Tags = tags,
                });
            return response.StatusCode >= 200 && response.StatusCode <= 299;
        }
    }
}