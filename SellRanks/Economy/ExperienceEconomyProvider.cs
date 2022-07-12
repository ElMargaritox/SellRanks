using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellRanks.Economy
{
    public class ExperienceEconomyProvider : IEconomyProvider
    {
        public decimal GetBalance(string playerId)
        {
            var player = GetPlayerFromId(playerId);
            return player.skills.experience;
        }

        public void IncrementBalance(string playerId, decimal amount)
        {
            var player = GetPlayerFromId(playerId);

            if (amount < 0)
            {
                player.skills.askSpend((uint)Math.Abs(amount));
            }
            else
            {
                player.skills.askAward((uint)amount);
            }
        }

        private Player GetPlayerFromId(string playerId)
        {
            return PlayerTool.getPlayer(new CSteamID(ulong.Parse(playerId)));
        }
    }
}
