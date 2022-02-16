using System.Threading.Tasks;
using SwipeNFT.Shared.Infrastructure.Command;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.CommandHandler
{
    public interface IAsyncCommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
        Task<TResponse> Handle(TCommand command);
    }
}
