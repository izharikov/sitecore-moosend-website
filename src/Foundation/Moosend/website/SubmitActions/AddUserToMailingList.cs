using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Moosend.Models.Forms;
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
            string emailFieldId = null, nameFieldId = null, tagsFieldId = null;
            data.Mapping?.TryGetValue(Constants.Email, out emailFieldId);
            data.Mapping?.TryGetValue(Constants.Name, out nameFieldId);
            data.Mapping?.TryGetValue(Constants.Tags, out tagsFieldId);
            var email = GetFieldValue(formSubmitContext, emailFieldId) as string;
            if (string.IsNullOrEmpty(list) || string.IsNullOrEmpty(email))
            {
                formSubmitContext.Abort();
                return false;
            }

            var service = ServiceLocator.ServiceProvider.GetService<IMailingListService>();

            var name = GetFieldValue(formSubmitContext, nameFieldId) as string;
            var tags = GetFieldValue(formSubmitContext, tagsFieldId) as IEnumerable<string>;
            var customFields = GetCustomFields(data, formSubmitContext);
            
            var success = service.AddToMailingList(list, email, name, customFields, tags);
            return success;
        }

        private static IList<string> GetCustomFields(AddUserToMailingListData data, FormSubmitContext formSubmitContext)
        {
            var customFields = new List<string>();
            if (data.Mapping == null)
            {
                return customFields;
            }
            foreach (var pair in data.Mapping.Where(x => !Constants.SpecialFields.Contains(x.Key)))
            {
                var value = GetFieldValue(formSubmitContext, pair.Value);
                if (value != null)
                {
                    customFields.Add($"{pair.Key}={value}");
                }
            }

            return customFields;
        }

        private static object GetFieldValue(FormSubmitContext formSubmitContext, string fieldId)
        {
            if (string.IsNullOrEmpty(fieldId))
            {
                return null;
            }
            var field = formSubmitContext.Fields.FirstOrDefault(x => x.ItemId == fieldId);
            switch (field)
            {
                case InputViewModel<string> stringField:
                    return stringField.Value;
                case InputViewModel<List<string>> listField:
                    return listField.Value;
                case InputViewModel<double?> numberField:
                    return numberField.Value;
                case InputViewModel<bool> checkboxField:
                    return checkboxField.Value;
                case InputViewModel<DateTime?> dateField:
                    return dateField.Value;
            }

            return null;
        }
    }
}