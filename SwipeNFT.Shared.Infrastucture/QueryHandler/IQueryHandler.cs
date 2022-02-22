using SwipeNFT.Shared.Infrastructure.Requests;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.QueryHandler
{
    public interface IQueryHandler<in TQuery, out TResponse>
        where TQuery : IQuery
        where TResponse : IResponse
    {
        TResponse Handle(TQuery query);

    }
}
