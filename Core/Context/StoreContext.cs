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
                    CategoryName = "Smart Watches"
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

            #region Mobiles Seeding
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung A-52 128GB ",
                    Description = "Samsung A-52 with 128GB,Dual SIM,6 GB RAM ,Black and 1 year Warranty ",
                    Manufacturer = "Samsung",
                    Price = 6999,
                    PictureUrl = "images/Mobiles/01-A52.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Iphone 13 Pro Max",
                    Description = "Iphone 13 Pro Max with 256GB,Dual SIM,6 GB RAM ,weight 400gm ,Blue and 1 year Warranty ",
                    Manufacturer = "Apple",
                    Price = 27000,
                    PictureUrl = "images/Mobiles/02-Iphone13.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Lenovo K-13",
                    Description = "Lenovo K-13 with 5000 mAh Battery,Dual SIM,2 GB RAM ,Blue and 1 year Warranty ",
                    Manufacturer = "Lenovo",
                    Price = 3000,
                    PictureUrl = "images/Mobiles/03-lenovo-k13.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Oppo A55",
                    Description = "Oppo A55 with 128GB,Dual SIM,4 GB RAM ,Rainbow Blue and 1 year Warranty ",
                    Manufacturer = "Oppo",
                    Price = 5500,
                    PictureUrl = "images/Mobiles/04-Oppo-A55.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Xiaomi Redmi 10C",
                    Description = "Xiaomi Redmi 10C with 64GB,Dual SIM, Fast charging 18W ,4 GB RAM ,Black and 1 year Warranty ",
                    Manufacturer = "Xiaomi",
                    Price = 4100,
                    PictureUrl = "images/Mobiles/05-Redmi-10C.jpg",
                    CategoryId = 1
                },
            #endregion

            #region Laptops Seeding
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dell Vostro 3510",
                    Description = "Dell Vostro 3510 laptop - 11th Gen Intel core i7-1165G7, 16GB RAM, 1TB HDD + 256GB SSD, Nvidia GeForce MX350 GDDR5 Graphics, '15.6' FHD(1920 x 1080) Anti - glare,Carbon Black",
                    Manufacturer = "Dell",
                    Price = 15900,
                    PictureUrl = "images/Laptops/01-Dell_Vostoro.png",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "MacBook Pro Late",
                    Description = "Apple MacBook Pro Late 2020 MYD82 Model with Touch Bar And ID ",
                    Manufacturer = "Apple",
                    Price = 29100,
                    PictureUrl = "images/Laptops/02-Mac.png",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Lenovo V15",
                    Description = "Lenovo V15-ADA 82C7007YED Laptop - AMD 3020e, 4 GB RAM, 1TB HDD, Integrated AMD Radeon Graphics, 15.6 HD(1366x768) TN 220nits Anti - glare,Dos – Iron Grey",
                    Manufacturer = "Lenovo",
                    Price = 5460,
                    PictureUrl = "images/Laptops/03-lenovo.png",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "HP 15-Laptop",
                    Description = "HP 15-DY2095WM Laptop - 11th Intel core i5-1135G7, 8GB RAM , 256GB SSD, Intel Iris Xe Graphics, 15.6 FHD(1920 x 1080) micro - edge anti - glare 250 nits,Windows 11 - Natural Silver",
                    Manufacturer = "HP",
                    Price = 12500,
                    PictureUrl = "images/Laptops/04-Hp.png",
                    CategoryId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alienware M15",
                    Description = "Alienware m15 Ryzen™ Edition R5 Gaming Laptop, AMD Ryzen R7 5800H 8 Core, 15.6 FHD 165Hz Display,Nvidia RTX 3060 6GB GDDR6,256GB SSD,16G DDR4 Ram,English AlienFX RGB keyboard,Win 10 Home",
                    Manufacturer = "Alienware",
                    Price = 30700,
                    PictureUrl = "images/Laptops/05-alien.png",
                    CategoryId = 2
                },
            #endregion

            #region Smart Watches Seeding
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Xiaomi Mi Lite",
                    Description = "Xiaomi Mi Lite Waterproof Smart Watch with Built-in GPS and Heart Rate Monitoring - Black",
                    Manufacturer = "Xiaomi",
                    Price = 1270,
                    PictureUrl = "images/Watches/01-Xiaomi.png",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "HW56 Plus Smart Watch",
                    Description = "HW56 Plus Smart Watch 1.77 Inch Curved Touch Screen, Silicone Band, Wireless Charging, Bluetooth Call, Health Monitor, Fitness Tracker, Support Arabic Language For Android & IOS- Black",
                    Manufacturer = "Huawei",
                    Price = 640,
                    PictureUrl = "images/Watches/02-huawei.png",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "SAMSUNG Galaxy Watch4",
                    Description = "SAMSUNG Galaxy Watch4 44mm Bluetooth Smartwatch, Black, wireless-Charging",
                    Manufacturer = "Samsung",
                    Price = 4050,
                    PictureUrl = "images/Watches/03-sam.png",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Imilab KW66 Smartwatch",
                    Description = "Imilab KW66 Smartwatch, Arabic Support, IP68, Long Battery Black",
                    Manufacturer = "Imilab",
                    Price = 777,
                    PictureUrl = "images/Watches/04-im.png",
                    CategoryId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Joyroom Jr-ft1",
                    Description = "Joyroom Jr-ft1 Waterproof Smart Watch with Silicone Strap - Grey",
                    Manufacturer = "Joyroom",
                    Price = 478,
                    PictureUrl = "images/Watches/05-joy.png",
                    CategoryId = 3
                },
            #endregion

            #region Tablets Seeding
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Nokia T20 Android Tablet",
                    Description = "Nokia T20 Android Tablet (4G + Wi-Fi) with 10.36 WUXGA Display,4GB RAM + 64GB ROM,Android 11 - Blue",
                    Manufacturer = "Nokia",
                    Price = 4199,
                    PictureUrl = "images/Tablets/01-nokia.png",
                    CategoryId = 4
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple iPad mini 2021",
                    Description = "New 2021 Apple iPad mini (8.3-inch, Wi-Fi, 64GB) - Pink (6th Generation)",
                    Manufacturer = "Apple",
                    Price = 11999,
                    PictureUrl = "images/Tablets/03-ipad.png",
                    CategoryId = 4
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "HUAWEI MatePad",
                    Description = "HUAWEI MatePad 10.4-inch 2022 Tablet + Keyboard, 2K Display, 7250 mAh Battery, Four large-amplitude speakers, All-round noise cancellation, LTE, 4GB+128GB, Grey",
                    Manufacturer = "Huawei",
                    Price = 7399,
                    PictureUrl = "images/Tablets/02-huawei.png",
                    CategoryId = 4
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alcatel 1T",
                    Description = "Alcatel 1T 9009G Tablet, 7 Inch, 16GB, 1GB RAM, 3G, Prime Black with Flip Cover",
                    Manufacturer = "Alcatel",
                    Price = 1440,
                    PictureUrl = "images/Tablets/04-alcatel.png",
                    CategoryId = 4
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung Galaxy Tab",
                    Description = "Samsung Galaxy Tab A7 Lite - 8.7 Inches, 32 GB, 3 GB RAM, 4G LTE - Grey",
                    Manufacturer = "Samsung",
                    Price = 4200,
                    PictureUrl = "images/Tablets/05-sam.png",
                    CategoryId = 4
                }
            #endregion

            );

            #endregion

        }

    }
}
