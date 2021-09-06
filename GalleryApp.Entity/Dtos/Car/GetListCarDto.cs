using AppCore.Entities;

namespace GalleryApp.Entity.Dtos.Car
{
    public class GetListCarDto : IDto
    {
        /// <summary>
        /// Araç id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Araç marka 
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Araç model 
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Araç yılı
        /// </summary>
        public string ModelYear { get; set; }

        /// <summary>
        /// Araç km 
        /// </summary>
        public string Km { get; set; }

        /// <summary>
        /// Araç yakıt türü 
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// Araç açıklama 
        /// </summary>
        public string Desc { get; set; }
    }
}
