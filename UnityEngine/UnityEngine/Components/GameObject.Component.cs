using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class GameObject //Component
    {
        private const int SIZE_GET_COMPONENTS = 5;

        private List<Component> m_Components = new List<Component>();
        private LinkedList<Component> m_Awaked = new LinkedList<Component>();

        #region AddComponent
        public T AddComponent<T>() where T : Component
        {
            return (T)AddComponent(typeof(T));
        }

        public Component AddComponent(Type type)
        {
            var comp = (Component?)Activator.CreateInstance(type)
                ?? throw new ArgumentException($"Not found component '{type.FullName}'");

            if (comp is Transform transform)
            {
                this.transform = transform;
            }

            m_Components.Add(comp);

            if (activeInHierarchy)
            {
                EnsureAwake(comp);
            }
            else
            {
                m_Awaked.AddLast(comp);
            }

            return comp;
        }

        private static void EnsureAwake(Component comp)
        {
            try
            {
                comp.Awake();

                if (comp.enabled)
                {
                    Application.impl.Worker.AddWork(comp);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                comp.enabled = false;
            }
        }
        #endregion

        #region GetComponent
        public T? GetComponent<T>()
        {
            for (int i = 0, iLen = m_Components.Count; i < iLen; i++)
            {
                var comp = m_Components[i];
                if (comp is T t)
                {
                    return t;
                }
            }
            return default;
        }

        public bool TryGetComponent<T>(out T? component)
        {
            component = GetComponent<T>();
            return component != null;
        }

        public T[] GetComponents<T>()
        {
            List<T> result = new List<T>(SIZE_GET_COMPONENTS);
            GetComponents<T>(result);
            return result.ToArray();
        }

        public void GetComponents<T>(List<T> result)
        {
            result.Clear();
            GetComponentsInternal(result);
        }

        private void GetComponentsInternal<T>(List<T> result)
        {
            for (int i = 0, iLen = m_Components.Count; i < iLen; i++)
            {
                var comp = m_Components[i];
                if (comp is T t)
                {
                    result.Add(t);
                }
            }
        }
        #endregion GetComponent

        #region GetComponentChildren
        public T? GetComponentInChildren<T>(bool includeInactive = false)
        {
            T? comp = GetComponent<T>();
            if (comp != null) return comp;

            for (int i = 0, len = transform.childCount; i < len; i++)
            {
                Transform child = transform.GetChild(i);

                if (!child.gameObject.activeSelf) continue;

                comp = child.gameObject.GetComponentInChildren<T>(includeInactive);
                if (comp != null) return comp;
            }

            return default;
        }

        public T[] GetComponentsInChildren<T>(bool includeInactive = false)
        {
            List<T> result = new List<T>(SIZE_GET_COMPONENTS);
            GetComponentsInChildren<T>(includeInactive, result);
            return result.ToArray();
        }

        public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
        {
            result.Clear();
            GetComponentsInChildrenInternal(includeInactive, result);
        }

        private void GetComponentsInChildrenInternal<T>(bool includeInactive, List<T> result)
        {
            GetComponentsInternal(result);

            for (int i = 0, len = transform.childCount; i < len; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.gameObject.activeSelf)
                {
                    child.gameObject.GetComponentsInChildrenInternal<T>(includeInactive, result);
                }
            }
        }
        #endregion GetComponentChildren

        #region GetComponentInParent
        public T? GetComponentInParent<T>(bool includeInactive = false)
        {
            Transform? cursor = transform;
            while (cursor != null)
            {
                if (!includeInactive && cursor.gameObject.activeInHierarchy)
                {
                    continue;
                }

                T? comp = GetComponent<T>();
                if (comp != null)
                {
                    return comp;
                }

                cursor = cursor.parent;
            }

            return default;
        }

        public T[] GetComponentsInParent<T>(bool includeInactive = false)
        {
            List<T> result = new List<T>(SIZE_GET_COMPONENTS);
            GetComponentsInParentInternal(includeInactive, result);
            return result.ToArray();
        }

        public void GetComponentsInParent<T>(bool includeInactive, List<T> result)
        {
            result.Clear();
            GetComponentsInParent(includeInactive, result);
        }

        private void GetComponentsInParentInternal<T>(bool includeInactive, List<T> result)
        {
            Transform? cursor = transform;
            while (cursor != null)
            {
                if (!includeInactive && cursor.gameObject.activeInHierarchy)
                {
                    continue;
                }

                GetComponentsInternal(result);

                cursor = cursor.parent;
            }
        }
        #endregion GetComponentInParent
    }
}