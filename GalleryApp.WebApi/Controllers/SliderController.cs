using GalleryApp.Business.Abstract;
using GalleryApp.Entity.Dtos.Slider;
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
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet("GetAllSliderList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetListSliderDto>>> GetAllSliderListAsync()
        {
            try
            {
                return Ok(await _sliderService.GetAllSliderListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSliderById/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSliderDto>> GetSliderByIdAsync(int sliderId)
        {
            var list = new List<string>();
            try
            {
                var slider = await _sliderService.GetSliderAsync(sliderId);
                if (slider == null)
                {
                    list.Add("Slider bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(slider);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSlider")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddSliderAsync(AddSliderDto addSliderDto)
        {
            try
            {
                await _sliderService.AddSliderAsync(addSliderDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSlider/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateSliderAsync(int sliderId, UpdateSliderDto updateSliderDto)
        {
            var list = new List<string>();
            try
            {
                int result = await _sliderService.UpdateSliderAsync(sliderId, updateSliderDto);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Slider bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Slider güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSlider/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteSlider(int sliderId)
        {
            var list = new List<string>();
            try
            {
                int result = await _sliderService.DeleteSlider(sliderId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Slider silinemedi.");  //silme işlemi başarısız
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSliderImage/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddSliderImageAsync(int sliderId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.AddSliderImageAsync(sliderId, file);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Slider bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu slider Id ye ait resmi vardır.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Slider Resimi eklenemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSliderImage/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SliderImage>> GetSliderImageAsync(int sliderId)
        {
            var list = new List<string>();
            try
            {
                var slider = await _sliderService.GetSliderImage(sliderId);
                if (slider == null)
                {
                    list.Add("Slider resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                return File(slider.Content, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSliderImage/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateSliderImageAsync(int sliderId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.UpdateSliderImageAsync(sliderId, file);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Slider resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Slider resmi güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSliderImage/{sliderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteSliderImageAsync(int sliderId)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.DeleteSliderImageAsync(sliderId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Slider resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Slider resmi silinemedi.");
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
