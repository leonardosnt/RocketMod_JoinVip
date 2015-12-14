/*
* Copyright (C) 2014 - 2015 Leonardosc
*
* This program is free software; you can redistribute it and/or modify
*
* it under the terms of the GNU General Public License version 2 as
* published by the Free Software Foundation.
* 
* See LICENCE.txt for mode details
*
*/

using System;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace JoinVip
{
    // NOT TESTED!
    public class JoinVipPlugin : RocketPlugin<JoinVipConfiguratio>
    {
        protected override void Load()
        => U.Events.OnPlayerConnected += OnPlayerConnected;


        protected override void Unload()
        => U.Events.OnPlayerConnected -= OnPlayerConnected;
        

        private void OnPlayerConnected( UnturnedPlayer player )
        {
            var players = Provider.Players;

            if ( players.Count != Provider.MaxPlayers - 1 ) return;

            if ( player.HasPermission( "joinvip.join" ) )
            {
                var randomPlayer = players[new Random().Next( players.Count )].SteamPlayerID.CSteamID;
                Provider.kick( randomPlayer, Configuration.Instance.KickMessage );
            }
            else
            {
                Provider.Reject( player.CSteamID, ESteamRejection.SERVER_FULL );// Reserve one slot
            }
        }
    }

    public class JoinVipConfiguratio : IRocketPluginConfiguration
    {
        public string KickMessage;

        public void LoadDefaults()
        {
            KickMessage = "You're kicked because server is full and vip has joined.";
        }
    }
}
