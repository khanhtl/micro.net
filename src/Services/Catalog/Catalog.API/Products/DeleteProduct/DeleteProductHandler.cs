
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
internal record DeleteProductResult(bool Success);


internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = session.LoadAsync<Product>(command.Id, cancellationToken);

        if(product == null)
        {
            throw new ProductNotFoundException();
        }

        session.Delete(product);
        await session.SaveChangesAsync();
        return new DeleteProductResult(true);
    }
}
