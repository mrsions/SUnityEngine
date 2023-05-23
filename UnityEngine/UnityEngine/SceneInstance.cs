using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public class SceneInstance : Component
    {
        public string path { get; set; }

        public string name { get; set; }

        public bool isLoaded { get; set; }

        public int isDirty { get; set; }

        public int rootCount { get; set; }

        public bool isSubScene { get; set; }

        internal GameObject[] GetRootGameObjects()
        {
            throw new NotImplementedException();
        }

        internal void GetRootGameObjects(List<GameObject> rootGameObjects)
        {
            throw new NotImplementedException();
        }
    }
}