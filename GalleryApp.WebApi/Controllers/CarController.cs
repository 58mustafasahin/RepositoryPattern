using GalleryApp.Business.Abstract;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetListCarDto>>> GetAllCarListAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCarDto>> GetCarByIdAsync(int carId)
        {
            var list = new List<string>();
            try
            {
                var car = await _carService.GetCarByIdAsync(carId);
                if (car == null)
                {
                    list.Add("Araç bulunamadı.");
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCarAsync(AddCarDto addCarDto)
        {
            try
            {
                await _carService.AddCarAsync(addCarDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCar/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCarAsync(int carId, UpdateCarDto updateCarDto)
        {
            var list = new List<string>();
            try
            {
                int result = await _carService.UpdateCarAsync(carId, updateCarDto);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Araç bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Araç güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCar/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCar(int carId)
        {
            var list = new List<string>();
            try
            {
                int result = await _carService.DeleteCar(carId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Araç silinemedi.");  //silme işlemi başarısız
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCarCoverImageAsync(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.AddCarCoverImageAsync(carId, file);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Araç kapağı bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu carId ye ait araç kapağı resmi vardır.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Araç kapağı resmi eklenemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCarCoverImage/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarImage>> GetCarCoverImageAsync(int carId)
        {
            var list = new List<string>();
            try
            {
                var car = await _carService.GetCarCoverImage(carId);
                if (car == null)
                {
                    list.Add("Araç kapağı resmi bulunamadı.");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCarCoverImageAsync(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.UpdateCarCoverImageAsync(carId, file);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Araç kapağı resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Araç kapağı resmi güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCarCoverImage/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCarCoverImageAsync(int carId)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.DeleteCarCoverImageAsync(carId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Araç kapağı resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Araç kapağı resmi silinemedi.");
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
        public async Task<ActionResult<List<string>>> GetCarImageAsync(int carId)
        {
            var list = new List<string>();
            try
            {
                var carImage = await _carService.GetCarImagesIdList(carId);
                if (carImage == null)
                {
                    list.Add("Araç listesi bulunamadı.");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarImage>> GetCarImageAsync(string carId)
        {
            var list = new List<string>();
            try
            {
                var currentImage = await _carService.GetCarImage(carId);
                if (currentImage == null)
                {
                    list.Add("Araç resmi bulunamadı.");
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCarImageAsync(int carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.AddCarImageAsync(carId, file);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Araç bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu carId ye ait araç resmi vardır.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Araç resmi eklenemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateCarImage/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCarImageAsync(string carId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.UpdateCarImageAsync(carId, file);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Araç resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Araç resmi güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCarImage/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCarImageAsync(string carId)
        {
            var list = new List<string>();
            try
            {
                var result = await _carService.DeleteCarImageAsync(carId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Araç resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Araç resmi silinemedi.");
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
