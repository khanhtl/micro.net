using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
    :ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // create product
        var product = new Product
        {
            Name = request.Name,
            Categories = request.Categories,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
        };
        // save to database
        return new CreateProductResult(Guid.NewGuid());
    }
}
