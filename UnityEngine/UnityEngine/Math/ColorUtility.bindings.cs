// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using UnityEngine.Bindings;

namespace UnityEngine
{
    [NativeHeader("Runtime/Export/Math/ColorUtility.bindings.h")]
    public partial class ColorUtility
    {
        [FreeFunction]
        [Obsolete("Not Support. please make extern code.", true)] internal static bool DoTryParseHtmlColor(string htmlString, out Color32 color) { color = default; return default; }
    }
}
