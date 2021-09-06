using GalleryApp.Entity.Dtos.Slider;
using GalleryApp.Entity.Entity.ImageEntities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.Business.Abstract
{
    /// <summary>
    /// Slider İşlemleri
    /// </summary>
    public interface ISliderService
    {
        /// <summary>
        /// Id ye göre Slider Getirir.
        /// </summary>
        Task<GetSliderDto> GetSliderAsync(int sliderId);

        /// <summary>
        /// Tüm Slider Listeler.
        /// </summary>
        Task<List<GetListSliderDto>> GetAllSliderListAsync();

        /// <summary>
        /// Yeni Slider ekler.
        /// </summary>
        Task<int> AddSliderAsync(AddSliderDto addSliderDto);

        /// <summary>
        /// Slider Günceller.
        /// </summary>
        Task<int> UpdateSliderAsync(int sliderId, UpdateSliderDto updateSliderDto);

        /// <summary>
        /// Id ye göre Slider Siler. 
        /// </summary>
        Task<int> DeleteSlider(int sliderId);


        /// <summary>
        /// Slider resim ekleme
        /// </summary>
        Task<int> AddSliderImageAsync(int sliderId, IFormFile file);

        /// <summary>
        /// sliderId ye göre Slider resimi getirme
        /// </summary>
        Task<SliderImage> GetSliderImage(int sliderId);

        /// <summary>
        /// sliderId ye göre Slider resim güncelleme 
        /// </summary>
        Task<int> UpdateSliderImageAsync(int sliderId, IFormFile file);

        /// <summary>
        /// sliderId ye göre Slider resmi silme
        /// </summary>
        Task<int> DeleteSliderImageAsync(int sliderId);

        /// <summary>
        /// Slider listeler - Resimleriyle birlikte
        /// </summary>
        /// <returns></returns>
        //Task<List<GetListSliderDto>> GetAllSliderImageAsync();


    }
}
