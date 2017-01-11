using Modules.System.IO;
using Pokemon_3D_Server_Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SystemInfoLibrary.OperatingSystem;
using static Pokemon_3D_Server_Core.Collections.LogTypeCollection;

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

                string errorLog = $@"[CODE]
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
        Physical count: {(Type.GetType("Mono.Runtime") != null ? "0" : osInfo.Hardware.GPUs.Count.ToString())}
        Name: {(Type.GetType("Mono.Runtime") != null ? "" : osInfo.Hardware.GPUs.First().Name)}
        Brand: {(Type.GetType("Mono.Runtime") != null ? "" : osInfo.Hardware.GPUs.First().Brand)}
        Resolution: {(Type.GetType("Mono.Runtime") != null ?  "0x0" : osInfo.Hardware.GPUs.First().Resolution)} {(Type.GetType("Mono.Runtime") != null ? "0" : osInfo.Hardware.GPUs.First().RefreshRate.ToString())} Hz
        Memory Total: {(Type.GetType("Mono.Runtime") != null ? "0" : osInfo.Hardware.GPUs.First().MemoryTotal.ToString())} KB

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

                DateTime errorTime = DateTime.Now;
                int randomIndetifier = MathHelper.Random(0, int.MaxValue);
                string Path = $"{Core.Settings.Directories.CrashLogDirectory}/Crash_{errorTime.ToString("yyyy-MM-dd_HH.mm.ss")}.{randomIndetifier.ToString("0000000000")}.dat".GetFullPath();

                using (FileStream fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8) { AutoFlush = true })
                        writer.Write(errorLog);
                }

                Core.Logger.Log(ex.Message + Environment.NewLine + $"Error Log saved at: {Path}", LogTypes.Error);
            }
            catch (Exception ex2)
            {
                Core.Logger.Log(ex2.Message + Environment.NewLine + GenerateInnerExceptionStackTrace(ex2), LogTypes.Error);
            }
        }

        private static string GenerateInnerExceptionMessage(Exception ex)
        {
            Exception exceptionRef = ex;
            int innerExceptionCount = 1;
            string returnString = "";

            if (ex.InnerException == null)
                returnString = "Nothing";
            else
            {
                do
                {
                    if (innerExceptionCount == 1)
                        returnString += exceptionRef.InnerException.Message;
                    else
                        returnString += Environment.NewLine + $"InnerException {innerExceptionCount.ToString()}: {exceptionRef.InnerException.Message}";

                    innerExceptionCount++;
                    exceptionRef = exceptionRef.InnerException;
                } while (exceptionRef.InnerException != null);
            }

            return returnString;
        }

        private static string GenerateInnerExceptionStackTrace(Exception ex)
        {
            Exception exceptionRef = ex;
            List<string> exceptionStackTrace = new List<string>();
            string returnString;

            if (string.IsNullOrEmpty(ex.StackTrace))
                exceptionStackTrace.Add(string.Join(Environment.NewLine, Environment.StackTrace.Split('\n').Skip(3).ToArray()));
            else
                exceptionStackTrace.Add(ex.StackTrace);

            while (exceptionRef.InnerException != null)
            {
                exceptionStackTrace.Add(exceptionRef.InnerException.StackTrace);
                exceptionRef = exceptionRef.InnerException;
            }

            exceptionStackTrace.Reverse();

            returnString = string.Join(Environment.NewLine, exceptionStackTrace.ToArray());

            return returnString;
        }
    }
}
