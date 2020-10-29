using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("WBCTemplateNG"));
            else
                services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<ISubscriptionService, SubscriptionService>();
            //services.AddTransient<IWbcSsoHttpClientService, WbcSsoHttpClientService>();
            //services.AddTransient<ISsoService, SsoService>();
            //services.AddTransient<INotificationService, NotificationService>();
            //services.AddTransient<IWorkflowService, WorkFlowService>();
            //services.AddTransient<IDocumentService, DocumentService>();
            //services.AddTransient<ILongRunningTaskProccesor, LongRunningTaskProcessor>();
            return services;
        }
    }
}