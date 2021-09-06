using AppCore.Data.EF;
using GalleryApp.DataAccess.EF.Abstract;
using GalleryApp.Entity.Dtos.Contact;
using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.EF.Concrete.Repository
{
    public class EFContactRepo : EFBaseRepository<Contact>, IContactRepo
    {
        public EFContactRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<GetListContactDto>> GetAllContactListAsync()
        {
            return await GetListQueryable(c => !c.IsDeleted)
               .Select(c => new GetListContactDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   Email = c.Email,
                   PhoneNumber = c.PhoneNumber,
                   Subject = c.Subject,
                   Message = c.Message
               }).ToListAsync();
        }

        public async Task<GetContactDto> GetContactAsync(int id)
        {
            return await GetListQueryable(c => !c.IsDeleted && c.Id == id)
               .Select(c => new GetContactDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   Email = c.Email,
                   PhoneNumber = c.PhoneNumber,
                   Subject = c.Subject,
                   Message = c.Message
               }).FirstOrDefaultAsync();
        }

        public void UpdateContact(int contactId, UpdateContactDto updateContactDto)
        {
            var updateContact = GetById(contactId);

            updateContact.Name = updateContactDto.Name;
            updateContact.Email = updateContactDto.Email;
            updateContact.PhoneNumber = updateContactDto.PhoneNumber;
            updateContact.Subject = updateContactDto.Subject;
            updateContact.Message = updateContactDto.Message;

            updateContact.ModifiedDate = DateTime.Now;

            Update(updateContact);
        }
    }
}
