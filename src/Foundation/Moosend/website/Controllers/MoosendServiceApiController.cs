using System.Web.Http;
using Foundation.Moosend.Services;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Foundation.Moosend.Controllers
{
    [ServicesController("Moosend")]
    public class MoosendServiceApiController : ServicesApiController
    {
        private readonly IMailingListService _mailingListService;

        public MoosendServiceApiController(IMailingListService mailingListService)
        {
            _mailingListService = mailingListService;
        }

        [HttpGet]
        [ActionName("All")]
        public IHttpActionResult All()
        {
            var result = _mailingListService.GetAll();
            return Ok(new {Data = result});
        }
    }
}