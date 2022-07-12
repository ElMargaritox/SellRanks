using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellRanks.Models
{
    public class Rank
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string HasPermission { get; set; }
        public string PermissionGroup { get; set; }
    }
}
