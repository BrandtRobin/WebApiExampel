using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using MySql.Data.Entity;

namespace WebApplication5.Models
{
    public class User
    {

        public User()
        {
            CreatedDate = DateTime.Today;
        }

        public long UserId { get; set; }
        [StringLength(65)]
        [Index(IsUnique = true)]
        [Required]
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Role> Roles { get; set; }
    }

    [NotMapped]
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                            .HasMany(e => e.Roles)
                            .WithMany(e => e.Users)
                            .Map(m => m.ToTable("Role_User").MapLeftKey("Users_UserId").MapRightKey("Roles_RoleId"));

            modelBuilder.Entity<Token>()
                            .HasKey(e => e.TokenString)
                            .HasRequired(e => e.User);
        }
    }
}