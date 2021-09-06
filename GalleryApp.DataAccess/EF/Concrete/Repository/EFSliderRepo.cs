using AppCore.Data.EF;
using GalleryApp.DataAccess.EF.Abstract;
using GalleryApp.Entity.Dtos.Slider;
using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Concrete.Repository
{
    public class EFSliderRepo : EFBaseRepository<Slider>, ISliderRepo
    {
        public EFSliderRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<GetListSliderDto>> GetAllSliderListAsync()
        {
            return await GetListQueryable(p => !p.IsDeleted)
               .Select(s => new GetListSliderDto
               {
                   Id = s.Id,
                   Title = s.Title,
                   Text = s.Text
               }).ToListAsync();
        }

        public async Task<GetSliderDto> GetSliderAsync(int sliderId)
        {
            return await GetListQueryable(p => !p.IsDeleted && p.Id == sliderId)
               .Select(s => new GetSliderDto
               {
                   Id = s.Id,
                   Title = s.Title,
                   Text = s.Text
               }).FirstOrDefaultAsync();
        }

        public void UpdateSlider(int sliderId, UpdateSliderDto updateSliderDto)
        {
            var updateSlider = GetById(sliderId);

            updateSlider.Title = updateSliderDto.Title;
            updateSlider.Text = updateSliderDto.Text;

            updateSlider.ModifiedDate = DateTime.Now;

            Update(updateSlider);
        }
    }
}
