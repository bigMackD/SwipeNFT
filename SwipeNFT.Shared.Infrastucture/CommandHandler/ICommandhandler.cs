using SwipeNFT.Shared.Infrastructure.Requests;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.CommandHandler
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        TResponse Handle(TCommand command);
    }
}
