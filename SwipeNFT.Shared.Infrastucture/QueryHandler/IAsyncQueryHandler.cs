using System.Threading.Tasks;
using SwipeNFT.Shared.Infrastructure.Requests;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.QueryHandler
{
    public interface IAsyncQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery
        where TResponse : IResponse
    {
        Task<TResponse> Handle(TQuery query);

    }
}
