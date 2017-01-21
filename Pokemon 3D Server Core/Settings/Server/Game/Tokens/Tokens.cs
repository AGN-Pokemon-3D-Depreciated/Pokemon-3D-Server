using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Tokens
{
    public class Tokens
    {
        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public Dictionary<string, string> TokenDefination { get; private set; } = new Dictionary<string, string>();

        public Tokens()
        {
            #region Player Name Text

            TokenDefination.Add("SERVER_GAMEJOLT", "{0} ({1}) {2}");
            TokenDefination.Add("SERVER_NOGAMEJOLT", "{0} {1}");
            TokenDefination.Add("SERVER_CHATGAMEJOLT", "<{0} ({1})>: {2}");
            TokenDefination.Add("SERVER_CHATNOGAMEJOLT", "<{0}>: {1}");
            TokenDefination.Add("SERVER_COMMANDGAMEJOLT", "[Command] {0} ({1}) {2}");
            TokenDefination.Add("SERVER_COMMANDNOGAMEJOLT", "[Command] {0} {1}");

            #endregion Player Name Text

            #region Player Join Messages

            TokenDefination.Add("SERVER_FULL", "This server is currently full of players.");
            TokenDefination.Add("SERVER_OFFLINEMODE", "This server do not allow offline save.");
            TokenDefination.Add("SERVER_WRONGGAMEMODE", "This server require you to play the following gamemode: {0}.");
            TokenDefination.Add("SERVER_DISALLOW", "You do not have required permission to join the server. Please try again later.");
            TokenDefination.Add("SERVER_CLONE", "You are still in the server. Please try again later.");

            #endregion Player Join Messages

            #region Player Left Messages

            TokenDefination.Add("SERVER_AFK", "You have been afking for too long.");
            TokenDefination.Add("SERVER_NOPING", "You have a slow connection or the server is not responding.");
            TokenDefination.Add("SERVER_KICKED", "You have been kicked from the server with the following reason: {0}");

            #endregion Player Left Messages

            #region Client Events

            TokenDefination.Add("SERVER_CLOSE", "This server have been shut down or lost its connection. Sorry for the inconveniences caused.");
            TokenDefination.Add("SERVER_RESTART", "This server is restarting. Sorry for the inconveniences caused.");
            TokenDefination.Add("SERVER_RESTARTWARNING", "The server is scheduled to restart in {0}. Please enjoy your stay.");
            TokenDefination.Add("SERVER_TRADEPVPFAIL", "The server is scheduled to restart in {0}. For your personal safety, starting a new trading and PvP is disabled.");

            #endregion Client Events

            #region Ban / Mute

            TokenDefination.Add("SERVER_BLACKLISTED", "You have been banned from server. Reason: {0} | Ban duration: {1}.");
            TokenDefination.Add("SERVER_IPBLACKLISTED", "You have been ip banned from server. Reason: {0} | Ban duration: {1}.");
            TokenDefination.Add("SERVER_MUTED", "You have been muted in the server. Reason: {0} | Ban duration: {1}.");
            TokenDefination.Add("SERVER_MUTEDTEMP", "You have been muted by that player. Reason: {0} | Ban duration: {1}.");
            TokenDefination.Add("SERVER_SWEAR", "Please avoid swearing where necessary. Triggered word: {0}");
            TokenDefination.Add("SERVER_SWEARWARNING", "Please avoid swearing where necessary. Triggered word: {0} | You have {1} infraction point. {2} infraction point will get a timeout.");

            #endregion Ban / Mute

            #region Using Command

            TokenDefination.Add("SERVER_COMMANDPERMISSION", "You do not have the required permission to use this command.");
            TokenDefination.Add("SERVER_PLAYERNOTEXIST", "The requested player does not exist in the server. Please try again.");
            TokenDefination.Add("SERVER_KICKSELF", "You are trying to kick yourself. For your personal safety, we will not kick you :)");
            TokenDefination.Add("SERVER_NOTOPERATOR", "The requested player is not an operator.");

            #endregion Using Command

            TokenDefination.Add("SERVER_SPAM", "Please be unique :) don't send the same message again in quick succession.");
            TokenDefination.Add("SERVER_NOCHAT", "This server do not allow user to chat. Sorry for the inconveniences caused.");

            TokenDefination.Add("SERVER_LOGINTIME", "You have played in the server for {0} hour(s). We encourage your stay but also encourage you to take a small break :)");

            TokenDefination.Add("SERVER_PVPVALIDATION", "You are unable to use this party due to the following reason: {0}");
            TokenDefination.Add("SERVER_PVPDISALLOW", "This server do not allow user to PvP. Sorry for the inconveniences caused.");

            TokenDefination.Add("SERVER_TRADEVALIDATION", "You are unable to trade this pokemon due to the following reason: {0}");
            TokenDefination.Add("SERVER_TRADEDISALLOW", "This server do not allow user to Trade. Sorry for the inconveniences caused.");

            TokenDefination.Add("SERVER_CURRENTCHATCHANNEL", "You are now at {0} Chat Channel. For more info, type /help chatchannel.");
        }

        public string ToString(string key, params string[] variable)
        {
            string returnValue = null;

            if (TokenDefination.ContainsKey(key))
                returnValue = string.Format(TokenDefination[key], variable);

            return returnValue;
        }
    }
}