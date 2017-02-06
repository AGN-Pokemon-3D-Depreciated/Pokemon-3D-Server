using Modules.System.IO;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Server.Game.SQLite.Tables;
using SQLite;
using System;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite
{
    public class SQLite : IModules, IDisposable
    {
        public string Name { get; } = "SQLite Database";
        public string Version { get; } = "0.54";

        public SQLiteConnection Connection { get; private set; }

        public void Start()
        {
            Connection = new SQLiteConnection($"{Core.Settings.Directories.DataDirectory}/{Core.Settings.Server.Game.Features.SQLite.DatabaseName}.db".GetFullPath(), true);
            Connection.CreateTable<PlayerInfo>(CreateFlags.AutoIncPK);
            Connection.CreateTable<BlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<IPBlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<MuteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<OperatorList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<WhiteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<TradeHistoryList>(CreateFlags.AutoIncPK);
        }

        public void Stop()
        {
            Connection.Close();
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

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Connection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}