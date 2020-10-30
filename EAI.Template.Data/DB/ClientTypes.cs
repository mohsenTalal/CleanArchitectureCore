using System.Collections.Generic;

namespace $safeprojectname$
{
    public partial class ClientTypes
    {
        public ClientTypes()
        {
            Clients = new HashSet<Clients>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Clients> Clients { get; set; }
    }
}