using Modules.System.IO;
using Pokemon_3D_Server_Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SystemInfoLibrary.OperatingSystem;
using static Pokemon_3D_Server_Core.Settings.Logger;

namespace Modules.System
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Catch exception and create a crash log.
        /// </summary>
        /// <param name="ex">Represents errors that occur during application execution.</param>
        public static void CatchError(this Exception ex)
        {
            try
            {
                OperatingSystemInfo osInfo = OperatingSystemInfo.GetOperatingSystemInfo();

                string ErrorLog = $@"[CODE]
Pokemon 3D Server Application Crash Log v{Core.Settings.Version}
--------------------------------------------------

System specifications:

Software:

    OS: {osInfo.Name} {osInfo.Architecture} [{(Type.GetType("Mono.Runtime") != null ? "Mono" : ".NET")}]
    Language: {CultureInfo.CurrentCulture.EnglishName}, LCID {osInfo.LocaleID}
    Framework: Version {osInfo.FrameworkVersion}

Hardware:

    CPU:
        Physical count: {osInfo.Hardware.CPUs.Count}
        Name: {osInfo.Hardware.CPUs.First().Name}
        Brand: {osInfo.Hardware.CPUs.First().Brand}
        Architecture: {osInfo.Hardware.CPUs.First().Architecture}
        Cores: {osInfo.Hardware.CPUs.First().Cores}

    GPU:
        Physical count: {osInfo.Hardware.GPUs.Count}
        Name: {osInfo.Hardware.GPUs.First().Name}
        Brand: {osInfo.Hardware.GPUs.First().Brand}
        Resolution: {osInfo.Hardware.GPUs.First().Resolution} {osInfo.Hardware.GPUs.First().RefreshRate} Hz
        Memory Total: {osInfo.Hardware.GPUs.First().MemoryTotal} KB

    RAM:
        Memory Total: {osInfo.Hardware.RAM.Total} KB
        Memory Free: {osInfo.Hardware.RAM.Free} KB

--------------------------------------------------

Error information:

Message: {ex.Message}
InnerException: {GenerateInnerExceptionMessage(ex)}
HelpLink: {(string.IsNullOrEmpty(ex.HelpLink) ? "Empty" : ex.HelpLink)}
Source: {ex.Source}

--------------------------------------------------

CallStack:

{GenerateInnerExceptionStackTrace(ex)}

--------------------------------------------------

You should report this error if it is reproduceable or you could not solve it by yourself.

Go To: <INSERTURL> to report this crash.
[/CODE]";

                DateTime ErrorTime = DateTime.Now;
                int RandomIndetifier = MathHelper.Random(0, int.MaxValue);
                string Path = $"{Core.Settings.Directories.CrashLogDirectory}/Crash_{ErrorTime.ToString("yyyy-MM-dd_HH.mm.ss")}.{RandomIndetifier.ToString("0000000000")}.dat".GetFullPath();

                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter Writer = new StreamWriter(FileStream, Encoding.UTF8) { AutoFlush = true })
                        Writer.Write(ErrorLog);
                }

                Core.Logger.Log(ex.Message + Environment.NewLine + $"Error Log saved at: {Path}", LogTypes.Error);
            }
            catch (Exception ex2)
            {
                Core.Logger.Log(ex2.Message, LogTypes.Error);
            }
        }

        private static string GenerateInnerExceptionMessage(Exception ex)
        {
            Exception ExceptionRef = ex;
            int InnerExceptionCount = 1;
            string ReturnString = "";

            if (ex.InnerException == null)
                ReturnString = "Nothing";
            else
            {
                do
                {
                    if (InnerExceptionCount == 1)
                        ReturnString += ExceptionRef.InnerException.Message;
                    else
                        ReturnString += Environment.NewLine + $"InnerException {InnerExceptionCount.ToString()}: {ExceptionRef.InnerException.Message}";

                    InnerExceptionCount++;
                    ExceptionRef = ExceptionRef.InnerException;
                } while (ExceptionRef.InnerException != null);
            }

            return ReturnString;
        }

        private static string GenerateInnerExceptionStackTrace(Exception ex)
        {
            Exception ExceptionRef = ex;
            List<string> ExceptionStackTrace = new List<string>();
            string ReturnString;

            if (string.IsNullOrEmpty(ex.StackTrace))
                ExceptionStackTrace.Add(string.Join(Environment.NewLine, Environment.StackTrace.Split('\n').Skip(3).ToArray()));
            else
                ExceptionStackTrace.Add(ex.StackTrace);

            while (ExceptionRef.InnerException != null)
            {
                ExceptionStackTrace.Add(ExceptionRef.InnerException.StackTrace);
                ExceptionRef = ExceptionRef.InnerException;
            }

            ExceptionStackTrace.Reverse();

            ReturnString = string.Join(Environment.NewLine, ExceptionStackTrace.ToArray());

            return ReturnString;
        }
    }
}
