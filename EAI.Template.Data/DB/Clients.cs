using System.Collections.Generic;

namespace $safeprojectname$
{
    public partial class Clients
    {
        public Clients()
        {
            ClientScopes = new HashSet<ClientScopes>();
        }

        public int Id { get; set; }
        public string ClientId { get; set; }
        public byte[] ClientSecret { get; set; }
        public int ClientTypeId { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public bool AllowRefreshToken { get; set; }

        public ClientTypes ClientType { get; set; }
        public ICollection<ClientScopes> ClientScopes { get; set; }
    }
}