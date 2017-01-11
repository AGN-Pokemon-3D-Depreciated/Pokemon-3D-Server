using Modules.System;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Player
{
    public class Player
    {
        #region Player Data
        /// <summary>
        /// Get/Set Player DataItem[0]
        /// </summary>
        public string GameMode { get; set; }

        private int _IsGameJoltPlayer;
        /// <summary>
        /// Get/Set Player DataItem[1]
        /// </summary>
        public bool IsGameJoltPlayer
        {
            get
            {
                return _IsGameJoltPlayer.ToBool();
            }
            set
            {
                _IsGameJoltPlayer = value.ToInt();
            }
        }

        /// <summary>
        /// Get/Set Player DataItem[2]
        /// </summary>
        public int GameJoltID { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[3]
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[4]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[5]
        /// </summary>
        public string LevelFile { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[6]
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Get/Set Player Position X
        /// </summary>
        public double Position_X
        {
            get
            {
                return Position.GetSplit(0).ToDouble();
            }
            set
            {
                Position = value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Z.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player Position Y
        /// </summary>
        public double Position_Y
        {
            get
            {
                return Position.GetSplit(1).ToDouble();
            }
            set
            {
                Position = Position_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Z.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player Position Z
        /// </summary>
        public double Position_Z
        {
            get
            {
                return Position.GetSplit(2).ToDouble();
            }
            set
            {
                Position = Position_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player DataItem[7]
        /// </summary>
        public int Facing { get; set; }

        private int _Moving;
        /// <summary>
        /// Get/Set Player DataItem[8]
        /// </summary>
        public bool Moving
        {
            get
            {
                return _Moving.ToBool();
            }
            set
            {
                _Moving = value.ToInt();
            }
        }

        /// <summary>
        /// Get/Set Player DataItem[9]
        /// </summary>
        public string Skin { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[10]
        /// </summary>
        public int BusyType { get; set; }

        private int _PokemonVisible;
        /// <summary>
        /// Get/Set Player DataItem[11]
        /// </summary>
        public bool PokemonVisible
        {
            get
            {
                return _PokemonVisible.ToBool();
            }
            set
            {
                _PokemonVisible = value.ToInt();
            }
        }

        /// <summary>
        /// Get/Set Player DataItem[12]
        /// </summary>
        public string PokemonPosition { get; set; }

        /// <summary>
        /// Get/Set Player PokemonPosition X
        /// </summary>
        public double PokemonPosition_X
        {
            get
            {
                return PokemonPosition.GetSplit(0).ToDouble();
            }
            set
            {
                PokemonPosition = value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Z.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player PokemonPosition Y
        /// </summary>
        public double PokemonPosition_Y
        {
            get
            {
                return PokemonPosition.GetSplit(1).ToDouble();
            }
            set
            {
                PokemonPosition = PokemonPosition_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Z.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player PokemonPosition Z
        /// </summary>
        public double PokemonPosition_Z
        {
            get
            {
                return PokemonPosition.GetSplit(2).ToDouble();
            }
            set
            {
                PokemonPosition = PokemonPosition_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator);
            }
        }

        /// <summary>
        /// Get/Set Player DataItem[13]
        /// </summary>
        public string PokemonSkin { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[14]
        /// </summary>
        public int PokemonFacing { get; set; }

        /// <summary>
        /// Get/Set Player Last Valid Game Data
        /// </summary>
        public Player LastValidGameData { get; set; }
        #endregion Player Data

        /// <summary>
        /// Get/Set Player ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Get Network
        /// </summary>
        public Networking Network { get; private set; }

        public Player(int id, Networking network, Package.Package p)
        {
            ID = id;
            Network = network;
        }
    }
}
