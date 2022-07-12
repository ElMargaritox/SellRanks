using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using SellRanks.Economy;
using SellRanks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SellRanks
{
    public class SellRanksPlugin : RocketPlugin<Configuration>
    {
        public static SellRanksPlugin Instance { get; set; }
        public IEconomyProvider EconomyProvider { get; set; }
        protected override void Load()
        {
            Instance = this;


            if (Configuration.Instance.UseEconomy)
            {
                EconomyProvider = new UconomyEconomyProvider();
            }
            else EconomyProvider = new ExperienceEconomyProvider();
        }

        public void EnviarMensaje(UnturnedPlayer player, string message)
        {
            ChatManager.serverSendMessage(message.Replace('(', '<').Replace(')', '>'), Color.white, null, player.SteamPlayer(), EChatMode.GLOBAL, Instance.Configuration.Instance.Image, true);
        }

        public void EnviarMensaje(string message)
        {
            ChatManager.serverSendMessage(message.Replace('(', '<').Replace(')', '>'), Color.white, null, null, EChatMode.GLOBAL, Instance.Configuration.Instance.Image, true);
        }

        public void BuyRank(UnturnedPlayer player, Rank rank)
        {


            if (!player.HasGroup(rank.PermissionGroup))
            {

                EnviarMensaje(player, Translate("purchase_rank", rank.Name, rank.Price));
                EnviarMensaje(Translate("purchase_rank_global", player.CharacterName, rank.Name, rank.Price));
                EconomyProvider.IncrementBalance(player.Id, -rank.Price);
            }
            else
            {
                EnviarMensaje(player, Translate("purchased_rank", rank.Name));
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList list = new TranslationList();

                list.Add("list_ranks", "{0}");
                list.Add("ranks_syntax", "Usa /rangos <RangoAComprar> o /rangos (para ver la lista)");
                list.Add("rank_no_exist", "No existe el rango {0}");
                list.Add("no_money_to_buy", "No tienes dinero suficiente para comprar el rango {0}");
                list.Add("no_permission", "No tienes los permisos suficientes para comprar el rango {0}");
                list.Add("purchase_rank", "Has comprado el rango {0} por {1} $");
                list.Add("purchased_rank", "Ya tienes comprado el rango {0}");
                list.Add("purchase_rank_global", "El jugador {0} compro el rango {1} por {2} $");
                return list;    
            }
        }

        protected override void Unload()
        {
            base.Unload();
        }
    }
}
