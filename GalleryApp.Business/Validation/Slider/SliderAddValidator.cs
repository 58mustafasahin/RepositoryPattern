using FluentValidation;
using GalleryApp.Entity.Dtos.Slider;

namespace GalleryApp.Business.Validation.Slider
{
    public class SliderAddValidator : AbstractValidator<AddSliderDto>
    {
        public SliderAddValidator()
        {
            RuleFor(p => p.Text).NotEmpty().WithMessage("Text alanı boş geçilemez.")
                .MaximumLength(100).WithMessage("Text alanı en fazla 100 karakter olmalıdır.");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık alanı boş geçilemez.");
        }
    }
}
