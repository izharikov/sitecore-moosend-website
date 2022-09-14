using System.Collections.Generic;
using Foundation.Moosend.Models;

namespace Foundation.Moosend.Services
{
    public interface IMailingListService
    {
        IEnumerable<MailingListJsonModel> GetAll();
        bool AddToMailingList(string mailingList, string email, string name, object customFields, IEnumerable<string> tags);
    }
}