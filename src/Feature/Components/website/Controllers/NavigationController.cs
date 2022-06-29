using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Feature.Components.Controllers
{
    public class NavigationController: SitecoreController
    {
        public ActionResult Header()
        {
            return View(nameof(Header));
        }
        
        public ActionResult Footer()
        {
            return View(nameof(Footer));
        }
    }
}