using System;
using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (!context.User.Any(u => u.Role == UserRole.Master))
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

            var userModel = new User.UserModel(
                "Administrador",
                "admin@system.com",
                senhaHash,
                UserRole.Master,
                1
            );

            var master = User.Create(userModel);

            context.User.Add(master);
            context.SaveChanges();
        }
    }
}

