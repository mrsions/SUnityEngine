// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License


namespace UnityEngine.Bindings
{
    public class FreeFunctionAttribute : Attribute
    {
        public string? Name { get; set; }
        public bool IsThreadSafe { get; set; }
        public bool HasExplicitThis { get; set; }

        public FreeFunctionAttribute(string? name = null)
        {
            Name = name;
        }
    }
}