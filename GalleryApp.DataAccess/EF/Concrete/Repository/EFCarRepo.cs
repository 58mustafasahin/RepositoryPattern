using AppCore.Data.EF;
using GalleryApp.DataAccess.EF.Abstract;
using GalleryApp.Entity.Dtos.Car;
using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Concrete.Repository
{
    public class EFCarRepo : EFBaseRepository<Car>, ICarRepo
    {
        public EFCarRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<GetListCarDto>> GetAllCarListAsync()
        {
            return await GetListQueryable(p => !p.IsDeleted)
               .Select(c => new GetListCarDto
               {
                   Id = c.Id,
                   Brand = c.Brand,
                   Model = c.Model,
                   ModelYear=c.ModelYear,
                   Km=c.Km,
                   FuelType=c.FuelType,
                   Desc=c.Desc
               }).ToListAsync();
        }

        public async Task<GetCarDto> GetCarAsync(int carId)
        {
            return await GetListQueryable(p => !p.IsDeleted && p.Id == carId)
               .Select(c => new GetCarDto
               {
                   Id = c.Id,
                   Brand = c.Brand,
                   Model = c.Model,
                   ModelYear = c.ModelYear,
                   Km = c.Km,
                   FuelType = c.FuelType,
                   Desc = c.Desc
               }).FirstOrDefaultAsync();
        }

        public void UpdateCar(int carId, UpdateCarDto updateCarDto)
        {
            var updateCar = GetById(carId);

            updateCar.Brand = updateCarDto.Brand;
            updateCar.Model = updateCarDto.Model;
            updateCar.ModelYear = updateCarDto.ModelYear;
            updateCar.Km = updateCarDto.Km;
            updateCar.FuelType = updateCarDto.FuelType;
            updateCar.Desc = updateCarDto.Desc;
            updateCar.ModifiedDate = DateTime.Now;

            Update(updateCar);
        }
    }
}
