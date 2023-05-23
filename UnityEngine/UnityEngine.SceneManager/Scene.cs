
using System.Diagnostics.CodeAnalysis;

namespace UnityEngine.SceneManagement
{
    public struct Scene
    {
        internal SceneInstance? m_Instance;

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

        public string? path => m_Instance?.path ?? "";

        public string? name => m_Instance?.name ?? "";

        public bool isLoaded => m_Instance?.isLoaded ?? false;

        public bool isDirty => m_Instance?.isDirty ?? false;

        public int rootCount => m_Instance?.rootCount ?? 0;

        public bool isSubScene => m_Instance?.isSubScene ?? true;

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHODS
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public bool IsValid()
        {
            return m_Instance != null;
        }

        public GameObject[] GetRootGameObjects()
        {
            ThrowIfInvalid();
            return m_Instance.m_RootGameObjects.ToArray();
        }

        public void GetRootGameObjects(List<GameObject> rootGameObjects)
        {
            ThrowIfInvalid();
            rootGameObjects.Clear();
            rootGameObjects.AddRange(m_Instance.m_RootGameObjects);
        }

        public override int GetHashCode()
        {
            return m_Instance?.GetHashCode() ?? 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Scene other)
            {
                return false;
            }

            return this.m_Instance == other.m_Instance;
        }

        [MemberNotNull(nameof(m_Instance))]
        private void ThrowIfInvalid()
        {
            if (m_Instance == null)
            {
                throw new ArgumentException("The scene is invalid.");
            }
        }

        public static explicit operator SceneInstance?(Scene scene)
        {
            return scene.m_Instance;
        }

        public static explicit operator Scene(SceneInstance? scene)
        {
            return new Scene { m_Instance = scene };
        }
    }
}