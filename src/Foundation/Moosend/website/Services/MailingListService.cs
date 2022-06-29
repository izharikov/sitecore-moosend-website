using System.Collections.Generic;
using System.Linq;
using Foundation.Moosend.Models;
using Moosend.Wrappers.CSharpWrapper.Api;

namespace Foundation.Moosend.Services
{
    public class MailingListService : IMailingListService
    {
        private readonly IMailingListsApi _mailingListsApi;
        private readonly IMoosendSettings _moosendSettings;

        public MailingListService(IMoosendSettings moosendSettings, IMailingListsApi mailingListsApi)
        {
            _moosendSettings = moosendSettings;
            _mailingListsApi = mailingListsApi;
        }

        public IEnumerable<MailingList> GetAll()
        {
            var response = _mailingListsApi.GettingAllActiveMailingLists(_moosendSettings.Format, _moosendSettings.ApiKey);
            return response.Context.MailingLists.Select(x => new MailingList()
            {
                Name = x.Name,
                Id = x.ID,
            });
        }
    }
}