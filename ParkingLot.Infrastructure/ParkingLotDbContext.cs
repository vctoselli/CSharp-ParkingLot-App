using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingLot.Domain.Entities;

namespace ParkingLot.Infrastructure
{
    public class ParkingLotDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<VehicleInfo> VehicleInfo { get; set; }

        public ParkingLotDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var typeDatabase = _configuration["TypeDatabase"];
            var connectionString = _configuration.GetConnectionString(name:"SqlServer");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
