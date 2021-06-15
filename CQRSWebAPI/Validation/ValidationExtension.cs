using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Validation
{
    public static class ValidationExtension
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblyOf<IValidationHandler>()
                        .AddClasses(cls => cls.AssignableTo<IValidationHandler>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime());
        }
    }
}