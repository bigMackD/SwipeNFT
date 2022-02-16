using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Users
{
    public class GetUserProfileResponse : IBaseResponse
    {
        public string FullName { get; set; }

        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
