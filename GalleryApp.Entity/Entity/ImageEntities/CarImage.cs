using MongoDB.Bson;

namespace GalleryApp.Entity.Entity.ImageEntities
{
    /// <summary>
    /// Araç Resim 
    /// </summary>
    public class CarImage
    {
        /// <summary>
        /// Mongo Db - Araç resim Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Araç Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Araç resim içeriği
        /// </summary>
        public byte[] Content { get; set; }
    }
}
