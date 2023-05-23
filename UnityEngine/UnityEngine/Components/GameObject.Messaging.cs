using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class GameObject //Messaging 
    {
        public void SendMessage(string method, SendMessageOptions options = default)
        {
            SendMessageUtility.SendMessage(gameObject, method, options);
        }

        public void SendMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {
            SendMessageUtility.SendMessage(gameObject, method, parameter, options);
        }

        public void SendMessageUpwards(string method, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {
            SendMessageUtility.SendMessageUpwards(gameObject, method, options);
        }

        public void SendMessageUpwards(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {
            SendMessageUtility.SendMessageUpwards(gameObject, method, parameter, options);
        }

        public void BroadcastMessage(string method, SendMessageOptions options = default)
        {
            SendMessageUtility.BroadcastMessage(gameObject, method, options);
        }

        public void BroadcastMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {
            SendMessageUtility.BroadcastMessage(gameObject, method, parameter, options);
        }

    }
}