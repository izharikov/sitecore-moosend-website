using System.Collections.Generic;
using System.Linq;
using Foundation.Moosend.Models;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Model;
using MailingList = Foundation.Moosend.Models.MailingList;

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

        public IEnumerable<MailingList> GetAll()
        {
            var response =
                _mailingListsApi.GettingAllActiveMailingLists(_moosendSettings.Format, _moosendSettings.ApiKey);
            return response.Context.MailingLists.Select(x => new MailingList()
            {
                Name = x.Name,
                Id = x.ID,
                CustomProperties = x.CustomFieldsDefinition.Select(y => new CustomProperty()
                {
                    Name = y.Name,
                }),
            });
        }

        public bool AddToMailingList(string mailingList, string email)
        {
            var response = _subscribersApi.AddingSubscribersWithHttpInfo(_moosendSettings.Format, mailingList,
                _moosendSettings.ApiKey, new AddingSubscribersRequest(email));
            return response.StatusCode >= 200 && response.StatusCode <= 299;
        }
    }
}