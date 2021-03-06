using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using MediatR;

namespace SentezMicro.Services.Customer.Application.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}