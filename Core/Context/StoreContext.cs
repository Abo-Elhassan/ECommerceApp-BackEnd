using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Core.Context
{
    public class StoreContext : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<DeliveryMethod> DeliveryMethods => Set<DeliveryMethod>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region DB Tables Name Configutaion
            modelBuilder.Entity<Customer>().ToTable("User");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<Product>().Property(b => b.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DeliveryMethod>().Property(b => b.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(b => b.Subtotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(b => b.Price).HasColumnType("decimal(18,2)");
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
                    Manufacturer = "NIKE",
                    Price=1000,
                    PictureUrl="This is Url for Product 1",
                    CategoryId=1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for product 2",
                    Manufacturer = "HP",
                    Price = 2000,
                    PictureUrl = "This is Url for Product 2",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description for product 3",
                    Manufacturer = "Apple",
                    Price = 3000,
                    PictureUrl = "This is Url for Product 3",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Description = "Description for product 4",
                    Manufacturer = "Huawei",
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
                    Manufacturer = "LG",
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
                    Manufacturer = "Nokia",
                    Price = 7000,
                    PictureUrl = "This is Url for Product 7",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 8",
                    Description = "Description for product 8",
                    Manufacturer = "Samsung",
                    Price = 8000,
                    PictureUrl = "This is Url for Product 8",
                    CategoryId = 4
                }
                );

            #endregion

        }

    }
}
