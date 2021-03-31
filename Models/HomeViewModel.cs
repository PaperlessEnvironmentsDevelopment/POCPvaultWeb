using POCPvaultWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Models
{
    public class HomeViewModel
    {
        public HomeViewModel() 
        {
            Clients = new List<ClientContract>();
            Vaults = new List<VaultContract>();
            Applications = new List<WebUserApplicationContract>();
            User = new WebUserContract();
        }

        public List<ClientContract> Clients { get; set; } 
        public List<VaultContract> Vaults { get; set; }
        public List<WebUserApplicationContract> Applications { get; set; }
        public WebUserContract User { get; set; }
    }
}
