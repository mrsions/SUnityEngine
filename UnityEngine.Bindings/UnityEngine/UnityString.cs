
using System.Globalization;

namespace UnityEngine
{
    public sealed class UnityString
    {
        public static string Format(string fmt, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);
        }
    }
}