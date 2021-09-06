using AppCore.Data.EF;
using GalleryApp.DataAccess.EF.Abstract;
using GalleryApp.Entity.Dtos.About;
using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Concrete.Repository
{
    public class EFAboutRepo : EFBaseRepository<About>, IAboutRepo
    {
        public EFAboutRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<GetListAboutDto>> AboutList()
        {
            return await GetListQueryable(p => !p.IsDeleted)
                .Select(p => new GetListAboutDto
                {
                    Id = p.Id,
                    Vision = p.Vision,
                    Mission = p.Mission,
                    History = p.History

                }).ToListAsync();
        }

        public async Task<GetAboutDto> AboutGetById(int id)
        {
            return await GetQueryable(p => !p.IsDeleted && p.Id == id)
                .Select(p => new GetAboutDto
                {
                    Vision = p.Vision,
                    Mission = p.Mission,
                    History = p.History
                }).SingleOrDefaultAsync();
        }

        public async Task UpdateAbout(int id, UpdateAboutDto updateAbout)
        {
            var currentAbout = await GetByIdAsync(id);
            Update(currentAbout);
            currentAbout.Vision = updateAbout.Vision;
            currentAbout.Mission = updateAbout.Mission;
            currentAbout.History = updateAbout.History;
        }
    }
}
