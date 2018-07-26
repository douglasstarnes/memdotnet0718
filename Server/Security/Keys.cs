using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Server.Security
{
    public static class Keys
    {
        public static SymmetricSecurityKey SigningKey
        {
            get 
            {
                var directory = Directory.GetCurrentDirectory();
                var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json");
                var config = configurationBuilder.Build();
                var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["SecurityToken"]));
                return signingKey;
            }
        }
    }
}