using AppCore.Data;
using GalleryApp.Entity.Dtos.Slider;
using GalleryApp.Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Abstract
{
    /// <summary>
    /// Slider Repo işlemleri
    /// </summary>
    public interface ISliderRepo : IBaseRepository<Slider>
    {
        /// <summary>
        /// Slider Listeler
        /// </summary>
        Task<List<GetListSliderDto>> GetAllSliderListAsync();

        /// <summary>
        /// Id ye göre Slider getirir
        /// </summary>
        Task<GetSliderDto> GetSliderAsync(int sliderId);

        /// <summary>
        /// Id ye göre Slider güncelleme
        /// </summary>
        void UpdateSlider(int sliderId, UpdateSliderDto updateSliderDto);
    }
}
