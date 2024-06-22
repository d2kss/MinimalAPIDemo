using FluentValidation;
using MinimalAPI.Demo.Guvi.Models.DTO;

namespace MinimalAPI.Demo.Guvi.Validators
{
    public class ProductCreateValidator:AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateValidator()
        {
            RuleFor(model=>model.Name).NotEmpty().WithMessage("Product name should not be empty");
            RuleFor(model=>model.Price).InclusiveBetween(1,100000).NotEmpty();
            RuleFor(model => model.DisplayOrder).InclusiveBetween(1, 100);
        }
    }
}
