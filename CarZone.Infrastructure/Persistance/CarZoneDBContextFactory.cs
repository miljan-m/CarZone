using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarZone.Infrastructure.Persistance
{
    class CarZoneDBContextFactory : IDesignTimeDbContextFactory<CarZoneDBContext>
    {
        public CarZoneDBContext CreateDbContext(string[] args)
        {   
            var options=new DbContextOptionsBuilder<CarZoneDBContext>();
            options.UseSqlServer("Server=DESKTOP-QLQGED2\\SQLEXPRESS01;Database=CarZone;Trusted_Connection=True;TrustServerCertificate=True");
            return new CarZoneDBContext(options.Options);
        }
    }
}