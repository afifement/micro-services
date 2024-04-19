namespace Catalog.API.Products.UpdateProduct; 
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
              : ICommand<UpdateProductResult>;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Product Id is required");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Product Name is required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters");

        RuleFor(command => command.Description)
           .NotEmpty()
           .WithMessage("Product Description is required")
           .Length(2, 150)
           .WithMessage("Description must be between 2 and 150 characters");

        RuleFor(command => command.ImageFile)
           .Length(2, 150)
           .WithMessage("Image File must be between 2 and 150 characters");

        RuleFor(command => command.Price)
           .GreaterThan(0)
           .WithMessage("Price must be greater than 0");


    }
}
