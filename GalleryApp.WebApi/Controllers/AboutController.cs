using GalleryApp.Business.Abstract;
using GalleryApp.Business.Validation.About;
using GalleryApp.Entity.Dtos.About;
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
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("GetAllAboutList")]
        public async Task<ActionResult<IEnumerable<GetListAboutDto>>> GetAllAboutList()
        {
            try
            {
                return Ok(await _aboutService.GetAboutList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAboutById/{aboutId}")]
        public async Task<ActionResult<GetAboutDto>> GetAboutById(int aboutId)
        {
            var list = new List<string>();
            if (aboutId <= 0)
            {
                list.Add("Invalid aboutId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var about = await _aboutService.GetAboutById(aboutId);
                if (about == null)
                {
                    list.Add("About is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(about);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAbout")]
        public async Task<ActionResult<string>> AddAbout(AddAboutDto addAboutDto)
        {
            var list = new List<string>();
            var validator = new AboutAddValidator();
            var validationResult = validator.Validate(addAboutDto);
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
                var result = await _aboutService.AddAbout(addAboutDto);
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

        [HttpPut("UpdateAbout/{aboutId}")]
        public async Task<ActionResult<string>> UpdateAbout(int aboutId, UpdateAboutDto updateAboutDto)
        {
            var list = new List<string>();
            var validator = new AboutUpdateValidator();
            var validationResult = validator.Validate(updateAboutDto);
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
                var result = await _aboutService.UpdateAbout(aboutId, updateAboutDto);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("About is not found.");
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

        [HttpDelete("DeleteAbout/{aboutId}")]
        public async Task<ActionResult<string>> DeleteAbout(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.DeleteAbout(aboutId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("About is not found.");
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

        [HttpPost("AddAboutImage/{aboutId}")]
        public async Task<ActionResult<string>> AddAboutImage(int aboutId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.AddAboutImageAsync(aboutId, file);
                switch (result)
                {
                    case 1:
                        list.Add("Successful Adding.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("About is not found.");
                        return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                    case -2:
                        list.Add("There is already a about picture of this aboutId.");
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

        [HttpGet("GetAboutImage/{aboutId}")]
        public async Task<ActionResult<AboutImage>> GetAboutImage(int aboutId)
        {
            var list = new List<string>();
            if (aboutId <= 0)
            {
                list.Add("Invalid aboutId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var about = await _aboutService.GetAboutImage(aboutId);
                if (about == null)
                {
                    list.Add("About picture is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                return File(about.Content, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAboutImage/{aboutId}")]
        public async Task<ActionResult<string>> UpdateAboutImage(int aboutId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.UpdateAboutImageAsync(aboutId, file);
                switch (result)
                {
                    case 1:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("About picture is not found.");
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

        [HttpDelete("DeleteAboutImage/{aboutId}")]
        public async Task<ActionResult<string>> DeleteAboutImage(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.DeleteAboutImageAsync(aboutId);
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
