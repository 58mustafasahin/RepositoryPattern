using FluentValidation;
using GalleryApp.Entity.Dtos.Car;

namespace GalleryApp.Business.Validation.Car
{
    public class CarUpdateValidator : AbstractValidator<UpdateCarDto>
    {
        public CarUpdateValidator()
        {
            RuleFor(p => p.Brand).NotEmpty().WithMessage("Marka alanı boş geçilemez.")
                .MaximumLength(50).WithMessage("Marka 50 karakterden fazla olamaz");
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model alanı boş geçilemez.")
                .MaximumLength(50).WithMessage("Model 50 karakterden fazla olamaz");
            RuleFor(p => p.ModelYear).NotEmpty().WithMessage("Model yılı alanı boş geçilemez.");
            RuleFor(p => p.Km).NotEmpty().WithMessage("Km alanı boş geçilemez.");
            RuleFor(p => p.FuelType).NotEmpty().WithMessage("Model yılı alanı boş geçilemez.");
            RuleFor(p => p.Desc).NotEmpty().WithMessage("Açıklama alanı boş geçilemez.")
                .MaximumLength(200).WithMessage("Açıklama 200 karakterden fazla olamaz");
        }
    }
}
