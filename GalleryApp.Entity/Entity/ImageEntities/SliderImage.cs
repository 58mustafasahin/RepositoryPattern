using MongoDB.Bson;

namespace GalleryApp.Entity.Entity.ImageEntities
{
    /// <summary>
    /// Slider Resim 
    /// </summary>
    public class SliderImage
    {
        /// <summary>
        /// Mongo Db - Slider resim Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Slider Id
        /// </summary>
        public int SliderId { get; set; }

        /// <summary>
        /// Slider resim içeriği
        /// </summary>
        public byte[] Content { get; set; }
    }
}
