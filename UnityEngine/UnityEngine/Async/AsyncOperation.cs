using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
    //
    // 요약:
    //     Asynchronous operation coroutine.
    [StructLayout(LayoutKind.Sequential)]
    public class AsyncOperation : YieldInstruction
    {
        private Action<AsyncOperation>? m_completeCallback;
        private bool m_AllowSceneActivation;

        //
        // 요약:
        //     Has the operation finished? (Read Only)
        public bool isDone { get; internal set; }

        //
        // 요약:
        //     What's the operation's progress. (Read Only)
        public float progress { get; internal set; }

        //
        // 요약:
        //     Priority lets you tweak in which order async operation calls will be performed.
        public int priority { get; set; }

        //
        // 요약:
        //     Allow Scenes to be activated as soon as it is ready.
        public bool allowSceneActivation
        {
            get
            {
                return m_AllowSceneActivation;
            }
            set
            {
                m_AllowSceneActivation = value;
            }
        }

        public event Action<AsyncOperation> completed
        {
            add
            {
                if (isDone)
                {
                    value(this);
                }
                else
                {
                    m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(m_completeCallback, value);
                }
            }
            remove
            {
                m_completeCallback = (Action<AsyncOperation>?)Delegate.Remove(m_completeCallback, value);
            }
        }

        [RequiredByNativeCode]
        internal void InvokeCompletionEvent()
        {
            if (m_completeCallback != null)
            {
                m_completeCallback(this);
                m_completeCallback = null;
            }
        }
    }
}