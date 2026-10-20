
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.HardDeletePaymentMethodUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.SoftDeletePaymentMehtodUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs;


namespace PersonalFinanceMng.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreatePaymentMethodUseCase, CreatePaymentMethodUseCase>();
            services.AddScoped<IFindPaymentMethodByIdUseCase, FindPaymentMethodByIdUseCase>();
            services.AddScoped<IFindAllByUserIdUseCase, FindAllByUserIdUseCase>();
            services.AddScoped<IUpdatePaymentMethodUseCase, UpdatePaymentMethodUseCase>();
            services.AddScoped<ISoftDeletePaymentMethodUseCase,  SoftDeletePaymentMethodUseCase>();
            services.AddScoped<IHardDeletePaymentMethod,  HardDeletePaymentMethodUseCase>(); 
            return services;
        }
    }
}
