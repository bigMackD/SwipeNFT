using SwipeNFT.Shared.Infrastructure.Requests;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUsersQuery : IQuery, IPagingQuery
    {
        public string SortDirection { get; set; }
        public int PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
