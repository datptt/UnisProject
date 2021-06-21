﻿using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unis.Repository;

namespace Unis.API
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //Add DBContext
            services.AddDbContext<RepositoryContext>();

            services.AddScoped<Func<RepositoryContext>>((provider) => () => provider.GetService<RepositoryContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
                .AddScoped<IUserRepository, UserRepository>()
                 .AddScoped<ICalimsService, ClaimsService>()
                 .AddScoped<IPolicyEvaluator, BearerPolicyEvaluator>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<UserBL>();
        }


        public static IServiceCollection AddAuthen(this IServiceCollection services, IConfiguration _configuration)
        {
            return (IServiceCollection)services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearerConfiguration(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"]
      );
        }
    }
}
