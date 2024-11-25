﻿namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/{userName}", async (string userName, ISender sender) =>
        {
            var result = sender.Send(new GetBasketQuery(userName));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
           .WithName("GetBasket")
           .Produces<GetBasketResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Get Basket")
           .WithDescription("Get Basket");
    }
}
