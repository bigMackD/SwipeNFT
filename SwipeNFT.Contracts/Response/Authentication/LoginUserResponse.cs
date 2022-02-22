using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Authentication
{
    public class LoginUserResponse : IResponse
    {
        public string Token { get; set; }
    }
}
