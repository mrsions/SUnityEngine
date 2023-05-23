using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public partial class GameObject : Object
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    STATIC
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public static GameObject FindGameObjectWithTag(string tag)
        {
            return Resources.FindGameObjectWithTag(tag);
        }

        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            return Resources.FindGameObjectsWithTag(tag);
        }

        public static GameObject[] Find(string name)
        {
            return Resources.FindGameObjectWithName(name);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private bool m_Active;

        public GameObject gameObject => this;

        public Transform transform { get; internal set; }

        public string tag { get; set; } = String.Empty;

        public int layer { get; set; }

        public bool activeSelf => m_Active;

        public Scene scene { get; set; }

        public bool activeInHierarchy
        {
            get
            {
                if (!m_Active) return false;

                Transform? cursor = transform.parent;
                while (cursor != null)
                {
                    if (!cursor.gameObject.m_Active) return false;

                    cursor = cursor.parent;
                }

                return true;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHODS
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public GameObject(string? name = null, params Type[] components)
        {
            this.name = name ?? "New Game Object";

            scene = SceneManager.GetActiveScene();

            foreach (var type in components)
            {
                Component comp = AddComponent(type);
            }

            if(transform == null)
            {
                transform = AddComponent<Transform>();
            }
        }

        public bool CompareTag(string tag)
        {
            return this.tag == tag;
        }

        public void SetActive(bool value)
        {
            if (m_Active != value)
            {
                m_Active = value;

                if (activeInHierarchy)
                {
                    while (m_Awaked.First != null)
                    {
                        Component comp = m_Awaked.First.Value;

                        m_Awaked.RemoveFirst();

                        EnsureAwake(comp);
                    }
                }
            }
        }
    }
}