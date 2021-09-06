using FluentValidation;
using GalleryApp.Entity.Dtos.Contact;

namespace GalleryApp.Business.Validation.Contact
{
    public class ContactAddValidator : AbstractValidator<AddContactDto>
    {
        public ContactAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.")
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olmalıdır.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş geçilemez.");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("İletişim numarası boş geçilemez.")
                .Length(11, 11).WithMessage("Telefon numarası 11 haneden oluşmalıdır."); ;
            RuleFor(p => p.Subject).NotEmpty().WithMessage("Konu boş geçilemez.");
            RuleFor(p => p.Message).NotEmpty().WithMessage("Mesaj içeriği boş geçilemez.");
        }
    }
}
