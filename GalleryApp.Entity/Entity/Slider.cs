using AppCore.Entities;

namespace GalleryApp.Entity.Entity
{
    /// <summary>
    /// Slider Bilgileri
    /// </summary>
    public class Slider : BaseEntity
    {
        /// <summary>
        /// Slider Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Slider Başlık
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Slider Metni
        /// </summary>
        public string Text { get; set; }
    }
}
