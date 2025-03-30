namespace TaskManager.API.Settings
{
    public class JwtOptions
    {
        public string Authority { get; set; } = "http://localhost:8080/realms/task-manager";
        public string Audience { get; set; } = "account";
        public bool RequireHttpsMetadata { get; set; } = false;
    }
}

