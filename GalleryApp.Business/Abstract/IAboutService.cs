using GalleryApp.Entity.Dtos.About;
using GalleryApp.Entity.Entity.ImageEntities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.Business.Abstract
{
    /// <summary>
    /// Hakkımızda İşlemleri
    /// </summary>
    public interface IAboutService
    {
        /// <summary>
        /// Id ye göre Hakkımızda Getirir.
        /// </summary>
        Task<GetAboutDto> GetAboutById(int id);

        /// <summary>
        /// Tüm Hakkımızda Listeler.
        /// </summary>
        Task<IEnumerable<GetListAboutDto>> GetAboutList();

        /// <summary>
        /// Yeni Hakkımızda Ekler.
        /// </summary>
        Task<int> AddAbout(AddAboutDto addAbout);

        /// <summary>
        /// Hakkımızda Günceller.
        /// </summary>
        Task<int> UpdateAbout(int id, UpdateAboutDto updateAbout);

        /// <summary>
        /// Id ye göre Hakkımızda Siler.
        /// </summary>
        Task<int> DeleteAbout(int id);


        /// <summary>
        /// Hakkımızda resim ekleme
        /// </summary>
        Task<int> AddAboutImageAsync(int aboutId, IFormFile file);

        /// <summary>
        /// aboutId ye göre resimi getirme
        /// </summary>
        Task<AboutImage> GetAboutImage(int aboutId);

        /// <summary>
        /// aboutId ye göre resim güncelleme 
        /// </summary>
        Task<int> UpdateAboutImageAsync(int aboutId, IFormFile file);

        /// <summary>
        /// about resmi silme
        /// </summary>
        Task<int> DeleteAboutImageAsync(int aboutId);
    }
}