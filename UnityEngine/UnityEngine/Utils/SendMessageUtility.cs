using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public static class SendMessageUtility
    {
        public static void SendMessage(GameObject obj, string methodName, SendMessageOptions options) { }
        public static void SendMessage(GameObject obj, string methodName, object param, SendMessageOptions options) { }

        public static void SendMessageUpwards(GameObject obj, string methodName, SendMessageOptions options) { }
        public static void SendMessageUpwards(GameObject obj, string methodName, object param, SendMessageOptions options) { }

        public static void BroadcastMessage(GameObject obj, string methodName, SendMessageOptions options) { }
        public static void BroadcastMessage(GameObject obj, string methodName, object param, SendMessageOptions options) { }
    }
}