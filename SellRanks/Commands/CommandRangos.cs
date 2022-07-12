using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellRanks.Commands
{
    internal class CommandRangos : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "rangos";

        public string Help => "buy your rank";

        public string Syntax => "/rangos <rankToBuy>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {

            UnturnedPlayer player = (UnturnedPlayer)caller;


            if(command.Length > 1)
            {
                SellRanksPlugin.Instance.EnviarMensaje(player, SellRanksPlugin.Instance.Translate("ranks_syntax"));
                return;
            }

            if(command.Length == 0)
            {

                string msg = string.Empty;
                SellRanksPlugin.Instance.Configuration.Instance.Ranks.ForEach(rank =>
                {
                    string vipOnly = rank.HasPermission == "none" ? "" : "VIP ONLY";
                    msg += $"[{rank.Name}] ({rank.Price} $) {vipOnly} - ";
                });
                SellRanksPlugin.Instance.EnviarMensaje(player, SellRanksPlugin.Instance.Translate("list_ranks", msg));
                return;
            }

            var rank1 = SellRanksPlugin.Instance.Configuration.Instance.Ranks.Find(x => x.Name.ToLower() == command[0].ToLower());

            if(rank1 != null)
            {
                if(SellRanksPlugin.Instance.EconomyProvider.GetBalance(player.Id) >= rank1.Price)
                {
                    if(rank1.HasPermission == "none")
                    {
                        SellRanksPlugin.Instance.BuyRank(player, rank1);
                    }
                    else
                    {
                        if (player.HasPermission(rank1.HasPermission))
                        {
                            SellRanksPlugin.Instance.BuyRank(player, rank1);
                        }
                        else
                        {
                            SellRanksPlugin.Instance.EnviarMensaje(player, SellRanksPlugin.Instance.Translate("no_permission", rank1.Name));
                        }
                    }
                }
                else
                {
                    SellRanksPlugin.Instance.EnviarMensaje(player, SellRanksPlugin.Instance.Translate("no_money_to_buy", rank1.Name));
                }
            }
            else
            {
                SellRanksPlugin.Instance.EnviarMensaje(player, SellRanksPlugin.Instance.Translate("rank_no_exist", command[0].ToUpper()));
            }



        }
    }
}
