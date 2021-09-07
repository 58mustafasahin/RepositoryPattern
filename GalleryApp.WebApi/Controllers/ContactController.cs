using GalleryApp.Business.Abstract;
using GalleryApp.Business.Validation.Contact;
using GalleryApp.Entity.Dtos.Contact;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("GetAllContactList")]
        public async Task<ActionResult<List<GetListContactDto>>> GetAllContactList()
        {
            try
            {
                return Ok(await _contactService.GetAllContactListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetContactById/{contactId}")]
        public async Task<ActionResult<GetContactDto>> GetContactById(int contactId)
        {
            var list = new List<string>();
            if (contactId <= 0)
            {
                list.Add("Invalid contactId.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var contact = await _contactService.GetContactAsync(contactId);
                if (contact == null)
                {
                    list.Add("Contact is not found.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return Ok(contact);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddContact")]
        public async Task<ActionResult<string>> AddContact(AddContactDto addContactDto)
        {
            var list = new List<string>();
            var validator = new ContactAddValidator();
            var validationResults = validator.Validate(addContactDto);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _contactService.AddContactAsync(addContactDto);
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

        [HttpPut("UpdateContact/{contactId}")]
        public async Task<ActionResult<string>> UpdateContact(int contactId, UpdateContactDto updateContactDto)
        {
            var list = new List<string>();
            var validator = new ContactUpdateValidator();
            var validationResults = validator.Validate(updateContactDto);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _contactService.UpdateContactAsync(contactId, updateContactDto);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Updating.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Contact is not found.");
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

        [HttpDelete("DeleteContact/{contactId}")]
        public async Task<ActionResult<string>> DeleteContact(int contactId)
        {
            var list = new List<string>();
            try
            {
                var result = await _contactService.DeleteContact(contactId);
                switch (result)
                {
                    case > 0:
                        list.Add("Successful Deleting.");
                        return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                    case -1:
                        list.Add("Contact is not found.");
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
