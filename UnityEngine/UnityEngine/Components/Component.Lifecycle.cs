using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public partial class Component : Object
    {
        public virtual void Awake() { }
        public virtual void OnEnable() { }
        public virtual void OnDisabled() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
    }
}