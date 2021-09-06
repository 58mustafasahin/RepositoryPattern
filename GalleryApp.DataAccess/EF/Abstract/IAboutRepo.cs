using AppCore.Data;
using GalleryApp.Entity.Dtos.About;
using GalleryApp.Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Abstract
{
    /// <summary>
    /// Hakkımızda Repo işlemleri
    /// </summary>
    public interface IAboutRepo : IBaseRepository<About>
    {
        /// <summary>
        /// Hakkımızda Listeler
        /// </summary>
        Task<List<GetListAboutDto>> AboutList();

        /// <summary>
        /// Id ye göre Hakkımızda getirir
        /// </summary>
        Task<GetAboutDto> AboutGetById(int id);

        /// <summary>
        /// Id ye göre Hakkımızda güncelleme
        /// </summary>
        Task UpdateAbout(int id, UpdateAboutDto updateAbout);
    }
}
