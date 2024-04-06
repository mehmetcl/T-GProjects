using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.DataAccessLayer.Concrete
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1",CategoryId = 1,Price=100,Description="Product 1 Desc" },
                new Product { Id = 2, Name = "Product 2", CategoryId = 2, Price = 120, Description = "Product 2 Desc" },
                new Product { Id = 3, Name = "Product 3", CategoryId = 2, Price = 130, Description = "Product 3 Desc" },
                new Product { Id = 4, Name = "Product 4", CategoryId = 3, Price = 250, Description = "Product 4 Desc" });
        }
    }

}


