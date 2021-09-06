using GalleryApp.Entity.Dtos.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.Business.Abstract
{
    /// <summary>
    /// İletişim işlemleri
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Id ye göre İletişim Getirir.
        /// </summary>
        Task<GetContactDto> GetContactAsync(int contactId);

        /// <summary>
        /// Tüm İletişim Listeler.
        /// </summary>
        Task<IEnumerable<GetListContactDto>> GetAllContactListAsync();

        /// <summary>
        /// Yeni İletişim Ekler.
        /// </summary>
        Task<int> AddContactAsync(AddContactDto addContactDto);

        /// <summary>
        /// İletişim Günceller.
        /// </summary>
        Task<int> UpdateContactAsync(int contactId, UpdateContactDto updateContactDto);

        /// <summary>
        /// Id ye göre İletişim Siler.
        /// </summary>
        Task<int> DeleteContact(int contactId);
    }
}
