using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>().HasData(CreateUsers());
        builder.Entity<AppRole>().HasData(CreateRoles());
        builder.Entity<IdentityUserRole<string>>().HasData(CreateUsersRoles());

        base.OnModelCreating(builder);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    #region Private

    private static ICollection<AppUser> CreateUsers()
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        var password = passwordHasher.HashPassword(null, "password");

        var users = new List<AppUser>
        {
            new AppUser
            {
                Id = "1",
                FirstName = "MBARK",
                LastName = "T3STO",
                UserName = "mbark",
                NormalizedUserName = "MBARK",
                Email = "mbark@mail.com",
                PasswordHash = password
            },
            new AppUser
            {
                Id = "2",
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = password
            },
            new AppUser
            {
                Id = "3",
                FirstName = "User",
                LastName = "User",
                UserName = "user",
                NormalizedUserName = "USER",
                PasswordHash = password
            }
        };

        return users;
    }

    private static ICollection<AppRole> CreateRoles()
    {
        var roles = new List<AppRole>
        {
            new AppRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            }
        };

        return roles;
    }

    private static ICollection<IdentityUserRole<string>> CreateUsersRoles()
    {
        var usersRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                UserId = "1",
                RoleId = "1"
            },
            new IdentityUserRole<string>
            {
                UserId = "2",
                RoleId = "1"
            },
            new IdentityUserRole<string>
            {
                UserId = "3",
                RoleId = "2"
            }
        };

        return usersRoles;
    }

    #endregion

}