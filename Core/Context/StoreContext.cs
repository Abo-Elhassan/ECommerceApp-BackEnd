using Microsoft.EntityFrameworkCore;
using Core.Entities;
namespace Core.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seeding Customer
            modelBuilder.Entity<Customer>().HasData(

                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Youssef",
                       LastName = "Hassan",
                       Address = " Mostakbal",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "Youssef@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Medhat",
                       LastName = "Saleh",
                       Address = " NasrCity",
                       Phone = "01278956883",
                       City = "Cairo",
                       Email = "Medhat@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Mohamed",
                       LastName = "Ahmed",
                       Address = " Elsalam",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "Mohamed@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Mostafa",
                       LastName = "Riyad",
                       Address = " Faisal",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "Mostafa@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Alaa",
                       LastName = "Abo EL-Hassan",
                       Address = " Sabah",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "Alaa@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Hala",
                       LastName = "Elsayed",
                       Address = " suez",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "hala@joe.com"
                   },
                   new Customer
                   {
                       CustomerId = Guid.NewGuid(),
                       FirstName = "Salma",
                       LastName = "Hozein",
                       Address = " suez",
                       Phone = "01278956883",
                       City = "Suez",
                       Email = "Salma@joe.com"
                   });
            #endregion

            #region Seeding Categories

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Mobiles"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Laptops"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "PCs"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Tablets"
                }
            );


            #endregion

            #region Seeding Products

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id= Guid.NewGuid(),
                    Name="Product 1",
                    Description="Description for product 1",
                    Price=1000,
                    PictureUrl="This is Url for Product 1",
                    CategoryId=1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for product 2",
                    Price = 2000,
                    PictureUrl = "This is Url for Product 2",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description for product 3",
                    Price = 3000,
                    PictureUrl = "This is Url for Product 3",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Description = "Description for product 4",
                    Price = 4000,
                    PictureUrl = "This is Url for Product 4",
                    CategoryId = 4
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 5",
                    Description = "Description for product 5",
                    Price = 5000,
                    PictureUrl = "This is Url for Product 5",
                    CategoryId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 6",
                    Description = "Description for product 6",
                    Price = 6000,
                    PictureUrl = "This is Url for Product 6",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 7",
                    Description = "Description for product 7",
                    Price = 7000,
                    PictureUrl = "This is Url for Product 7",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 8",
                    Description = "Description for product 8",
                    Price = 8000,
                    PictureUrl = "This is Url for Product 8",
                    CategoryId = 4
                }
                );

            #endregion

        }

    }
}
