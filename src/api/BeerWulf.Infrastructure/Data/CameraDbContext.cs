using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EverybodyCodes.Infrastructure
{
    public class CameraDbContext : DbContext
    {
        public CameraDbContext(DbContextOptions<CameraDbContext> options) : base(options) { }
        public DbSet<Domain.Entities.Camera> Cameras { get; set; }

    }
}
