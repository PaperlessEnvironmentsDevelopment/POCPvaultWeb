using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Models
{
    public class ClientContract
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isSageClient { get; set; }
        public bool isDesktopClient { get; set; }
        public int PEUserID { get; set; }
        public string DesktopVersion { get; set; }
        public int LOTOUserType { get; set; }
        public string ZohoID { get; set; }
        public int PasswordExpirationIntervalInDays { get; set; }
        public int PasswordExpirationWarningInDays { get; set; }
    }
}
