using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine.SceneManagement
{
    public class SceneInstance : Object
    {
        internal List<GameObject> m_RootGameObjects = new();

        public int buildIndex { get; set; } = -1;

        public string path { get; set; } = "";

        public bool isLoaded { get; set; }

        public bool isDirty { get; set; }

        public bool isSubScene { get; set; }

        public int rootCount => m_RootGameObjects.Count;

        public void AddRootGameObject(GameObject obj)
        {
            m_RootGameObjects.Add(obj);
        }
    }
}