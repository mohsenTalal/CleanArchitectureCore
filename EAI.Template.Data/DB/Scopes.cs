using System.Collections.Generic;

namespace $safeprojectname$
{
    public partial class Scopes
    {
        public Scopes()
        {
            ClientScopes = new HashSet<ClientScopes>();
        }

        public int Id { get; set; }
        public string ScopeName { get; set; }
        public bool? Active { get; set; }
        public int ApplicationId { get; set; }

        public Applications Application { get; set; }
        public ICollection<ClientScopes> ClientScopes { get; set; }
    }
}