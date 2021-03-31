using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Models
{
    public class WebUserApplicationContract
    {
        public int WebUserID { get; set; }
        public int WebApplicationID { get; set; }
        public string WebApplicationName { get; set; }
    }
}
