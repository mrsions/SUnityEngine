﻿using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public class Camera : Component
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHOD
        //
        ///////////////////////////////////////////////////////////////////////////////////////
        public Matrix4x4 worldToCameraMatrix { get; set; }
        public Matrix4x4 projectionMatrix { get; set; }
    }
}