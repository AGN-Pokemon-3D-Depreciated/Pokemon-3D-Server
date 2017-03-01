using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pokemon_3D_Server_Launcher_Core.Settings
{
    public abstract class BaseSettings : ISettings
    {
        protected ICore Core;

        public abstract Dictionary<string, bool> LogTypes { get; set; }

        protected BaseSettings()
        {
        }

        protected BaseSettings(ICore core)
        {
            Core = core;
        }

        public abstract void Load();

        public abstract void Save();

        public virtual T GetSettings<T>(string setting)
        {
            string[] propertyName = setting.Split('.');
            object result = this;
            PropertyInfo[] properites;

            for (int i = 0; i < propertyName.Length; i++)
            {
                properites = result.GetType().GetProperties().Where(a => a.Name == propertyName[i]).ToArray();

                if (properites.Length == 0)
                {
                    result = null;
                    break;
                }
                else
                    result = properites[0].GetValue(result);
            }

            return (T)result;
        }

        public virtual void SetSettings(string setting, object value)
        {
            string[] propertyName = setting.Split('.');
            object result = this;
            PropertyInfo[] properites;

            for (int i = 0; i < propertyName.Length; i++)
            {
                properites = result.GetType().GetProperties().Where(a => a.Name == propertyName[i]).ToArray();

                if (properites.Length == 0)
                {
                    result = null;
                    break;
                }
                else
                {
                    if (i + 1 == propertyName.Length)
                        properites[0].SetValue(result, value);
                    else
                        result = properites[0].GetValue(result);
                }
            }
        }
    }
}