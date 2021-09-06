using AppCore.Utilities.Mapper;
using GalleryApp.Business.Abstract;
using GalleryApp.DataAccess.MongoDB;
using GalleryApp.DataAccess.UOW;
using GalleryApp.Entity.Dtos.Slider;
using GalleryApp.Entity.Entity;
using GalleryApp.Entity.Entity.ImageEntities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GalleryApp.Business.Concrete
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public readonly MongoDbContext _mongoDbContext;
        public SliderService(IUnitOfWork unitOfWork, IMapper mapper, MongoDbContext mongoDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<GetSliderDto> GetSliderAsync(int sliderId)
        {
            return await _unitOfWork.Sliders.GetSliderAsync(sliderId);
        }

        public async Task<List<GetListSliderDto>> GetAllSliderListAsync()
        {
            return await _unitOfWork.Sliders.GetAllSliderListAsync();
        }

        public async Task<int> AddSliderAsync(AddSliderDto addSliderDto)
        {
            var slider = _mapper.Map<AddSliderDto, Slider>(addSliderDto);
            await _unitOfWork.Sliders.AddAsync(slider);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateSliderAsync(int sliderId, UpdateSliderDto updateSliderDto)
        {
            var slider = _unitOfWork.Sliders.GetById(sliderId);
            if (slider == null)
            {
                return await Task.FromResult(-1);
            }
            else if (slider.IsDeleted == true)
            {
                return await Task.FromResult(-1);
            }
            _unitOfWork.Sliders.UpdateSlider(sliderId, updateSliderDto);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteSlider(int sliderId)
        {
            _unitOfWork.Sliders.Delete(sliderId);
            return await _unitOfWork.SaveAsync();  //silinemezse 0 döner
        }


        public async Task<SliderImage> GetSliderImage(int sliderId)
        {

            var sliderImageCollection = _mongoDbContext.GetCollection<SliderImage>();
            return await sliderImageCollection.Find(s => s.SliderId == sliderId).FirstOrDefaultAsync();
        }

        public async Task<int> AddSliderImageAsync(int sliderId, IFormFile file)
        {
            var sliderImageCollection = _mongoDbContext.GetCollection<SliderImage>();
            var sliderObject = await _unitOfWork.Sliders.GetSliderAsync(sliderId);
            var image = await sliderImageCollection.Find(s => s.SliderId == sliderId).FirstOrDefaultAsync();
            if (sliderObject == null)
            {
                return await Task.FromResult(-1);  // slider id ye ait slider yoksa
            }
            else if (image != null)
            {
                return await Task.FromResult(-2);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var sliderImage = new SliderImage()
                {
                    Id = ObjectId.GenerateNewId(),
                    //Eğer image resize kullanıyorsan Content = newImageByte olarak eşitle
                    Content = memoryStream.ToArray(),
                    SliderId = sliderId
                };
                sliderImageCollection.InsertOne(sliderImage);
            }
            return await Task.FromResult(1);
        }

        public async Task<int> UpdateSliderImageAsync(int sliderId, IFormFile file)
        {
            var sliderImageCollection = _mongoDbContext.GetCollection<SliderImage>();
            var slider = await sliderImageCollection.Find(s => s.SliderId == sliderId).FirstOrDefaultAsync();
            if (slider == null)
            {
                return await Task.FromResult(-1);  // id bulunamadı
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                slider.Content = memoryStream.ToArray();
                var updatedSliderImage = await sliderImageCollection.ReplaceOneAsync(s => s.SliderId == sliderId, slider);
                if (updatedSliderImage.IsAcknowledged)
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    return await Task.FromResult(-2);
                }
            }
        }

        public async Task<int> DeleteSliderImageAsync(int sliderId)
        {
            var sliderImageCollection = _mongoDbContext.GetCollection<SliderImage>();
            var sliderImage = await GetSliderImage(sliderId);

            if (sliderImage == null)
            {
                return await Task.FromResult(-1);
            }

            var deletedSliderImage = await sliderImageCollection.DeleteOneAsync(p => p.SliderId == sliderImage.SliderId);
            if (deletedSliderImage.IsAcknowledged)  //silindiyse
            {
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(-2);
            }
        }

        //public async Task<List<GetListSliderDto>> GetAllSliderImageAsync()
        //{
        //    var sliderImageCollection = _mongoDbContext.GetCollection<SliderImage>();
        //    var listImage = await sliderImageCollection.Find(u => true).ToListAsync();
        //    var listSlider = await GetAllSliderListAsync();

        //    for (var i = 0; i < listImage.Count; i++)
        //    {
        //        var imagecontent = listImage[i].Content;
        //        if (listImage[i].SliderId == listSlider[i].Id)
        //        {
        //            listSlider[i].Content = imagecontent;
        //        }

        //    }
        //    return listSlider;
        //}
    }
}
