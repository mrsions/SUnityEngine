using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public static class Application
    {
        private static volatile int s_InstanceIDGenerator;

        internal static ApplicationWorker s_Worker = new();

        public static string temporaryCachePath
        {
            get => Path.Combine(Path.GetTempPath(), Process.GetCurrentProcess().ProcessName);
        }
        public static string dataPath
        {
            get => Path.GetFullPath(".");
        }


        internal static int NewInstanceId()
        {
            return s_InstanceIDGenerator++;
        }

        public static void Run()
        {
        }
    }
}