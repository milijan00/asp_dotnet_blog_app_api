namespace BlogApp.API.Core
{
    public class AppSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
    }
}
