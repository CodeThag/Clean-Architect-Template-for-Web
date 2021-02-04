using Application.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.Dependencies
{
    public static class ConfigurationDependencyInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfiguration = configuration.GetSection(nameof(SMTPConfiguration)).Get<SMTPConfiguration>();
            services.AddSingleton(smtpConfiguration);
            var smsConfiguration = configuration.GetSection(nameof(SMSConfiguration)).Get<SMSConfiguration>();
            services.AddSingleton(smsConfiguration);

            return services;
        }
    }
}
