namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
              : ICommand<CreateProductResult>;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("Image File is required");
            RuleFor(x=>x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x=>x.Price).GreaterThan(0).WithMessage("price must be greater than 0");
    }
}
