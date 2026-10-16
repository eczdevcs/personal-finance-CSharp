using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
namespace PersonalFinanceMng.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreatePaymentMethodUseCase, CreatePaymentMethodUseCase>();

            return services;
        }
    }
}
