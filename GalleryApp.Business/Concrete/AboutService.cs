using AppCore.Utilities.Mapper;
using GalleryApp.Business.Abstract;
using GalleryApp.DataAccess.MongoDB;
using GalleryApp.DataAccess.UOW;
using GalleryApp.Entity.Dtos.About;
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
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public readonly MongoDbContext _mongoDbContext;

        public AboutService(IUnitOfWork unitOfWork, IMapper mapper, MongoDbContext mongoDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<GetAboutDto> GetAboutById(int id)
        {
            return await _unitOfWork.Abouts.AboutGetById(id);
        }

        public async Task<IEnumerable<GetListAboutDto>> GetAboutList()
        {
            return await _unitOfWork.Abouts.AboutList();
        }

        public async Task<int> AddAbout(AddAboutDto addAbout)
        {
            var about = _mapper.Map<AddAboutDto, About>(addAbout);
            await _unitOfWork.Abouts.AddAsync(about);
            return await _unitOfWork.SaveAsync();
        }
        public async Task<int> UpdateAbout(int id, UpdateAboutDto updateAbout)
        {
            var result = _unitOfWork.Abouts.GetById(id);
            if (result == null)
            {
                return await Task.FromResult(-1);
            }
            await _unitOfWork.Abouts.UpdateAbout(id, updateAbout);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAbout(int id)
        {
            _unitOfWork.Abouts.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<AboutImage> GetAboutImage(int aboutId)
        {
            var aboutImageCollection = _mongoDbContext.GetCollection<AboutImage>();
            return await aboutImageCollection.Find(s => s.AboutId == aboutId).FirstOrDefaultAsync();
        }

        public async Task<int> AddAboutImageAsync(int aboutId, IFormFile file)
        {
            var aboutImageCollection = _mongoDbContext.GetCollection<AboutImage>();
            var aboutObject = await _unitOfWork.Abouts.AboutGetById(aboutId);
            var image = await aboutImageCollection.Find(s => s.AboutId == aboutId).FirstOrDefaultAsync();
            if (aboutObject == null)
            {
                return await Task.FromResult(-1);
            }
            else if (image != null)
            {
                return await Task.FromResult(-2);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var aboutImage = new AboutImage()
                {
                    Id = ObjectId.GenerateNewId(),
                    Content = memoryStream.ToArray(),
                    AboutId = aboutId
                };
                aboutImageCollection.InsertOne(aboutImage);
            }
            return await Task.FromResult(1);
        }
        
        public async Task<int> UpdateAboutImageAsync(int aboutId, IFormFile file)
        {
            var aboutImageCollection = _mongoDbContext.GetCollection<AboutImage>();
            var about = await aboutImageCollection.Find(s => s.AboutId == aboutId).FirstOrDefaultAsync();
            if (about == null)
            {
                return await Task.FromResult(-1);  // id bulunamadı
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                about.Content = memoryStream.ToArray();
                var updatedAboutImage = await aboutImageCollection.ReplaceOneAsync(s => s.AboutId == aboutId, about);
                if (updatedAboutImage.IsAcknowledged)
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    return await Task.FromResult(-2);
                }
            }
        }

        public async Task<int> DeleteAboutImageAsync(int aboutId)
        {
            var aboutImageCollection = _mongoDbContext.GetCollection<AboutImage>();
            var aboutImage = await GetAboutImage(aboutId);

            if (aboutImage == null)
            {
                return await Task.FromResult(-1);
            }

            var deletedAboutImage = await aboutImageCollection.DeleteOneAsync(p => p.AboutId == aboutImage.AboutId);
            if (deletedAboutImage.IsAcknowledged)  //silindiyse
            {
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(-2);
            }
        }
    }
}
