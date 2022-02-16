using SwipeNFT.Shared.Infrastructure.Query;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Shared.Infrastructure.QueryHandler
{
    public interface IQueryHandler<in TQuery, out TResponse>
        where TQuery : IQuery
        where TResponse : IBaseResponse
    {
        TResponse Handle(TQuery query);

    }
}
