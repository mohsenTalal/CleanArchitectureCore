using System.Collections.Generic;
using $safeprojectname$.Dtos;

namespace $safeprojectname$.Application.Models
{
    public class Client
    {
        public Client()
        {
            Methods = new List<Scope>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Scope> Methods { get; set; }
    }
}
