using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Users
{
    public class GetUserProfileResponse : IResponse
    {
        public string FullName { get; set; }
    }
}
