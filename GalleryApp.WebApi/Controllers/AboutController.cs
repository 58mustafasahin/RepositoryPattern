using GalleryApp.Business.Abstract;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetListAboutDto>>> GetAllAboutListAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAboutDto>> GetAboutByIdAsync(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var report = await _aboutService.GetAboutById(aboutId);
                if (report == null)
                {
                    list.Add("Hakkımızda bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(report);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAbout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddAboutAsync([FromBody] AddAboutDto addAbout)
        {
            //var validator = new AboutAddValidator(_aboutService);
            //var validationResults = validator.Validate(addAbout);


            //if (!validationResults.IsValid)
            //{
            //    return Ok(validationResults.Errors);
            //}

            try
            {
                await _aboutService.AddAbout(addAbout);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAbout/{aboutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> UpdateAboutAsync(int aboutId, [FromBody] UpdateAboutDto updateAbout)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.UpdateAbout(aboutId, updateAbout);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("hakkımızda bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("hakkımızda silinemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAbout/{aboutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> DeleteAbout(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.DeleteAbout(aboutId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == 0)
                {
                    list.Add("hakkımızda bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAboutImage/{aboutId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddAboutImageAsync(int aboutId, IFormFile file)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.AddAboutImageAsync(aboutId, file);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Hakkımızda bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu Hakkımızda Id ye ait resmi vardır.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Hakkımızda resimi eklenemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAboutImage/{aboutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AboutImage>> GetAboutImageAsync(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var about = await _aboutService.GetAboutImage(aboutId);
                if (about == null)
                {
                    list.Add("Hakkımızda resmi bulunamadı.");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateAboutImageAsync(int aboutId, IFormFile file)
        {
            var list = new List<string>();

            try
            {
                var result = await _aboutService.UpdateAboutImageAsync(aboutId, file);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result == -1)
                {
                    list.Add("Hakkımızda resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Hakkımızda resmi güncellemedi.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAboutImage/{aboutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteAboutImageAsync(int aboutId)
        {
            var list = new List<string>();
            try
            {
                var result = await _aboutService.DeleteAboutImageAsync(aboutId);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Hakkımızda resmi bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else  //-2 veya farklı bir durum olursa
                {
                    list.Add("Hakkımızda resmi silinemedi.");
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
