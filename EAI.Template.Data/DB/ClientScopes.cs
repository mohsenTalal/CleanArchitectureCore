namespace $safeprojectname$
{
    public partial class ClientScopes
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ScopeId { get; set; }

        public Clients Client { get; set; }
        public Scopes Scope { get; set; }
    }
}