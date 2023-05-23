using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public static class Debug
    {
        public static void Log(object msg) => Console.WriteLine(msg);
        public static void LogInfo(object msg) => Console.WriteLine(msg);
        public static void LogWarning(object msg) => Console.WriteLine(msg);
        public static void LogError(object msg) => Console.WriteLine(msg);
        public static void LogException(Exception ex) => Console.WriteLine(ex);
    }
}