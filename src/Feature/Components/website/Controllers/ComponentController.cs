using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Feature.Components.Controllers
{
    public class ComponentController: SitecoreController
    {
        public ActionResult RichText()
        {
            return View(nameof(RichText));
        }
    }
}