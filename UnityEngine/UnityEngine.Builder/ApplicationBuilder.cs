using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

namespace UnityEngine.Builder
{
    public delegate string ConfigurePathDelegate();

    public delegate void ConfigureSceneManagerDelegate(ISceneManager sceneManager);

    public delegate void ConfigureResourcesDelegate(IResources sceneManager);

    public class ApplicationBuilder
    {

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private string? m_BasePath;
        private string? m_CachePath;

        private SceneManagerImpl? m_SceneManager;
        private ResourcesImpl? m_Resources;

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHOD
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public ApplicationBuilder()
        {

        }

        public ApplicationBuilder ConfigureBasePath(string basePath)
        {
            m_BasePath = basePath;
            return this;
        }

        public ApplicationBuilder ConfigureBasePath(ConfigurePathDelegate builder)
        {
            m_BasePath = builder();
            return this;
        }

        public ApplicationBuilder ConfigureCachePath(string basePath)
        {
            m_BasePath = basePath;
            return this;
        }

        public ApplicationBuilder ConfigureCachePath(ConfigurePathDelegate builder)
        {
            m_BasePath = builder();
            return this;
        }

        public ApplicationBuilder ConfigureSceneManager(ConfigureSceneManagerDelegate builder)
        {
            m_SceneManager = new SceneManagerImpl();
            builder(m_SceneManager);
            return this;
        }

        public ApplicationBuilder ConfigureResources(ConfigureResourcesDelegate builder)
        {
            m_Resources = new ResourcesImpl();
            builder(m_Resources);
            return this;
        }

        public IApplication Build()
        {
            Application.dataPath = SolvePath(Path.GetFullPath(m_BasePath ?? "."));
            Application.temporaryCachePath = m_CachePath ?? Path.Combine(Path.GetTempPath(), Process.GetCurrentProcess().ProcessName);
            Application.temporaryCachePath = SolvePath(Path.GetFullPath(Application.temporaryCachePath));

            ApplicationImpl app = new ApplicationImpl(
                resources: m_Resources ?? new ResourcesImpl()
                , sceneManager: m_SceneManager ?? new SceneManagerImpl());

            Application.impl = app;
            Resources.impl = app.Resources;
            SceneManager.impl = app.SceneManager;

            return app;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    CONTAINER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private string SolvePath(string path)
        {
            path = path.Replace('\\', '/');
            if (path.EndsWith('/'))
            {
                path = path.Substring(0, path.Length - 1);
            }
            return path;
        }

    }
}