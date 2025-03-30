namespace TaskManager.API.Settings
{
    public class JwtOptions
    {
        public string Authority { get; init; }
        public string Audience { get; init; }
        public bool RequireHttpsMetadata { get; init; }
    }
}

