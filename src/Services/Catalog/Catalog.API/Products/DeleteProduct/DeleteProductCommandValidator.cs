namespace Catalog.API.Products.DeleteProduct;
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty()
                                      .WithMessage("Product Id is required");
    }
}
