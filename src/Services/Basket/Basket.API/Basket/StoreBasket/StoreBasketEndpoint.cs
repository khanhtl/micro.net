
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/{userName}", async (string userName, StoreBasketRequest request, ISender sender) =>
        {
            request.Cart.UserName = userName;
            var command = request.Adapt<StoreBasketCommand>();
            var result = sender.Send(command);
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
           .WithName("StoreBasket")
           .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Store Basket")
           .WithDescription("Store Basket");
    }
}
