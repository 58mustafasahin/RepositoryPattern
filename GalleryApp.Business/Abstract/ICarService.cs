using GalleryApp.Entity.Dtos.Car;
using GalleryApp.Entity.Entity.ImageEntities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.Business.Abstract
{
    /// <summary>
    /// Araç İşlemleri
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Id ye göre Araç Getirir.
        /// </summary>
        Task<GetCarDto> GetCarByIdAsync(int carId);

        /// <summary>
        /// Tüm Araç Listeler.
        /// </summary>
        Task<List<GetListCarDto>> GetAllCarListAsync();

        /// <summary>
        /// Yeni Araç ekler.
        /// </summary>
        Task<int> AddCarAsync(AddCarDto addCarDto);

        /// <summary>
        /// Araç Günceller.
        /// </summary>
        Task<int> UpdateCarAsync(int carId, UpdateCarDto updateCarDto);

        /// <summary>
        /// Id ye göre Araç Siler. 
        /// </summary>
        Task<int> DeleteCar(int carId);

        //--------------------------------------------------------------------------------
        //-----------------------kapak--------------------

        /// <summary>
        /// Araç kapak resim ekleme
        /// </summary>
        Task<int> AddCarCoverImageAsync(int carId, IFormFile file);

        /// <summary>
        /// carId ye göre Araç kapak resimi getirme
        /// </summary>
        Task<CarCoverImage> GetCarCoverImage(int carId);

        /// <summary>
        /// carId ye göre Araç kapak resim güncelleme 
        /// </summary>
        Task<int> UpdateCarCoverImageAsync(int carId, IFormFile file);

        /// <summary>
        /// carId ye göre Araç kapak resmi silme
        /// </summary>
        Task<int> DeleteCarCoverImageAsync(int carId);

        //--------------------------------------------------------------------------------
        //-----------------------galeri--------------------

        /// <summary>
        /// Id ye göre araçların objectId lerini getirme
        /// </summary>
        Task<List<string>> GetCarImagesIdList(int id);

        /// <summary>
        /// Araç resim ekleme
        /// </summary>
        Task<int> AddCarImageAsync(int carId, IFormFile file);

        /// <summary>
        /// carId ye göre Araç resimi getirme
        /// </summary>
        Task<byte[]> GetCarImage(string carId);

        /// <summary>
        /// carId ye göre Araç resim güncelleme 
        /// </summary>
        Task<int> UpdateCarImageAsync(string carId, IFormFile file);

        /// <summary>
        /// carId ye göre Araç resmi silme
        /// </summary>
        Task<int> DeleteCarImageAsync(string carId);
    }
}
