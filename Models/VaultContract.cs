using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Models
{
    public class VaultContract
    {
        public System.Int32 ID { get; set; }
        public System.String Name { get; set; }
        public System.String Description { get; set; }
        public System.String FilePath { get; set; }
        public System.DateTime FiscalYearStart { get; set; }
        public System.Boolean Active { get; set; }
        public System.String DisplayName { get; set; }
        public System.Int32 VaultNumber { get; set; }
        public System.String ArchiveFolder { get; set; }
    }
}
