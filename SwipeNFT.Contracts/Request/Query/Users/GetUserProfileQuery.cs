using SwipeNFT.Shared.Infrastructure.Query;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUserProfileQuery : IQuery
    {
        public string UserId { get; set; }

    }
}
