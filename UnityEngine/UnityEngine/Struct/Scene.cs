
namespace UnityEngine
{
    public class Scene
    {
        private SceneInstance m_Instance;

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    STATIC
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public string path => m_Instance.path;

        public string name => m_Instance.name;

        public bool isLoaded => m_Instance.isLoaded;

        public int isDirty => m_Instance.isDirty;

        public int rootCount => m_Instance.rootCount;

        public bool isSubScene => m_Instance.isSubScene;

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHODS
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        internal Scene(SceneInstance instance)
        {
            m_Instance = instance;
        }

        public bool IsValid()
        {
            return m_Instance != null;
        }

        public GameObject[] GetRootGameObjects()
        {
            return m_Instance.GetRootGameObjects();
        }

        public void GetRootGameObjects(List<GameObject> rootGameObjects)
        {
            m_Instance.GetRootGameObjects(rootGameObjects);
        }

        public override int GetHashCode()
        {
            return m_Instance.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Scene other)
            {
                return false;
            }

            return this.m_Instance == other.m_Instance;
        }

        public override string ToString()
        {
            return name;
        }
    }
}