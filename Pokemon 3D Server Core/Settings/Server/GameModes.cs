using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server
{
    public class GameModes
    {
        private List<string> _GameMode = new List<string>();
        /// <summary>
        /// Get GameMode List.
        /// </summary>
        [YamlIgnore]
        public List<string> GameMode
        {
            get
            {
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
                    foreach (string Item in Others)
                    {
                        if (!_GameMode.Contains(Item))
                            _GameMode.Add(Item);
                    }
                }

                return _GameMode;
            }
        }

        /// <summary>
        /// Get/Set Pokemon 3D GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon 3D", Order = 1)]
        public bool Pokemon3D { get; set; } = true;

        /// <summary>
        /// Get/Set Pokemon Silver's Soul GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon Silver Soul", Order = 2)]
        public bool PokemonSilversSoul { get; set; } = false;

        /// <summary>
        /// Get/Set Pokemon Brown 3D GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon Brown 3D", Order = 3)]
        public bool PokemonBrown3D { get; set; } = false;

        /// <summary>
        /// Get/Set Pokemon Gold Silver - RandomLocke GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon Gold&Silver - RandomLocke", Order = 4)]
        public bool PokemonGoldSilverRandomLocke { get; set; } = false;

        /// <summary>
        /// Get/Set Pokemon Universal 3D GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon Universal 3D", Order = 5)]
        public bool PokemonUniversal3D { get; set; } = false;

        /// <summary>
        /// Get/Set Pokemon Lost Silver GameMode.
        /// </summary>
        [YamlMember(Alias = "Pokemon Lost Silver", Order = 6)]
        public bool PokemonLostSilver { get; set; } = false;

        /// <summary>
        /// Get/Set German
        /// </summary>
        [YamlMember(Alias = "German", Order = 7)]
        public bool German { get; set; } = false;

        /// <summary>
        /// Get/Set DarkfireMode
        /// </summary>
        [YamlMember(Alias = "Darkfire Mode", Order = 8)]
        public bool DarkfireMode { get; set; } = false;

        /// <summary>
        /// Get/Set 1YearLater3D
        /// </summary>
        [YamlMember(Alias = "1 Year Later 3D", Order = 9)]
        public bool OneYearLater3D { get; set; } = false;

        /// <summary>
        /// Get/Set List of GameMode.
        /// </summary>
        [YamlMember(Alias = "Others", Order = 10)]
        public List<string> Others { get; set; } = new List<string>();

        /// <summary>
        /// Get if offline mode is needed.
        /// </summary>
        public bool NeedOfflineMode()
        {
            if (PokemonSilversSoul || PokemonBrown3D || PokemonGoldSilverRandomLocke || PokemonUniversal3D || PokemonLostSilver || German || DarkfireMode || OneYearLater3D || Others.Count > 0)
                return true;
            else
                return false;
        }
    }
}
