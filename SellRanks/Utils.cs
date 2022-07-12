using Rocket.Core;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellRanks
{
    public static  class Utils
    {
        public static bool HasGroup(this UnturnedPlayer player, string groupName)
        {
            var x = R.Permissions.AddPlayerToGroup(groupName, player);
            switch (x)
            {
                case Rocket.API.RocketPermissionsProviderResult.Success:
                    return false;
                case Rocket.API.RocketPermissionsProviderResult.DuplicateEntry:
                    return true;
                case Rocket.API.RocketPermissionsProviderResult.GroupNotFound:
                    return false;
                case Rocket.API.RocketPermissionsProviderResult.PlayerNotFound:
                    return false;
            }
            return false;
        }
    }
}
