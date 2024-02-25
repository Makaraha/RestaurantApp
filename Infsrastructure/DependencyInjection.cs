﻿using Domain;
using Domain.Services;
using Infrastructure.Repositories;
using Infrastructure.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static partial class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            if (connectionString == null)
                throw new Exception("Failed to parse connectionString");

            services.AddScoped(_ => new SqlCommandProvider(connectionString));
            services.AddSingleton<SqlDataMapper>();
            services.AddScoped<SqlExecutor>();
            services.AddSingleton<SqlScriptGenerator>();

            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<MeasurementUnit>, Repository<MeasurementUnit>>();
        }
    }
}