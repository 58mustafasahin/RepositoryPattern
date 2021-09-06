using AppCore.Data;
using GalleryApp.Entity.Dtos.Contact;
using GalleryApp.Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Abstract
{
    /// <summary>
    /// İletişim Repo işlemleri
    /// </summary>
    public interface IContactRepo : IBaseRepository<Contact>
    {
        /// <summary>
        /// İletişim Listeler
        /// </summary>
        Task<IEnumerable<GetListContactDto>> GetAllContactListAsync();

        /// <summary>
        /// Id ye göre İletişim getirir
        /// </summary>
        Task<GetContactDto> GetContactAsync(int id);

        /// <summary>
        /// Id ye göre İletişim güncelleme
        /// </summary>
        void UpdateContact(int contactId, UpdateContactDto updateContactDto);
    }
}
