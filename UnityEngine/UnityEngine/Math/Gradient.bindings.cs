﻿// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using UnityEngine.Bindings;

namespace UnityEngine
{
    // Color key used by Gradient
    [UsedByNativeCode]
    public struct GradientColorKey
    {
        // Gradient color key
        public GradientColorKey(Color col, float time)
        {
            color = col;
            this.time = time;
        }

        // color of key
        public Color color;

        // time of the key (0 - 1)
        public float time;
    }

    // Alpha key used by Gradient
    [UsedByNativeCode]
    public struct GradientAlphaKey
    {
        // Gradient alpha key
        public GradientAlphaKey(float alpha, float time)
        {
            this.alpha = alpha;
            this.time = time;
        }

        // alpha alpha of key
        public float alpha;

        // time of the key (0 - 1)
        public float time;
    }


    public enum GradientMode
    {
        Blend = 0,              // Keys will blend smoothly when the gradient is evaluated. (Default)
        Fixed = 1,              // An exact key color will be returned when the gradient is evaluated.
        PerceptualBlend = 2     // Keys will blend smoothly when the gradient is evaluated, using Oklab blending (https://bottosson.github.io/posts/oklab/)
    }

    // Gradient used for animating colors
    [StructLayout(LayoutKind.Sequential)]
    [RequiredByNativeCode]
    [NativeHeader("Runtime/Export/Math/Gradient.bindings.h")]
    [Obsolete("Not Support", true)]
    public class Gradient //: IEquatable<Gradient>
    {
        //[RequiredByNativeCode]
        //public Gradient()
        //{
        //}

        //~Gradient()
        //{
        //}

        //// Calculate color at a given time
        //[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
        //extern public Color Evaluate(float time);

        //extern public GradientColorKey[] colorKeys
        //{
        //    [FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)] get;
        //    [FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)] set;
        //}

        //extern public GradientAlphaKey[] alphaKeys
        //{
        //    [FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)] get;
        //    [FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)] set;
        //}


        //extern public GradientMode mode { get; set; }
        //public extern ColorSpace colorSpace { get; set; }

        //extern internal Color constantColor { get; set; }

        //// Setup Gradient with an array of color keys and alpha keys
        //[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
        //extern public void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

        //public override bool Equals(object o)
        //{
        //    if (ReferenceEquals(null, o))
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, o))
        //    {
        //        return true;
        //    }

        //    if (o.GetType() != this.GetType())
        //    {
        //        return false;
        //    }
        //    return Equals((Gradient)o);
        //}

        //public bool Equals(Gradient other)
        //{
        //    if (ReferenceEquals(null, other))
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, other))
        //    {
        //        return true;
        //    }

        //    if (m_Ptr.Equals(other.m_Ptr))
        //    {
        //        return true;
        //    }

        //    return Internal_Equals(other.m_Ptr);
        //}

        //public override int GetHashCode()
        //{
        //    return m_Ptr.GetHashCode();
        //}
    }
} // end of UnityEngine
