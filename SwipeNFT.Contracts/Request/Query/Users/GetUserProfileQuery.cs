using SwipeNFT.Shared.Infrastructure.Requests;

namespace SwipeNFT.Contracts.Request.Query.Users
{
    public class GetUserProfileQuery : IQuery
    {
        public string UserId { get; set; }

    }
}
