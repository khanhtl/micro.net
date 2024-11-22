﻿namespace Catalog.API.Products.GetProductsByCategory;

internal record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
           .WithName("GetProductsByCategory")
           .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Get Products By Category")
           .WithDescription("Get Products By Category"); ;
    }
}
