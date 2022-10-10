using EverybodyCodes.Domain.Repositories;

namespace EverybodyCodes.Infrastructure.Repository
{
    public class CameraRepository : BaseRepository<Domain.Entities.Camera>, ICameraRepository
    {
        public CameraRepository(CameraDbContext cameraDbContext) : base(cameraDbContext)
        {

        }
    }
}
