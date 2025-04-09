namespace TaskManager.API.Settings
{
    public class JwtOptions
    {
        public string Authority { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public bool RequireHttpsMetadata { get; init; }
    }
}

