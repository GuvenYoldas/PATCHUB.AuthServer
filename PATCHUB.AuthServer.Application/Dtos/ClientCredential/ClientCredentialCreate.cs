namespace PATCHUB.AuthServer.Application.Dtos.ClientCredential
{
    public class ClientCredentialCreate
    {
        public int RequestLimit { get; set; }
        public int MaxRequestsPerMinute { get; set; }
        public int MaxRequestsPerHour { get; set; }
        public int MaxRequestsPerDay { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<string> IpList { get; set; }
    }
}
