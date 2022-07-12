using Rocket.API;
using SellRanks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellRanks
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool UseEconomy;
        public string Image { get; set; }
        public List<Rank> Ranks { get; set; }
        public void LoadDefaults()
        {
            Image = "INSERT URL";
            UseEconomy = false;
            Ranks = new List<Rank>
            {
                new Rank{Name = "rango1", HasPermission = "none", Price = 15000, PermissionGroup = "rango1"},
                new Rank{Name = "rango2", HasPermission = "none", Price = 20000, PermissionGroup = "rango2"},
                new Rank{Name = "rangovip", HasPermission = "vip", Price = 30000, PermissionGroup = "rangovip"}
            };
        }
    }
}
