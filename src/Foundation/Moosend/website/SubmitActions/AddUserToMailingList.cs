using System.Linq;
using Foundation.Moosend.Models;
using Foundation.Moosend.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;

namespace Foundation.Moosend.SubmitActions
{
    public class AddUserToMailingList : SubmitActionBase<AddUserToMailingListData>
    {
        public AddUserToMailingList(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }

        protected override bool Execute(AddUserToMailingListData data, FormSubmitContext formSubmitContext)
        {
            var list = data.List;
            var email = formSubmitContext.Fields.OfType<InputViewModel<string>>().FirstOrDefault(x => x.Name == "Email")
                ?.Value;
            if (!string.IsNullOrEmpty(list) && !string.IsNullOrEmpty(email))
            {
                var service = ServiceLocator.ServiceProvider.GetService<IMailingListService>();
                return service.AddToMailingList(list, email);
            }
            return false;
        }
    }
}