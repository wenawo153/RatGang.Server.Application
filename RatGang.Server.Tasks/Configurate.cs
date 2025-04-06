namespace RatGang.Server.Tasks
{
    public class Configurate
    {
        public static Configurate Singleton { get; set; } = new();
        public string DataBaseConnection { get; set; } = string.Empty;

        public MinioOptions MinioOptions { get; set; } = new();
    }

    public class MinioOptions
    {
        public string Endpoint { get; set; } = string.Empty;

        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
