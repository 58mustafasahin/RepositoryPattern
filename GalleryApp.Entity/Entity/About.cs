using AppCore.Entities;

namespace GalleryApp.Entity.Entity
{
    /// <summary>
    /// Hakkımızda bilgileri
    /// </summary>
    public class About : BaseEntity
    {
        /// <summary>
        /// Hakkımızda kaydı id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// hakkımızda visyon 
        /// </summary>
        public string Vision { get; set; }

        /// <summary>
        /// hakkımızda misyon 
        /// </summary>
        public string Mission { get; set; }

        /// <summary>
        /// hakkımızda tarihçe
        /// </summary>
        public string History { get; set; }

    }
}
