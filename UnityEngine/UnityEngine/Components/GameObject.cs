using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class GameObject : Object
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    STATIC
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public static GameObject CreatePrimitive(PrimitiveType type)
        {
            return default;
        }

        public static GameObject FindWithTag(string tag)
        {
            return default;
        }

        public static GameObject FindGameObjectWithTag(string tag)
        {
            return default;
        }

        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            return default;
        }

        public static GameObject[] Find(string name)
        {
            return default;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public GameObject gameObject => this;

        public Transform transform { get; internal set; }

        public string tag { get; set; }

        public int layer { get; set; }
        
        public bool activeSelf { get; set; }

        public bool activeInHierarchy
        {
            get
            {
                return default;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHODS
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public GameObject(string name = null, params Type[] components)
        {

        }

        public bool CompareTag(string tag)
        {
            return this.tag == tag;
        }

        public void SetActive(bool value)
        {

        }





    }
}