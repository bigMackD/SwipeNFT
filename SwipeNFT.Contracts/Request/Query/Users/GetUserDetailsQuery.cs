using SwipeNFT.Shared.Infrastructure.Requests;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUserDetailsQuery : IQuery
    {
        public string Id { get; set; }
    }
}
