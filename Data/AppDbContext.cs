using Microsoft.EntityFrameworkCore;
using HRWebApp.Models;

namespace HRWebApp.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed: Organizations
            modelBuilder.Entity<Organization>().HasData(
                new Organization { Id = 1, Name = "Midis" },
                new Organization { Id = 2, Name = "Robologic" }
            );

            // Seed: Professions
            modelBuilder.Entity<Profession>().HasData(
                new Profession { Id = 1, Name = "Software Engineer" },
                new Profession { Id = 2, Name = "PLC Programmer" },
                new Profession { Id = 3, Name = "Electrician" }
            );

            // Seed: Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Manager" },
                new Category { Id = 2, Name = "Skilled Worker" },
                new Category { Id = 3, Name = "Helper" }
            );

            // Seed: Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Nikita",
                    LastName = "Komars",
                    HireDate = new DateTime(2022, 6, 1),
                    Status = "Active",
                    OrganizationId = 2,
                    ProfessionId = 2,
                    CategoryId = 2
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Marija",
                    LastName = "Esenina",
                    HireDate = new DateTime(2024, 1, 15),
                    Status = "Candidate",
                    OrganizationId = 1,
                    ProfessionId = 1,
                    CategoryId = 2
                }
            );
        }
    }
}
