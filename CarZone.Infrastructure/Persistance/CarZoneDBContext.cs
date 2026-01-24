using Microsoft.EntityFrameworkCore;
using CarZone.Domain.Models;

namespace CarZone.Infrastructure.Persistance
{
    public class CarZoneDBContext : DbContext
    {
        public CarZoneDBContext(DbContextOptions<CarZoneDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user1 = new User(1, "Miljan", "Mitic", "mm@gmail.com", "0668049057", "Milutina Stojanovica 14", "mypassword");
            modelBuilder.Entity<User>().HasData(user1);

            modelBuilder.Entity<Model>()
                .HasOne(e => e.Brand)
                .WithMany(e => e.Models)
                .HasForeignKey(e => e.BrandId);

            modelBuilder.Entity<Listing>()
                .HasOne(e=>e.User)
                .WithMany(e=>e.PostedListings)
                .HasForeignKey(e=>e.UserId); 
            modelBuilder.Entity<Listing>()
                .HasOne(b=>b.Buyer)
                .WithMany(l=>l.BoughtListing)
                .HasForeignKey(b=>b.BuyerId);

            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, BrandName = "BMW" },
                new Brand { BrandId = 2, BrandName = "Audi" },
                new Brand { BrandId = 3, BrandName = "Mercedes-Benz" },
                new Brand { BrandId = 4, BrandName = "Volkswagen" },
                new Brand { BrandId = 5, BrandName = "Toyota" },
                new Brand { BrandId = 6, BrandName = "Honda" },
                new Brand { BrandId = 7, BrandName = "Ford" },
                new Brand { BrandId = 8, BrandName = "Opel" },
                new Brand { BrandId = 9, BrandName = "Peugeot" },
                new Brand { BrandId = 10, BrandName = "Renault" },
                new Brand { BrandId = 11, BrandName = "Skoda" },
                new Brand { BrandId = 12, BrandName = "Hyundai" },
                new Brand { BrandId = 13, BrandName = "Kia" },
                new Brand { BrandId = 14, BrandName = "Mazda" },
                new Brand { BrandId = 15, BrandName = "Nissan" },
                new Brand { BrandId = 16, BrandName = "Volvo" },
                new Brand { BrandId = 17, BrandName = "Fiat" },
                new Brand { BrandId = 18, BrandName = "Alfa Romeo" },
                new Brand { BrandId = 19, BrandName = "Porsche" },
                new Brand { BrandId = 20, BrandName = "Tesla" },
                new Brand { BrandId = 21, BrandName = "Ferrari" },
                new Brand { BrandId = 22, BrandName = "Lamborghini" },
                new Brand { BrandId = 23, BrandName = "Maserati" },
                new Brand { BrandId = 24, BrandName = "Bentley" },
                new Brand { BrandId = 25, BrandName = "Rolls-Royce" }
            );

            modelBuilder.Entity<Model>().HasData(
                // BMW
                new Model { ModelId = 1, ModelName = "Series 3", BrandId = 1 },
                new Model { ModelId = 2, ModelName = "Series 5", BrandId = 1 },
                new Model { ModelId = 3, ModelName = "X5", BrandId = 1 },
                // Audi
                new Model { ModelId = 4, ModelName = "A4", BrandId = 2 },
                new Model { ModelId = 5, ModelName = "A6", BrandId = 2 },
                new Model { ModelId = 6, ModelName = "Q7", BrandId = 2 },
                // Mercedes-Benz
                new Model { ModelId = 7, ModelName = "C-Class", BrandId = 3 },
                new Model { ModelId = 8, ModelName = "E-Class", BrandId = 3 },
                new Model { ModelId = 9, ModelName = "GLE", BrandId = 3 },
                // Volkswagen
                new Model { ModelId = 10, ModelName = "Golf", BrandId = 4 },
                new Model { ModelId = 11, ModelName = "Passat", BrandId = 4 },
                new Model { ModelId = 12, ModelName = "Tiguan", BrandId = 4 },
                // Toyota
                new Model { ModelId = 13, ModelName = "Corolla", BrandId = 5 },
                new Model { ModelId = 14, ModelName = "Camry", BrandId = 5 },
                new Model { ModelId = 15, ModelName = "RAV4", BrandId = 5 },
                // Honda
                new Model { ModelId = 16, ModelName = "Civic", BrandId = 6 },
                new Model { ModelId = 17, ModelName = "Accord", BrandId = 6 },
                new Model { ModelId = 18, ModelName = "CR-V", BrandId = 6 },
                // Ford
                new Model { ModelId = 19, ModelName = "Focus", BrandId = 7 },
                new Model { ModelId = 20, ModelName = "Mondeo", BrandId = 7 },
                new Model { ModelId = 21, ModelName = "Mustang", BrandId = 7 },
                // Opel
                new Model { ModelId = 22, ModelName = "Astra", BrandId = 8 },
                new Model { ModelId = 23, ModelName = "Insignia", BrandId = 8 },
                new Model { ModelId = 24, ModelName = "Corsa", BrandId = 8 },
                // Peugeot
                new Model { ModelId = 25, ModelName = "208", BrandId = 9 },
                new Model { ModelId = 26, ModelName = "308", BrandId = 9 },
                new Model { ModelId = 27, ModelName = "3008", BrandId = 9 },
                // Renault
                new Model { ModelId = 28, ModelName = "Clio", BrandId = 10 },
                new Model { ModelId = 29, ModelName = "Megane", BrandId = 10 },
                new Model { ModelId = 30, ModelName = "Kadjar", BrandId = 10 },
                // Skoda
                new Model { ModelId = 31, ModelName = "Octavia", BrandId = 11 },
                new Model { ModelId = 32, ModelName = "Superb", BrandId = 11 },
                new Model { ModelId = 33, ModelName = "Kodiaq", BrandId = 11 },
                // Hyundai
                new Model { ModelId = 34, ModelName = "i30", BrandId = 12 },
                new Model { ModelId = 35, ModelName = "Tucson", BrandId = 12 },
                new Model { ModelId = 36, ModelName = "Santa Fe", BrandId = 12 },
                // Kia
                new Model { ModelId = 37, ModelName = "Ceed", BrandId = 13 },
                new Model { ModelId = 38, ModelName = "Sportage", BrandId = 13 },
                new Model { ModelId = 39, ModelName = "Sorento", BrandId = 13 },
                // Mazda
                new Model { ModelId = 40, ModelName = "Mazda 3", BrandId = 14 },
                new Model { ModelId = 41, ModelName = "Mazda 6", BrandId = 14 },
                new Model { ModelId = 42, ModelName = "CX-5", BrandId = 14 },
                // Nissan
                new Model { ModelId = 43, ModelName = "Qashqai", BrandId = 15 },
                new Model { ModelId = 44, ModelName = "X-Trail", BrandId = 15 },
                new Model { ModelId = 45, ModelName = "Micra", BrandId = 15 },
                // Volvo
                new Model { ModelId = 46, ModelName = "S60", BrandId = 16 },
                new Model { ModelId = 47, ModelName = "XC60", BrandId = 16 },
                new Model { ModelId = 48, ModelName = "XC90", BrandId = 16 },
                // Fiat
                new Model { ModelId = 49, ModelName = "Punto", BrandId = 17 },
                new Model { ModelId = 50, ModelName = "500", BrandId = 17 },
                new Model { ModelId = 51, ModelName = "Tipo", BrandId = 17 },
                // Alfa Romeo
                new Model { ModelId = 52, ModelName = "Giulia", BrandId = 18 },
                new Model { ModelId = 53, ModelName = "Stelvio", BrandId = 18 },
                // Porsche
                new Model { ModelId = 54, ModelName = "911", BrandId = 19 },
                new Model { ModelId = 55, ModelName = "Cayenne", BrandId = 19 },
                // Tesla
                new Model { ModelId = 56, ModelName = "Model S", BrandId = 20 },
                new Model { ModelId = 57, ModelName = "Model 3", BrandId = 20 },
                new Model { ModelId = 58, ModelName = "Model Y", BrandId = 20 },
                // Ferrari
                new Model { ModelId = 59, ModelName = "F8 Tributo", BrandId = 21 },
                new Model { ModelId = 60, ModelName = "Roma", BrandId = 21 },
                // Lamborghini
                new Model { ModelId = 61, ModelName = "Huracan", BrandId = 22 },
                new Model { ModelId = 62, ModelName = "Aventador", BrandId = 22 },
                // Maserati
                new Model { ModelId = 63, ModelName = "Ghibli", BrandId = 23 },
                new Model { ModelId = 64, ModelName = "Levante", BrandId = 23 },
                // Bentley
                new Model { ModelId = 65, ModelName = "Continental GT", BrandId = 24 },
                new Model { ModelId = 66, ModelName = "Bentayga", BrandId = 24 },
                // Rolls-Royce
                new Model { ModelId = 67, ModelName = "Ghost", BrandId = 25 },
                new Model { ModelId = 68, ModelName = "Phantom", BrandId = 25 },
                // BYD
                new Model { ModelId = 69, ModelName = "Atto 3", BrandId = 27 },
                new Model { ModelId = 70, ModelName = "Seal", BrandId = 27 }
             );
        }

    }
}