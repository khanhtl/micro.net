
using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool Success);
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
