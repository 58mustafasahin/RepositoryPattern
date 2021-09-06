using AppCore.Entities;

namespace GalleryApp.Entity.Entity
{
    /// <summary>
    /// iletişim form bilgileri 
    /// </summary>
    public class Contact : BaseEntity
    {
        /// <summary>
        /// iletişim id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// iletişim ad 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// iletişim eposta 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// iletişim telefon 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// iletişim konu 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// iletişim mesaj 
        /// </summary>
        public string Message { get; set; }
    }
}
