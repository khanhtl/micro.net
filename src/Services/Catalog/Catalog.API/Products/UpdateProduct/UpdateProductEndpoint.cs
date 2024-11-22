namespace Catalog.API.Products.UpdateProduct;

internal record UpdateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
{
    public Guid Id { get; set; }
};

internal record UpdateProductResponse(bool Success);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (Guid id, ISender sender, UpdateProductRequest product) =>
        {
            product.Id = id;
            var command = product.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product"); ;
    }
}
