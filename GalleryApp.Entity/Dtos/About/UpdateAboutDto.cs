using AppCore.Entities;

namespace GalleryApp.Entity.Dtos.About
{
    public class UpdateAboutDto : IDto
    {
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
