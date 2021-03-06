using System.Collections.Generic;
using Foundation.Moosend.Models;

namespace Foundation.Moosend.Services
{
    public interface IMailingListService
    {
        IEnumerable<MailingList> GetAll();
        bool AddToMailingList(string mailingList, string email);
    }
}