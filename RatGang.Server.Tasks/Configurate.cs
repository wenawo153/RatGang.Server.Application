namespace RatGang.Server.Tasks
{
    public class Configurate
    {
        public static Configurate Singleton { get; set; } = new();
        public string DataBaseConnection { get; set; } = string.Empty;
    }
}
