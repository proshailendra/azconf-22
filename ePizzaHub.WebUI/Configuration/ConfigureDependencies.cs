using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ePizzaHub.WebUI.Configuration
{
    public class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IFileHelper, FileHelper>();
        }
    }
}