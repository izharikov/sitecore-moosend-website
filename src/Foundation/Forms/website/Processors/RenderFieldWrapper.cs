using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.ExperienceForms;
using Sitecore.ExperienceForms.Mvc.Pipelines;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderField;
using Sitecore.Mvc.Pipelines;

namespace Foundation.Forms.Processors
{
    public class RenderFieldWrapper : MvcPipelineProcessor<RenderFieldEventArgs>
    {
        private static readonly ISet<string> DoNotWrapFieldTypeIds = new HashSet<string>(new[]
        {
            "{D819B43E-C136-4392-9393-8BE7FCE32F5E}", // Page
            "{447AA745-6D29-4B65-A5A3-8173AA8AF548}", // Section
            "{3A4DF9C0-7C82-4415-90C3-25440257756D}", // Form
        });

        private readonly IFormBuilderContext _formBuilderContext;

        public RenderFieldWrapper(IFormBuilderContext formBuilderContext)
        {
            _formBuilderContext = formBuilderContext;
        }

        public override void Process(RenderFieldEventArgs args)
        {
            if (_formBuilderContext.FormBuilderMode != FormBuilderMode.Load ||
                DoNotWrapFieldTypeIds.Contains(args.ViewModel.FieldTypeItemId))
            {
                return;
            }

            args.Result = MvcHtmlString.Create($"<div class=\"form-group\">{args.Result}</div>");
        }
    }
}