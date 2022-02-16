using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Authentication
{
    public class RegisterUserResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
