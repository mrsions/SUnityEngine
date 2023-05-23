using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public static class Resources
    {
        internal static ResourcesImpl impl = null!;

        public static int NewInstanceId() => impl.NewInstanceId();

        public static GameObject[] FindGameObjectWithName(string name) => impl.FindGameObjectWithName(name);
        public static GameObject[] FindGameObjectsWithTag(string tag) => impl.FindGameObjectsWithTag(tag);
        public static GameObject FindGameObjectWithTag(string tag) => impl.FindGameObjectWithTag(tag);

    }
}