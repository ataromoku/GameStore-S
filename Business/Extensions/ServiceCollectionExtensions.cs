﻿using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<IGameService, GameService>();
        services.AddScoped<ICommentService, CommentService>();

        services.AddAutoMapper(typeof(AutomapperProfile));
        
        return services;
    }
}