
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool IsSuccess);
internal class DeleteBasketCommandHandler(IBasketRepository repository)
              : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        bool isSuccess = await repository.DeleteBasketAsync(command.UserName, cancellationToken);
        return new DeleteBasketResult(isSuccess);
    }
}
