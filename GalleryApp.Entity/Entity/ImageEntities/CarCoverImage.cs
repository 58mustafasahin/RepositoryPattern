using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalleryApp.Entity.Entity.ImageEntities
{
    /// <summary>
    /// Araç Kapak Resim 
    /// </summary>
    public class CarCoverImage
    {
        /// <summary>
        /// Mongo Db - Araç Kapak resim Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Araç Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Araç Kapak resim içeriği
        /// </summary>
        public byte[] Content { get; set; }
    }
}
