using AppCore.Entities;

namespace GalleryApp.Entity.Dtos.Slider
{
    public class GetSliderDto : IDto
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
