using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.Server.GameModes
{
    public class GameModes
    {
        [YamlMember(Alias = "Pokemon 3D")]
        public bool Pokemon3D { get; set; } = true;

        [YamlMember(Alias = "Pokemon Silver Soul")]
        public bool PokemonSilversSoul { get; set; } = false;

        [YamlMember(Alias = "Pokemon Brown 3D")]
        public bool PokemonBrown3D { get; set; } = false;

        [YamlMember(Alias = "Pokemon Gold&Silver - RandomLocke")]
        public bool PokemonGoldSilverRandomLocke { get; set; } = false;

        [YamlMember(Alias = "Pokemon Universal 3D")]
        public bool PokemonUniversal3D { get; set; } = false;

        [YamlMember(Alias = "Pokemon Lost Silver")]
        public bool PokemonLostSilver { get; set; } = false;

        [YamlMember(Alias = "German")]
        public bool German { get; set; } = false;

        [YamlMember(Alias = "Darkfire Mode")]
        public bool DarkfireMode { get; set; } = false;

        [YamlMember(Alias = "1 Year Later 3D")]
        public bool OneYearLater3D { get; set; } = false;

        public List<string> Others { get; private set; } = new List<string>();

        private bool _OfflineMode = false;

        public bool OfflineMode
        {
            get
            {
                if (PokemonSilversSoul || PokemonBrown3D || PokemonGoldSilverRandomLocke || PokemonUniversal3D || PokemonLostSilver || German || DarkfireMode || OneYearLater3D || Others.Count > 0)
                    return true;
                else
                    return _OfflineMode;
            }
            set
            {
                _OfflineMode = value;
            }
        }

        public List<string> GetGameMode()
        {
            List<string> _GameMode = new List<string>();

            if (!_GameMode.Contains("Pokemon 3D") && Pokemon3D)
                _GameMode.Add("Pokemon 3D");

            if (!_GameMode.Contains("Pokemon Silver's Soul") && PokemonSilversSoul)
                _GameMode.Add("Pokemon Silver's Soul");

            if (!_GameMode.Contains("Pokemon Brown 3D") && PokemonBrown3D)
                _GameMode.Add("Pokemon Brown 3D");

            if (!_GameMode.Contains("Pokemon Gold&Silver - RandomLocke") && PokemonGoldSilverRandomLocke)
                _GameMode.Add("Pokemon Gold&Silver - RandomLocke");

            if (!_GameMode.Contains("Pokemon Universal 3D") && PokemonUniversal3D)
                _GameMode.Add("Pokemon Universal 3D");

            if (!_GameMode.Contains("Pokemon Lost Silver") && PokemonLostSilver)
                _GameMode.Add("Pokemon Lost Silver");

            if (!_GameMode.Contains("German") && German)
                _GameMode.Add("German");

            if (!_GameMode.Contains("Darkfire Mode") && DarkfireMode)
                _GameMode.Add("Darkfire Mode");

            if (!_GameMode.Contains("1 Year Later 3D") && OneYearLater3D)
                _GameMode.Add("1 Year Later 3D");

            if (Others.Count > 0)
            {
                foreach (string item in Others)
                {
                    if (!_GameMode.Contains(item))
                        _GameMode.Add(item);
                }
            }

            return _GameMode;
        }

        public override string ToString()
        {
            return string.Join(",", GetGameMode());
        }
    }
}
