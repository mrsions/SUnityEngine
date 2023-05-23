using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public interface IResources
    {

    }
    internal class ResourcesImpl : IResources
    {
        private volatile int s_InstanceIDGenerator;

        public int NewInstanceId()
        {
            return s_InstanceIDGenerator++;
        }

        public GameObject[] FindGameObjectWithName(string name)
        {
            throw new NotImplementedException();
        }

        public GameObject[] FindGameObjectsWithTag(string tag)
        {
            throw new NotImplementedException();
        }

        public GameObject FindGameObjectWithTag(string tag)
        {
            throw new NotImplementedException();
        }

        //internal static Object Instantiate(Object original)
        //{
        //}
    }
}