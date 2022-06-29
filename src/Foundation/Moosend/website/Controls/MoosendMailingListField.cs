using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Foundation.Moosend.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;

namespace Foundation.Moosend.Controls
{
    public class MoosendMailingListField : LookupEx
    {
        protected override void DoRender(HtmlTextWriter output)
        {
            Assert.ArgumentNotNull(output, nameof(output));
            var items = GetItems();
            output.Write("<select" + GetControlAttributes() + ">");
            output.Write("<option value=\"\"></option>");
            var hasSelection = false;
            foreach (var item in items)
            {
                var itemHeader = item.Value;
                var isSelected = Value == item.Key;
                if (isSelected)
                    hasSelection = true;
                output.Write("<option value=\"" + item.Key + "\"" +
                             (isSelected ? " selected=\"selected\"" : string.Empty) + ">" + itemHeader + "</option>");
            }

            var isWrongSelection = !string.IsNullOrEmpty(Value) && !hasSelection;
            if (isWrongSelection)
            {
                output.Write("<optgroup label=\"" + Translate.Text("Value not in the selection list.") + "\">");
                var str = HttpUtility.HtmlEncode(Value);
                output.Write("<option value=\"" + str + "\" selected=\"selected\">" + str + "</option>");
                output.Write("</optgroup>");
            }

            output.Write("</select>");
            if (!isWrongSelection)
                return;
            output.Write("<div style=\"color:#999999;padding:2px 0px 0px 0px\">{0}</div>",
                Translate.Text("The field contains a value that is not in the selection list."));
        }

        private IEnumerable<KeyValuePair<string, string>> GetItems()
        {
            var moosendService = ServiceLocator.ServiceProvider.GetService<IMailingListService>();
            return moosendService.GetAll().Select(x => new KeyValuePair<string, string>(x.Id, x.Name));
        }
    }
}