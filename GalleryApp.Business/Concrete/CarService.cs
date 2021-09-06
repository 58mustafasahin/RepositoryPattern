using AppCore.Utilities.Mapper;
using GalleryApp.Business.Abstract;
using GalleryApp.DataAccess.MongoDB;
using GalleryApp.DataAccess.UOW;
using GalleryApp.Entity.Dtos.Car;
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
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public readonly MongoDbContext _mongoDbContext;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper, MongoDbContext mongoDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<GetCarDto> GetCarByIdAsync(int id)
        {
            return await _unitOfWork.Cars.GetCarAsync(id);
        }

        public async Task<List<GetListCarDto>> GetAllCarListAsync()
        {
            return await _unitOfWork.Cars.GetAllCarListAsync();
        }

        public async Task<int> AddCarAsync(AddCarDto addCarDto)
        {
            var car = _mapper.Map<AddCarDto, Car>(addCarDto);
            await _unitOfWork.Cars.AddAsync(car);
            return await _unitOfWork.SaveAsync();
        }
        public async Task<int> UpdateCarAsync(int id, UpdateCarDto updateCarDto)
        {
            var result = _unitOfWork.Cars.GetById(id);
            if (result == null)
            {
                return await Task.FromResult(-1);
            }
            _unitOfWork.Cars.UpdateCar(id, updateCarDto);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteCar(int id)
        {
            _unitOfWork.Cars.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        //--------------------------------------------------------------------------------
        //-----------------------kapak--------------------

        public async Task<CarCoverImage> GetCarCoverImage(int carId)
        {
            var carImageCollection = _mongoDbContext.GetCollection<CarCoverImage>();
            return await carImageCollection.Find(s => s.CarId == carId).FirstOrDefaultAsync();
        }

        public async Task<int> AddCarCoverImageAsync(int carId, IFormFile file)
        {
            var carImageCollection = _mongoDbContext.GetCollection<CarCoverImage>();
            var carObject = await _unitOfWork.Cars.GetCarAsync(carId);
            var image = await carImageCollection.Find(s => s.CarId == carId).FirstOrDefaultAsync();
            if (carObject == null)
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
                var carImage = new CarCoverImage()
                {
                    Id = ObjectId.GenerateNewId(),
                    Content = memoryStream.ToArray(),
                    CarId = carId
                };
                carImageCollection.InsertOne(carImage);
            }
            return await Task.FromResult(1);
        }

        public async Task<int> UpdateCarCoverImageAsync(int carId, IFormFile file)
        {
            var carImageCollection = _mongoDbContext.GetCollection<CarCoverImage>();
            var car = await carImageCollection.Find(s => s.CarId == carId).FirstOrDefaultAsync();
            if (car == null)
            {
                return await Task.FromResult(-1);  // id bulunamadı
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                car.Content = memoryStream.ToArray();
                var updatedCarImage = await carImageCollection.ReplaceOneAsync(s => s.CarId == carId, car);
                if (updatedCarImage.IsAcknowledged)
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    return await Task.FromResult(-2);
                }
            }
        }

        public async Task<int> DeleteCarCoverImageAsync(int carId)
        {
            var carImageCollection = _mongoDbContext.GetCollection<CarCoverImage>();
            var carImage = await carImageCollection.Find(s => s.CarId == carId).FirstOrDefaultAsync();

            if (carImage == null)
            {
                return await Task.FromResult(-1);
            }

            var deletedCarImage = await carImageCollection.DeleteOneAsync(p => p.CarId == carImage.CarId);
            if (deletedCarImage.IsAcknowledged)  //silindiyse
            {
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(-2);
            }
        }

        //--------------------------------------------------------------------------------
        //-----------------------galeri--------------------

        public async Task<List<string>> GetCarImagesIdList(int id)
        {
            var imageCollection = _mongoDbContext.GetCollection<CarImage>();
            var imageList = await imageCollection.Find(p => p.CarId == id).ToListAsync();
            var imageIdList = new List<string>();
            for (int i = 0; i < imageList.Count; i++)
            {
                imageIdList.Add(imageList[i].Id.ToString());
            }
            return imageIdList;
        }
        public async Task<byte[]> GetCarImage(string carId)
        {
            var imageCollection = _mongoDbContext.GetCollection<CarImage>();
            var image = await imageCollection.Find(p => p.Id == ObjectId.Parse(carId)).FirstOrDefaultAsync();
            byte[] imageContent = image.Content;
            return imageContent;
        }

        public async Task<int> AddCarImageAsync(int carId, IFormFile file)
        {
            var carImageCollection = _mongoDbContext.GetCollection<CarImage>();
            var selectedCar = await GetCarByIdAsync(carId);

            if (selectedCar == null)
            {
                return await Task.FromResult(-1);
            }
            using (var memoryStream = new MemoryStream())
            {
                
                await file.CopyToAsync(memoryStream);
                var carImage = new CarImage()
                {
                    Id = ObjectId.GenerateNewId(),
                    Content = memoryStream.ToArray(),
                    CarId = carId
                };
                carImageCollection.InsertOne(carImage);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> UpdateCarImageAsync(string carId, IFormFile file)
        {
            var imageCollection = _mongoDbContext.GetCollection<CarImage>();
            var selectedImage = await imageCollection.Find(p => p.Id == ObjectId.Parse(carId)).SingleOrDefaultAsync();

            if (selectedImage == null)
            {
                return await Task.FromResult(-1);
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            selectedImage.Content = memoryStream.ToArray();
            var result = await imageCollection.ReplaceOneAsync(p => p.Id == ObjectId.Parse(carId), selectedImage);
            if (result.IsAcknowledged)  //silindiyse
            {
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<int> DeleteCarImageAsync(string carId)
        {
            var imageCollection = _mongoDbContext.GetCollection<CarImage>();
            var selectedImage = await imageCollection.Find(p => p.Id == ObjectId.Parse(carId)).SingleOrDefaultAsync();

            if (selectedImage == null)
            {
                await Task.FromResult(-1);
            }

            var deleteResult = await imageCollection.DeleteOneAsync(p => p.Id == ObjectId.Parse(carId));

            if (deleteResult.DeletedCount > 0)  //silindiyse
            {
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }
    }
}

