using AppCore.Entities;

namespace GalleryApp.Entity.Dtos.Slider
{
    public class UpdateSliderDto : IDto
    {
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
