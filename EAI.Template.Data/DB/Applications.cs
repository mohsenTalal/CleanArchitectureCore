using System;
using System.Collections.Generic;

namespace $safeprojectname$
{
    public partial class Applications
    {
        public Applications()
        {
            Scopes = new HashSet<Scopes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsMultiLogin { get; set; }

        public ICollection<Scopes> Scopes { get; set; }
    }
}