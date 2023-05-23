using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class Component : Object
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        internal bool m_hasStarted;
        internal bool m_Enabled;
        internal int m_ExecuteOrder;

        public bool enabled
        {
            get => m_Enabled;
            set
            {
                if (m_Enabled == value) return;

                m_Enabled = value;

                if (gameObject == null) return;

                if (value)
                {
                    Application.impl.Worker.AddWork(this);
                }
                else
                {
                    Application.impl.Worker.RemoveWork(this);
                }
            }
        }

        public bool isActiveAndEnabled => enabled && gameObject.activeInHierarchy;

        public GameObject gameObject { get; internal set; } = null!;

        public Transform transform => gameObject.transform;


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHOD
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public bool CompareTag(string tag) => gameObject.CompareTag(tag);


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    INHERIT
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public T? GetComponent<T>() => gameObject.GetComponent<T>();
        public bool TryGetComponent<T>(out T? component) => gameObject.TryGetComponent<T>(out component);
        public T[] GetComponents<T>() => gameObject.GetComponents<T>();
        public void GetComponents<T>(List<T> result) => gameObject.GetComponents<T>(result);
        public T? GetComponentInChildren<T>(bool includeInactive = false) => gameObject.GetComponentInChildren<T>(includeInactive);
        public T[] GetComponentsInChildren<T>(bool includeInactive = false) => gameObject.GetComponentsInChildren<T>(includeInactive);
        public void GetComponentsInChildren<T>(bool includeInactive, List<T> result) => gameObject.GetComponentsInChildren<T>(includeInactive, result);
        public T? GetComponentInParent<T>() => gameObject.GetComponentInParent<T>();
        public T[] GetComponentsInParent<T>(bool includeInactive = false) => gameObject.GetComponentsInParent<T>(includeInactive);
        public void GetComponentsInParent<T>(bool includeInactive, List<T> result) => gameObject.GetComponentsInParent<T>(includeInactive, result);

        public void SendMessage(string method, SendMessageOptions options = default) => gameObject.SendMessage(method, options);
        public void SendMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver) => gameObject.SendMessage(method, parameter, options);
        public void SendMessageUpwards(string method, SendMessageOptions options = SendMessageOptions.RequireReceiver) => gameObject.SendMessageUpwards(method, options);
        public void SendMessageUpwards(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver) => gameObject.SendMessageUpwards(method, parameter, options);
        public void BroadcastMessage(string method, SendMessageOptions options = default) => gameObject.BroadcastMessage(method, options);
        public void BroadcastMessage(string method, object parameter, SendMessageOptions options = SendMessageOptions.RequireReceiver) => gameObject.BroadcastMessage(method, parameter, options);

    }
}