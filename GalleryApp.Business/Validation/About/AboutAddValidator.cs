using FluentValidation;
using GalleryApp.Entity.Dtos.About;

namespace GalleryApp.Business.Validation.About
{
    public class AboutAddValidator : AbstractValidator<AddAboutDto>
    {
        public AboutAddValidator()
        {
            RuleFor(p => p.Vision).NotEmpty().WithMessage("Vizyon boş geçilemez.")
                .MaximumLength(500).WithMessage("Visyon 500 karakterden fazla olamaz");
            RuleFor(p => p.Mission).NotEmpty().WithMessage("Misyon boş geçilemez.")
                .MaximumLength(500).WithMessage("Misyon 500 karakterden fazla olamaz");
            RuleFor(p => p.History).NotEmpty().WithMessage("Tarihçe boş geçilemez.")
                .MaximumLength(500).WithMessage("Tarihçe 500 karakterden fazla olamaz");
        }

    }
}
