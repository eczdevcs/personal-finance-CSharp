
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase;


namespace PersonalFinanceMng.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreatePaymentMethodUseCase, CreatePaymentMethodUseCase>();
            services.AddScoped<IFindPaymentMethodByIdUseCase, FindPaymentMethodByIdUseCase>();

            return services;
        }
    }
}
