using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class GameObject //Messaging 
    {
        public void SendMessage(string method, SendMessageOptions options = default)
        {

        }

        public void SendMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {

        }

        public void SendMessageUpwards(string method, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {

        }

        public void SendMessageUpwards(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {

        }

        public void BroadcastMessage(string method, SendMessageOptions options = default)
        {

        }

        public void BroadcastMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver)
        {

        }

    }
}