using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.SceneManagement
{
    public static class SceneManager
    {
        internal static SceneManagerImpl impl = null!;

        public static Scene GetActiveScene() => (Scene)impl.GetActiveScene();
        public static bool SetActiveScene(Scene scene) => impl.SetActiveScene((SceneInstance?)scene);
        public static Scene GetSceneByPath(string scenePath) => (Scene)impl.GetSceneByPath(scenePath);
        public static Scene GetSceneByName(string name) => (Scene)impl.GetSceneByName(name);
        public static Scene GetSceneByBuildIndex(int buildIndex) => (Scene)impl.GetSceneByBuildIndex(buildIndex);
        public static Scene GetSceneAt(int index) => (Scene)impl.GetSceneAt(index);
        public static Scene CreateScene(string sceneName) => (Scene)impl.CreateScene(sceneName);

        public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) => throw new NotImplementedException();
        public static void LoadScene(int sceneBuildIndex, LoadSceneMode mode = LoadSceneMode.Single) => throw new NotImplementedException();
        public static void UnloadScene(Scene scene) => throw new NotImplementedException();
        public static void UnloadScene(int sceneBuildIndex) => throw new NotImplementedException();
        public static void UnloadScene(string sceneName) => throw new NotImplementedException();
        public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) => throw new NotImplementedException();
        public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneMode mode = LoadSceneMode.Single) => throw new NotImplementedException();
        public static AsyncOperation UnloadSceneAsync(Scene scene) => throw new NotImplementedException();
        public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex) => throw new NotImplementedException();
        public static AsyncOperation UnloadSceneAsync(string sceneName) => throw new NotImplementedException();
    }
}