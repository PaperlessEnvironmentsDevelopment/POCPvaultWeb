using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Models
{
    public class WebUserContract
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool WebAdministrator { get; set; }
        public bool Confirmed { get; set; }
        public bool CloudSynched { get; set; }
        public int PEUserID { get; set; }
        public int LOTOUserType { get; set; }
        public int PEFormsUserType { get; set; }
        public bool AdHoc { get; set; }
    }
}
