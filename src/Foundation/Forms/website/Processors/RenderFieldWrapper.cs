using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.Mvc.Pipelines;

namespace Foundation.Forms.Processors
{
    public class RenderFieldWrapper: MvcPipelineProcessor<RenderFieldEventArgs>
    {
        public override void Process(RenderFieldEventArgs args)
        {
            args.Result = MvcHtmlString.Create($"<div class=\"field\">{args.Result}</div>");
        }
    }
}