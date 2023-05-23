using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    //
    // 요약:
    //     Options for how to send a message.
    public enum SendMessageOptions
    {
        //
        // 요약:
        //     A receiver is required for SendMessage.
        RequireReceiver,
        //
        // 요약:
        //     No receiver is required for SendMessage.
        DontRequireReceiver
    }
}