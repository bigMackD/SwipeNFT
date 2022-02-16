using Microsoft.Extensions.Configuration;

namespace SwipeNFT.API.Helpers
{
    public static class ConnectionService
    {
        public static string connectionString;
        public static void Set(IConfiguration config)
        {
            connectionString = config.GetConnectionString("IdentityConnection");
        }
    }
}
