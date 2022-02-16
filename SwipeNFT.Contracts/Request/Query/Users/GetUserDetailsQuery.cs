using SwipeNFT.Shared.Infrastructure.Query;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUserDetailsQuery : IQuery
    {
        public string Id { get; set; }
    }
}
