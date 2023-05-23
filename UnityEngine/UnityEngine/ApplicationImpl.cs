using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public interface IApplication
    {
        void Run();
    }

    internal class ApplicationImpl : IApplication
    {
        public readonly ApplicationWorker Worker = new ApplicationWorker();
        public readonly ResourcesImpl Resources;
        public readonly SceneManagerImpl SceneManager;

        public bool isPlaying { get; set; }

        public ApplicationImpl(ResourcesImpl resources, SceneManagerImpl sceneManager)
        {
            Resources = resources;
            SceneManager = sceneManager;
        }

        void IApplication.Run()
        {
            isPlaying = true;
            while (true)
            {
                Time.NextFrame();

                Worker.UpdateFrame();

                Thread.Sleep(1);
            }
        }
    }
}