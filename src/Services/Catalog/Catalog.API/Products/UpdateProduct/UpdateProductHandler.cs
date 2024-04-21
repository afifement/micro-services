﻿namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);
internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await session.LoadAsync<Product>(command.Id,cancellationToken);
        if(product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
