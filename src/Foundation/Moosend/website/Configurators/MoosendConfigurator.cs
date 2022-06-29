using Foundation.Moosend.Models;
using Foundation.Moosend.Services;
using Microsoft.Extensions.DependencyInjection;
using Moosend.Wrappers.CSharpWrapper.Api;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;

namespace Foundation.Moosend.Configurators
{
    public class MoosendConfigurator: IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMoosendSettings>((pr) =>
            {
                var settings = pr.GetService<BaseSettings>();
                return new MoosendSettings()
                {
                    ApiKey = settings.GetSetting("Moosend.ApiKey"),
                };
            });
            serviceCollection.AddScoped<IMailingListsApi, MailingListsApi>();
            serviceCollection.AddScoped<IMailingListService, MailingListService>();
        }
    }
}