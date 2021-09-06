using MongoDB.Bson;

namespace GalleryApp.Entity.Entity.ImageEntities
{
    /// <summary>
    /// About Resim 
    /// </summary>
    public class AboutImage
    {
        /// <summary>
        /// Mongo Db - About resim Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// About Id
        /// </summary>
        public int AboutId { get; set; }

        /// <summary>
        /// About resim içeriği
        /// </summary>
        public byte[] Content { get; set; }
    }
}
