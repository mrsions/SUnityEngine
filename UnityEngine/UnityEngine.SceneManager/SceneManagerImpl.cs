using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.SceneManagement
{
    public interface ISceneManager
    {
    }
    internal class SceneManagerImpl: ISceneManager
    {
        public SceneInstance? m_ActiveScene;

        public List<SceneInstance> m_LoadedScenes = new List<SceneInstance>();

        public void Dispose()
        {
        }

        public SceneInstance? GetActiveScene()
        {
            return m_ActiveScene;
        }

        public bool SetActiveScene(SceneInstance? scene)
        {
            if (scene == null || !scene.isLoaded)
            {
                return false;
            }

            m_ActiveScene = scene;
            return true;
        }

        public SceneInstance? GetSceneByPath(string scenePath)
        {
            for (int i = 0, iLen = m_LoadedScenes.Count; i < iLen; i++)
            {
                var scene = m_LoadedScenes[i];
                if (scene.path == scenePath)
                {
                    return scene;
                }
            }
            return default;
        }

        public SceneInstance? GetSceneByName(string name)
        {
            for (int i = 0, iLen = m_LoadedScenes.Count; i < iLen; i++)
            {
                var scene = m_LoadedScenes[i];
                if (scene.name == name)
                {
                    return scene;
                }
            }
            return default;
        }

        public SceneInstance? GetSceneByBuildIndex(int buildIndex)
        {
            for (int i = 0, iLen = m_LoadedScenes.Count; i < iLen; i++)
            {
                var scene = m_LoadedScenes[i];
                if (scene.buildIndex == buildIndex)
                {
                    return scene;
                }
            }
            return default;
        }

        public SceneInstance GetSceneAt(int index)
        {
            if (index < 0 || index >= m_LoadedScenes.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return m_LoadedScenes[(int)index];
        }

        public SceneInstance CreateScene(string sceneName)
        {
            var scene = new SceneInstance();
            scene.name = sceneName;
            scene.isDirty = false;
            scene.isLoaded = true;
            scene.isSubScene = false;
            m_LoadedScenes.Add(scene);
            return scene;
        }
    }
}