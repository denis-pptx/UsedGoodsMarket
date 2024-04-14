﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Item.DataAccess.Data;

public static class MigrationExtension
{
    public static void ApplyMigrations<TDbContext>(this IApplicationBuilder app) 
        where TDbContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<DbContext>();
        dbContext?.Database.Migrate();
    }
}