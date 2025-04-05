using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RatGang.Server.Authentication
{
    public class Configurate
    {
        public static Configurate Singleton { get; set; } = new();

        public string Issuer { get; set; } = string.Empty; // издатель токена
        public string Audience { get; set; } = string.Empty; // потребитель токена

        public string Key { get; set; } = string.Empty;   // ключ для шифрации
        public string RefreshKey { get; set; } = string.Empty;

        public int LifeTime { get; set; } = 0;
        public int RefreshLifeTime { get; set; } = 0;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
        public SymmetricSecurityKey GetRefreshSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(RefreshKey));
        }
    }
}
