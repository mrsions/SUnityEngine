// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License


namespace UnityEngine.Bindings
{
    public class NativeHeaderAttribute : Attribute
    {
        private string v;

        public NativeHeaderAttribute(string v)
        {
            this.v = v;
        }
    }
}