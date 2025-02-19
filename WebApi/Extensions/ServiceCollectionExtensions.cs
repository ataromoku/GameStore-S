﻿using System.Reflection;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddResponseCaching();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Game Store API"
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(xmlFilePath);
        });
        
        return services;
    }
}