using System;

namespace Pokemon_3D_Server_Launcher_Core.PlayerList
{
    public abstract class PlayerListEventArgs : EventArgs
    {
        public enum Operations
        {
            Add,
            Remove,
            Update
        }

        public Operations Operation { get; private set; }
        public int Id { get; private set; }
        public abstract string AdditionalValue { get; }

        public string Data { get { return ToString(); } }

        public PlayerListEventArgs(Operations operation, int id)
        {
            Operation = operation;
            Id = id;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {AdditionalValue}";
        }
    }
}