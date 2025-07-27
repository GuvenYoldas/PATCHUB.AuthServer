
namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppClient
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<String> Audiences { get; set; }
    }
}
