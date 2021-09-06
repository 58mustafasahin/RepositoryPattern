using AppCore.Data;
using GalleryApp.Entity.Dtos.Car;
using GalleryApp.Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Abstract
{
    /// <summary>
    /// Araç Repo işlemleri
    /// </summary>
    public interface ICarRepo : IBaseRepository<Car>
    {
        /// <summary>
        /// Araç Listeler
        /// </summary>
        Task<List<GetListCarDto>> GetAllCarListAsync();

        /// <summary>
        /// Id ye göre Araç getirir
        /// </summary>
        Task<GetCarDto> GetCarAsync(int carId);

        /// <summary>
        /// Id ye göre Araç güncelleme
        /// </summary>
        void UpdateCar(int carId, UpdateCarDto updateCarDto);
    }
}
