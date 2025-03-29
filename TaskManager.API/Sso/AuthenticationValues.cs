namespace TaskManager.API.Sso
{
    public class AuthenticationValues
    {
        public string KeyCloakAuthority { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string MetadataAddress { get; init; }
        public string IssuerAddress { get; init; }
        

        public AuthenticationValues(string keyCloakAuthority, string clientId, string clientSecret, string metadataAddress, string issuerAddress)
        {
            KeyCloakAuthority = keyCloakAuthority;
            ClientId = clientId;
            ClientSecret = clientSecret;
            MetadataAddress = metadataAddress;
            IssuerAddress = issuerAddress;
        }
    }
}