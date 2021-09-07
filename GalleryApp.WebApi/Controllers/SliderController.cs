using GalleryApp.Business.Abstract;
using GalleryApp.Business.Validation.Slider;
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
        public async Task<ActionResult<List<GetListSliderDto>>> GetAllSliderList()
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
        public async Task<ActionResult<GetSliderDto>> GetSliderById(int sliderId)
        {
            var list = new List<string>();
            if (sliderId <= 0)
            {
                list.Add("Invalid sliderId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var slider = await _sliderService.GetSliderAsync(sliderId);
                if (slider == null)
                {
                    list.Add("Slider is not found.");
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
        public async Task<ActionResult<string>> AddSlider(AddSliderDto addSliderDto)
        {
            var list = new List<string>();
            var validator = new SliderAddValidator();
            var validationResult = validator.Validate(addSliderDto);
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
                var result = await _sliderService.AddSliderAsync(addSliderDto);
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

        [HttpPut("UpdateSlider/{sliderId}")]
        public async Task<ActionResult<string>> UpdateSlider(int sliderId, UpdateSliderDto updateSliderDto)
        {
            var list = new List<string>();
            var validator = new SliderUpdateValidator();
            var validationResult = validator.Validate(updateSliderDto);
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
                var result = await _sliderService.UpdateSliderAsync(sliderId, updateSliderDto);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Slider is not found.");
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

        [HttpDelete("DeleteSlider/{sliderId}")]
        public async Task<ActionResult<string>> DeleteSlider(int sliderId)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.DeleteSlider(sliderId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Slider is not found.");
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
        //-----------------------image--------------------

        [HttpPost("AddSliderImage/{sliderId}")]
        public async Task<ActionResult<string>> AddSliderImage(int sliderId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.AddSliderImageAsync(sliderId, file);
                switch (result)
                {
                    case 1:
                        list.Add("Successful Adding.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Slider is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    case -2:
                        list.Add("There is already a slider picture of this sliderId.");
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

        [HttpGet("GetSliderImage/{sliderId}")]
        public async Task<ActionResult<SliderImage>> GetSliderImage(int sliderId)
        {
            var list = new List<string>();
            if (sliderId <= 0)
            {
                list.Add("Invalid sliderId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var slider = await _sliderService.GetSliderImage(sliderId);
                if (slider == null)
                {
                    list.Add("Slider picture is not found.");
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
        public async Task<ActionResult<string>> UpdateSliderImage(int sliderId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.UpdateSliderImageAsync(sliderId, file);
                switch (result)
                {
                    case 1:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Slider picture is not found.");
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

        [HttpDelete("DeleteSliderImage/{sliderId}")]
        public async Task<ActionResult<string>> DeleteSliderImage(int sliderId)
        {
            var list = new List<string>();
            try
            {
                var result = await _sliderService.DeleteSliderImageAsync(sliderId);
                switch (result)
                {
                    case 1:
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
    }
}
