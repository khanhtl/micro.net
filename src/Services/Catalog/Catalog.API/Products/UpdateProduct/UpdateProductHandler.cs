﻿
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) :ICommand<UpdateProductResult>;

internal record UpdateProductResult(bool Success);
internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        product.Name = command.Name;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        product.Categories = command.Categories;

        session.Update(product);
        await session.SaveChangesAsync();
        return new UpdateProductResult(true);
    }
}
