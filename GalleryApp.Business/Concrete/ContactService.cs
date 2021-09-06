using AppCore.Utilities.Mapper;
using GalleryApp.Business.Abstract;
using GalleryApp.DataAccess.UOW;
using GalleryApp.Entity.Dtos.Contact;
using GalleryApp.Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApp.Business.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetContactDto> GetContactAsync(int contactId)
        {
            return await _unitOfWork.Contacts.GetContactAsync(contactId);
        }

        public async Task<IEnumerable<GetListContactDto>> GetAllContactListAsync()
        {
            return await _unitOfWork.Contacts.GetAllContactListAsync();
        }

        public async Task<int> AddContactAsync(AddContactDto addContactDto)
        {
            var contact = _mapper.Map<AddContactDto, Contact>(addContactDto);
            await _unitOfWork.Contacts.AddAsync(contact);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateContactAsync(int contactId, UpdateContactDto updateContactDto)
        {
            var contact = _unitOfWork.Contacts.GetById(contactId);
            if (contact == null)
            {
                return await Task.FromResult(-1);
            }
            else if (contact.IsDeleted == true)
            {
                return await Task.FromResult(-1);
            }
            _unitOfWork.Contacts.UpdateContact(contactId, updateContactDto);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteContact(int contactId)
        {
            _unitOfWork.Contacts.Delete(contactId);
            return await _unitOfWork.SaveAsync();
        }
    }
}
