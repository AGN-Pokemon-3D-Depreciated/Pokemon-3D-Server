using Modules.System.IO;
using Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables;
using SQLite;
using System;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Launcher_Game_Module.SQLite
{
    public class SQLite : IDisposable
    {
        private Core Core;

        public SQLiteConnection Connection { get; private set; }

        public SQLite(Core core)
        {
            Core = core;
            Core.Logger.Log("SQLite Initialized.");
        }

        public void Start()
        {
            Connection = new SQLiteConnection($"{Core.BaseInstance.Settings.Directories.DataDirectory}/{Core.Settings.Features.SQLite.DatabaseName}.db".GetFullPath());
            Connection.CreateTable<PlayerInfo>(CreateFlags.AutoIncPK);
            Connection.CreateTable<BlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<IPBlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<MuteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<OperatorList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<WhiteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<TradeHistoryList>(CreateFlags.AutoIncPK);
        }

        public List<T> Query<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : new()
        {
            lock (Connection)
            {
                List<T> result = new List<T>();
                TableQuery<T> stm;

                if (where == null)
                {
                    stm = Connection.Table<T>();

                    if (stm.Count() > 0)
                    {
                        for (int i = 0; i < stm.Count(); i++)
                        {
                            T item = stm.ElementAt(i);
                            result.Add(item);
                        }
                    }
                }
                else
                {
                    stm = Connection.Table<T>().Where(where);

                    if (stm.Count() > 0)
                    {
                        for (int i = 0; i < stm.Count(); i++)
                        {
                            T item = stm.ElementAt(i);
                            result.Add(item);
                        }
                    }
                }

                return result;
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
            Core.Logger.Log("SQLite Disposed.");
        }
    }
}
