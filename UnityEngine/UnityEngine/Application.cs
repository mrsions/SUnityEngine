using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public class Application
    {
        internal static ApplicationImpl impl = null!;

        public static string temporaryCachePath { get; internal set; } = null!;

        public static string dataPath { get; internal set; } = null!;

        public static bool isPlaying { get => impl != null && impl.isPlaying; }

    }
}