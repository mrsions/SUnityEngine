using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public class MonoBehaviour : Behaviour
    {
        public static void print(object message)
        {
            Debug.Log(message);
        }

        public bool IsInvoking() { return default; }
        public void CancelInvoke() { }
        public void Invoke(string methodName, float time) { }
        public void InvokeRepeating(string methodName, float time, float repeatRate) { }
        public bool IsInvoking(string methodName) { return default; }

        public Coroutine StartCoroutine(string methodName) { return default; }
        public Coroutine StartCoroutine(IEnumerator routine) { return default; }
        public void StopCoroutine(IEnumerator routine) { }
        public void StopCoroutine(Coroutine routine) { }
        public void StopCoroutine(string methodName) {  }
        public void StopAllCoroutines() { }

    }
}