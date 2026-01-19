using Microsoft.EntityFrameworkCore;
using CarZone.Domain.Models;

namespace CarZone.Infrastructure.Persistance
{
    public class CarZoneDBContext : DbContext
    {
        public CarZoneDBContext(DbContextOptions<CarZoneDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user1=new User(1,"Miljan","Mitic","mm@gmail.com","0668049057","Milutina Stojanovica 14");
            modelBuilder.Entity<User>().HasData(user1);
        }
       
    }
}