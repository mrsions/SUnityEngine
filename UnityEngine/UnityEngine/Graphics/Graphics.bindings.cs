// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
    [NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
    public sealed partial class GeometryUtility
    {
        [Obsolete("Not Support. please make extern code.", true)] public static bool TestPlanesAABB(Plane[] planes, Bounds bounds) => default;

        [Obsolete("Not Support. please make extern code.", true)] [NativeName("ExtractPlanes")] private static void Internal_ExtractPlanes(Plane[] planes, Matrix4x4 worldToProjectionMatrix) { }
        [Obsolete("Not Support. please make extern code.", true)] [NativeName("CalculateBounds")]  private static Bounds Internal_CalculateBounds(Vector3[] positions, Matrix4x4 transform) => default;
    }
}
