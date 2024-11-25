
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool Success);
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
