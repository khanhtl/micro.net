
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResponse(bool Success);

public class DeleteBasketEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{userName}", async (string userName, ISender sender) =>
        {
            var result = sender.Send(new DeleteBasketCommand(userName));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
           .WithName("DeleteBasket")
           .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Delete Basket")
           .WithDescription("Delete Basket");
    }
}
