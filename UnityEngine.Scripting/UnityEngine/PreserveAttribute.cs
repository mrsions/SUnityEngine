using System;

namespace UnityEngine.Scripting
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Struct)]
    public class PreserveAttribute : Attribute
    {
    }
}