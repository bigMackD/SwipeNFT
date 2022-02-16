using SwipeNFT.Shared.Infrastructure.Command;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.CommandHandler
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
        TResponse Handle(TCommand command);
    }
}
