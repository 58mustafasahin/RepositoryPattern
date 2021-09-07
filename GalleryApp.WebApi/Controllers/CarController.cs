using GalleryApp.Business.Abstract;
using GalleryApp.Business.Validation.Car;
using GalleryApp.Entity.Dtos.Car;
using GalleryApp.Entity.Entity.ImageEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetAllCarList")]
        public async Task<ActionResult<List<GetListCarDto>>> GetAllCarList()
        {
            try
            {
                return Ok(await _carService.GetAllCarListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCarById/{carId}")]
        public async Task<ActionResult<GetCarDto>> GetCarById(int carId)
        {
            var list = new List<string>();
            if (carId <= 0)
            {
                list.Add("Invalid carId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var car = await _carService.GetCarByIdAsync(carId);
                if (car == null)
                {
                    list.Add("Vehicle is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(car);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCar")]
        public async Task<ActionResult<string>> AddCar(AddCarDto addCarDto)
        {
            var list = new List<string>();
            var validator = new CarAddValidator();
            var validationResult = validator.Validate(addCarDto);
            if (!validationResult.IsValid)
            {
                foreach (var result in validationResult.Errors)
                {
                    list.Add(result.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }

            try
            {
                var result = await _carService.AddCarAsync(addCarDto);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Adding.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    default:
                        list.Add("Failed Adding.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCar/{carId}")]
        public async Task<ActionResult<string>> UpdateCar(int carId, UpdateCarDto updateCarDto)
        {
            var list = new List<string>();
            var validator = new CarUpdateValidator();
            var validationResult = validator.Validate(updateCarDto);
            if (!validationResult.IsValid)
            {
                foreach (var result in validationResult.Errors)
                {
                    list.Add(result.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _carService.UpdateCarAsync(carId, updateCarDto);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Updating.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCar/{carId}")]
        public async Task<ActionResult<string>> DeleteCar(int carId)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.DeleteCar(carId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Deleting.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------------------------------------
        //-----------------------kapak--------------------

        [HttpPost("AddCarCoverImage/{carId}")]
        public async Task<ActionResult<string>> AddCarCoverImage(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.AddCarCoverImageAsync(carId, file);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Adding.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle cover is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    case -2:
                        list.Add("There is already a vehicle cover picture of this carId.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Adding.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCarCoverImage/{carId}")]
        public async Task<ActionResult<CarImage>> GetCarCoverImage(int carId)
        {
            var list = new List<string>();
            if (carId <= 0)
            {
                list.Add("Invalid carId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var car = await _carService.GetCarCoverImage(carId);
                if (car == null)
                {
                    list.Add("Vehicle cover picture is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                return File(car.Content, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCarCoverImage/{carId}")]
        public async Task<ActionResult<string>> UpdateCarCoverImage(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.UpdateCarCoverImageAsync(carId, file);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle cover picture is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Updating.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCarCoverImage/{carId}")]
        public async Task<ActionResult<string>> DeleteCarCoverImage(int carId)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.DeleteCarCoverImageAsync(carId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle cover picture is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Deleting.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------------------------------------
        //-----------------------galeri--------------------

        [HttpGet("GetCarImagesIdList/{carId}")]
        public async Task<ActionResult<List<string>>> GetCarImage(int carId)
        {
            var list = new List<string>();
            try
            {
                var carImage = await _carService.GetCarImagesIdList(carId);
                if (carImage == null)
                {
                    list.Add("Vehicle list is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(carImage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCarImage/{carId}")]
        public async Task<ActionResult<CarImage>> GetCarImage(string carId)
        {
            var list = new List<string>();
            try
            {
                var currentImage = await _carService.GetCarImage(carId);
                if (currentImage == null)
                {
                    list.Add("Vehicle picture is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                return File(currentImage, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCarImage/{carId}")]
        public async Task<ActionResult<string>> AddCarImage(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.AddCarImageAsync(carId, file);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Adding.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle cover is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    case -2:
                        list.Add("There is already a vehicle cover picture of this carId.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Adding.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateCarImage/{carId}")]
        public async Task<ActionResult<string>> UpdateCarImage(string carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.UpdateCarImageAsync(carId, file);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle picture is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Updating.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCarImage/{carId}")]
        public async Task<ActionResult<string>> DeleteCarImage(string carId)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.DeleteCarImageAsync(carId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Vehicle picture is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    default:
                        list.Add("Failed Deleting.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
