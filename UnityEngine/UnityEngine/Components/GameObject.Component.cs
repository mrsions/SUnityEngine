using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class GameObject //Component
    {
        #region AddComponent
        public T AddComponent<T>()
        {
            return default;
        }
        #endregion

        #region GetComponent
        public T GetComponent<T>()
        {
            return default;
        }

        public bool TryGetComponent<T>(out T component)
        {
            component = default;
            return default;
        }

        public T[] GetComponents<T>()
        {
            return default;
        }

        public T[] GetComponents<T>(List<T> result)
        {
            return default;
        }
        #endregion GetComponent

        #region GetComponentChildren
        public T GetComponentInChildren<T>(bool includeInactive = false)
        {
            return default;
        }

        public T[] GetComponentsInChildren<T>(bool includeInactive = false)
        {
            return default;
        }

        public void GetComponentsInChildren<T>(bool includeInactive = false, List<T> result = null)
        {
        }
        #endregion GetComponentChildren

        #region GetComponentInParent
        public T GetComponentInParent<T>()
        {
            return default;
        }

        public T[] GetComponentsInParent<T>(bool includeInactive = false)
        {
            return default;
        }

        public void GetComponentsInParent<T>(bool includeInactive = false, List<T> result = null)
        {

        }
        #endregion GetComponentInParent
    }
}