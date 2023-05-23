using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public static class Random
    {
        static readonly ThreadLocal<System.Random> rnd = new(() => new System.Random());

        public static float value => (float)rnd.Value!.NextDouble();
        public static double valueDouble => rnd.Value!.NextDouble();

        public static int Range(int min, int max) => rnd.Value!.Next(min, max);
        public static float Range(float min, float max) => (float)(min + ((max - min) * rnd.Value!.NextDouble()));
    }
}