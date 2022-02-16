namespace SwipeNFT.Shared.Infrastructure.Response
{
    public interface IBaseResponse
    {
        bool Success { get; set; }
        string[] Errors { get; set; }
    }
}
