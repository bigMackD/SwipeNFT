using SwipeNFT.Shared.Infrastructure.Query;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUsersQuery : IQuery, IPagingQuery
    {
        public string SortDirection { get; set; }
        public int PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
