// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct FrustumPlanes
    {
        public float left;
        public float right;
        public float bottom;
        public float top;
        public float zNear;
        public float zFar;
    }

    public partial struct Matrix4x4
    {
        private Quaternion GetRotation()
        {
            Matrix4x4 matrix = NormalizeMatrix(this);

            float w = Mathf.Sqrt(1.0f + matrix.m00 + matrix.m11 + matrix.m22) / 2.0f;
            float x = (matrix.m21 - matrix.m12) / (4.0f * w);
            float y = (matrix.m02 - matrix.m20) / (4.0f * w);
            float z = (matrix.m10 - matrix.m01) / (4.0f * w);

            return new Quaternion(x, y, z, w);

            static Matrix4x4 NormalizeMatrix(Matrix4x4 matrix)
            {
                Vector3 xAxis = new Vector3(matrix.m00, matrix.m01, matrix.m02).normalized;
                Vector3 yAxis = new Vector3(matrix.m10, matrix.m11, matrix.m12).normalized;
                Vector3 zAxis = new Vector3(matrix.m20, matrix.m21, matrix.m22).normalized;

                matrix.m00 = xAxis.x;
                matrix.m01 = xAxis.y;
                matrix.m02 = xAxis.z;

                matrix.m10 = yAxis.x;
                matrix.m11 = yAxis.y;
                matrix.m12 = yAxis.z;

                matrix.m20 = zAxis.x;
                matrix.m21 = zAxis.y;
                matrix.m22 = zAxis.z;

                return matrix;
            }
        }
        private Vector3 GetLossyScale()
        {
            return new Vector3(
                new Vector3(m00, m01, m02).magnitude,
                new Vector3(m10, m11, m12).magnitude,
                new Vector3(m20, m21, m22).magnitude);
        }
        private bool IsIdentity()
        {
            return this == identityMatrix;
        }
        private float GetDeterminant()
        {

            // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
            // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
            // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
            // | m n o p |
            //
            //   | f g h |
            // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //   | n o p |
            //
            //   | e g h |     
            // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //   | m o p |     
            //
            //   | e f h |
            // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //   | m n p |
            //
            //   | e f g |
            // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //   | m n o |
            //
            // Cost of operation
            // 17 adds and 28 muls.
            //
            // add: 6 + 8 + 3 = 17
            // mul: 12 + 16 = 28

            float a = m00, b = m01, c = m02, d = m03;
            float e = m10, f = m11, g = m12, h = m13;
            float i = m20, j = m21, k = m22, l = m23;
            float m = m30, n = m31, o = m32, p = m33;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                   b * (e * kp_lo - g * ip_lm + h * io_km) +
                   c * (e * jp_ln - f * ip_lm + h * in_jm) -
                   d * (e * jo_kn - f * io_km + g * in_jm);
        }

        [Obsolete("Not support", true)]
        private FrustumPlanes DecomposeProjection() => default;

        public Quaternion rotation { get { return GetRotation(); } }
        public Vector3 lossyScale { get { return GetLossyScale(); } }
        public bool isIdentity { get { return IsIdentity(); } }
        public float determinant { get { return GetDeterminant(); } }
        public Matrix4x4 inverse { get { return Matrix4x4.Inverse(this); } }
        public Matrix4x4 transpose { get { return Matrix4x4.Transpose(this); } }

        public bool ValidTRS()
        {
            // One way to check if a Matrix is a valid TRS, is to check if the axis are orthogonal and their determinant is 1
            Vector3 x = new Vector3(m00, m01, m02);
            Vector3 y = new Vector3(m10, m11, m12);
            Vector3 z = new Vector3(m20, m21, m22);

            return Mathf.Approximately(Vector3.Dot(x, y), 0f)
                && Mathf.Approximately(Vector3.Dot(x, z), 0f)
                && Mathf.Approximately(Vector3.Dot(y, z), 0f)
                && Mathf.Approximately(GetDeterminant(), 1f);
        }
        public void SetTRS(Vector3 pos, Quaternion q, Vector3 s) { this = Matrix4x4.TRS(pos, q, s); }

        public static float Determinant(Matrix4x4 m) { return m.determinant; }
        public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            Matrix4x4 m = new Matrix4x4();

            // Apply rotation (from quaternion) to matrix
            m.m00 = 1 - 2 * q.y * q.y - 2 * q.z * q.z;
            m.m01 = 2 * q.x * q.y - 2 * q.z * q.w;
            m.m02 = 2 * q.x * q.z + 2 * q.y * q.w;
            m.m10 = 2 * q.x * q.y + 2 * q.z * q.w;
            m.m11 = 1 - 2 * q.x * q.x - 2 * q.z * q.z;
            m.m12 = 2 * q.y * q.z - 2 * q.x * q.w;
            m.m20 = 2 * q.x * q.z - 2 * q.y * q.w;
            m.m21 = 2 * q.y * q.z + 2 * q.x * q.w;
            m.m22 = 1 - 2 * q.x * q.x - 2 * q.y * q.y;

            // Apply translation to matrix
            m.m03 = pos.x;
            m.m13 = pos.y;
            m.m23 = pos.z;

            // Apply scale to matrix
            m.m00 *= s.x;
            m.m11 *= s.y;
            m.m22 *= s.z;

            // Set final row of matrix
            m.m30 = 0.0f;
            m.m31 = 0.0f;
            m.m32 = 0.0f;
            m.m33 = 1.0f;

            return m;
        }
        public static Matrix4x4 Inverse(Matrix4x4 matrix)
        {
            //                                       -1
            // If you have matrix M, inverse Matrix M   can compute
            //
            //     -1       1      
            //    M   = --------- A
            //            det(M)
            //
            // A is adjugate (adjoint) of M, where,
            //
            //      T
            // A = C
            //
            // C is Cofactor matrix of M, where,
            //           i + j
            // C   = (-1)      * det(M  )
            //  ij                    ij
            //
            //     [ a b c d ]
            // M = [ e f g h ]
            //     [ i j k l ]
            //     [ m n o p ]
            //
            // First Row
            //           2 | f g h |
            // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //  11         | n o p |
            //
            //           3 | e g h |
            // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //  12         | m o p |
            //
            //           4 | e f h |
            // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //  13         | m n p |
            //
            //           5 | e f g |
            // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //  14         | m n o |
            //
            // Second Row
            //           3 | b c d |
            // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
            //  21         | n o p |
            //
            //           4 | a c d |
            // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
            //  22         | m o p |
            //
            //           5 | a b d |
            // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
            //  23         | m n p |
            //
            //           6 | a b c |
            // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
            //  24         | m n o |
            //
            // Third Row
            //           4 | b c d |
            // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
            //  31         | n o p |
            //
            //           5 | a c d |
            // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
            //  32         | m o p |
            //
            //           6 | a b d |
            // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
            //  33         | m n p |
            //
            //           7 | a b c |
            // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
            //  34         | m n o |
            //
            // Fourth Row
            //           5 | b c d |
            // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
            //  41         | j k l |
            //
            //           6 | a c d |
            // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
            //  42         | i k l |
            //
            //           7 | a b d |
            // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
            //  43         | i j l |
            //
            //           8 | a b c |
            // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
            //  44         | i j k |
            //
            // Cost of operation
            // 53 adds, 104 muls, and 1 div.
            float a = matrix.m00, b = matrix.m01, c = matrix.m02, d = matrix.m03;
            float e = matrix.m10, f = matrix.m11, g = matrix.m12, h = matrix.m13;
            float i = matrix.m20, j = matrix.m21, k = matrix.m22, l = matrix.m23;
            float m = matrix.m30, n = matrix.m31, o = matrix.m32, p = matrix.m33;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            float a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            float a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            float a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            float a14 = -(e * jo_kn - f * io_km + g * in_jm);

            float det = a * a11 + b * a12 + c * a13 + d * a14;

            Matrix4x4 result;
            if (Math.Abs(det) < float.Epsilon)
            {
                result = new Matrix4x4();
                result.m00 = float.NaN;
                result.m01 = float.NaN;
                result.m02 = float.NaN;
                result.m03 = float.NaN;
                result.m10 = float.NaN;
                result.m11 = float.NaN;
                result.m12 = float.NaN;
                result.m13 = float.NaN;
                result.m20 = float.NaN;
                result.m21 = float.NaN;
                result.m22 = float.NaN;
                result.m23 = float.NaN;
                result.m30 = float.NaN;
                result.m31 = float.NaN;
                result.m32 = float.NaN;
                result.m33 = float.NaN;
            }
            else
            {
                float invDet = 1.0f / det;

                result.m00 = RoundEpsilon(a11 * invDet);
                result.m10 = RoundEpsilon(a12 * invDet);
                result.m20 = RoundEpsilon(a13 * invDet);
                result.m30 = RoundEpsilon(a14 * invDet);

                result.m01 = RoundEpsilon(-(b * kp_lo - c * jp_ln + d * jo_kn) * invDet);
                result.m11 = RoundEpsilon(+(a * kp_lo - c * ip_lm + d * io_km) * invDet);
                result.m21 = RoundEpsilon(-(a * jp_ln - b * ip_lm + d * in_jm) * invDet);
                result.m31 = RoundEpsilon(+(a * jo_kn - b * io_km + c * in_jm) * invDet);

                float gp_ho = g * p - h * o;
                float fp_hn = f * p - h * n;
                float fo_gn = f * o - g * n;
                float ep_hm = e * p - h * m;
                float eo_gm = e * o - g * m;
                float en_fm = e * n - f * m;

                result.m02 = RoundEpsilon(+(b * gp_ho - c * fp_hn + d * fo_gn) * invDet);
                result.m12 = RoundEpsilon(-(a * gp_ho - c * ep_hm + d * eo_gm) * invDet);
                result.m22 = RoundEpsilon(+(a * fp_hn - b * ep_hm + d * en_fm) * invDet);
                result.m32 = RoundEpsilon(-(a * fo_gn - b * eo_gm + c * en_fm) * invDet);

                float gl_hk = g * l - h * k;
                float fl_hj = f * l - h * j;
                float fk_gj = f * k - g * j;
                float el_hi = e * l - h * i;
                float ek_gi = e * k - g * i;
                float ej_fi = e * j - f * i;

                result.m03 = RoundEpsilon(-(b * gl_hk - c * fl_hj + d * fk_gj) * invDet);
                result.m13 = RoundEpsilon(+(a * gl_hk - c * el_hi + d * ek_gi) * invDet);
                result.m23 = RoundEpsilon(-(a * fl_hj - b * el_hi + d * ej_fi) * invDet);
                result.m33 = RoundEpsilon(+(a * fk_gj - b * ek_gi + c * ej_fi) * invDet);
            }
            return result;

            [MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
            static float RoundEpsilon(float v)
            {
                const float Positive = float.Epsilon;
                const float Negative = -float.Epsilon;
                return Negative <= v && v <= Positive ? 0 : v;
            }
        }

        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            Matrix4x4 result;

            result.m00 = m.m00;
            result.m01 = m.m10;
            result.m02 = m.m20;
            result.m03 = m.m30;
            result.m10 = m.m01;
            result.m11 = m.m11;
            result.m12 = m.m21;
            result.m13 = m.m31;
            result.m20 = m.m02;
            result.m21 = m.m12;
            result.m22 = m.m22;
            result.m23 = m.m32;
            result.m30 = m.m03;
            result.m31 = m.m13;
            result.m32 = m.m23;
            result.m33 = m.m33;

            return result;
        }

        [Obsolete("Not test method. please check unity3d original source code", true)]
        public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            // The orthographic projection matrix maps a rectangular prism 
            // (defined by the left, right, bottom, top, zNear, zFar parameters)
            // into normalized device coordinates (-1 to 1 in each dimension).
            // Here is a basic version of this matrix.

            Matrix4x4 m = new Matrix4x4();

            m.m00 = 2.0f / (right - left);
            m.m11 = 2.0f / (top - bottom);
            m.m22 = -2.0f / (zFar - zNear);

            m.m03 = -(right + left) / (right - left);
            m.m13 = -(top + bottom) / (top - bottom);
            m.m23 = -(zFar + zNear) / (zFar - zNear);

            m.m33 = 1.0f;

            return m;
        }
        [Obsolete("Not test method. please check unity3d original source code", true)]
        public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
        {
            // The perspective projection matrix maps a frustum (defined by the field of view (fov), 
            // aspect ratio (aspect), and zNear, zFar planes) to normalized device coordinates.
            // Here is a basic version of this matrix.

            Matrix4x4 m = new Matrix4x4();

            float tanHalfFov = Mathf.Tan(0.5f * fov);

            m.m00 = 1.0f / (aspect * tanHalfFov);
            m.m11 = 1.0f / tanHalfFov;
            m.m22 = -(zFar + zNear) / (zFar - zNear);
            m.m23 = -1.0f;
            m.m32 = -(2.0f * zFar * zNear) / (zFar - zNear);

            return m;
        }
        [Obsolete("Not test method. please check unity3d original source code", true)]
        public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
        {
            // This function creates a transformation matrix that moves the world origin to 'from', 
            // and rotates the world so 'up' is up and 'to' is in front of 'from'.

            Vector3 zAxis = Vector3.Normalize(from - to);
            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(up, zAxis));
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

            Matrix4x4 m = new Matrix4x4();

            m.m00 = xAxis.x; m.m01 = yAxis.x; m.m02 = zAxis.x; m.m03 = from.x;
            m.m10 = xAxis.y; m.m11 = yAxis.y; m.m12 = zAxis.y; m.m13 = from.y;
            m.m20 = xAxis.z; m.m21 = yAxis.z; m.m22 = zAxis.z; m.m23 = from.z;
            m.m30 = 0.0f; m.m31 = 0.0f; m.m32 = 0.0f; m.m33 = 1.0f;

            return m;
        }
        [Obsolete("Not test method. please check unity3d original source code", true)]
        public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            // The frustum projection matrix maps a frustum (defined by the left, right, bottom, 
            // top, zNear, zFar parameters) to normalized device coordinates. Here is a basic 
            // version of this matrix.

            Matrix4x4 m = new Matrix4x4();

            m.m00 = 2.0f * zNear / (right - left);
            m.m11 = 2.0f * zNear / (top - bottom);

            m.m02 = (right + left) / (right - left);
            m.m12 = (top + bottom) / (top - bottom);
            m.m22 = -(zFar + zNear) / (zFar - zNear);
            m.m23 = -1.0f;
            m.m32 = -(2.0f * zFar * zNear) / (zFar - zNear);

            return m;
        }
        [Obsolete("Not test method. please check unity3d original source code", true)]
        public static Matrix4x4 Frustum(FrustumPlanes fp)
        {
            return Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
        }
    }

    public partial struct Vector3
    {
        [Obsolete("Not support", true)]
        public static Vector3 Slerp(Vector3 a, Vector3 b, float t) => default;
        [Obsolete("Not support", true)]
        public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t) => default;

        [Obsolete("Not support", true)]
        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent) { OrthoNormalize2(ref normal, ref tangent); }
        [Obsolete("Not support", true)]
        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal) { OrthoNormalize3(ref normal, ref tangent, ref binormal); }
        private static void OrthoNormalize2(ref Vector3 a, ref Vector3 b) { }
        private static void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c) { }

        [Obsolete("Not support", true)]
        public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta) => default;
    }

    public partial struct Quaternion
    {
        public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            var from = LookRotation(fromDirection);
            var to = LookRotation(toDirection);
            return Inverse(from) * to;
        }

        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            return SlerpUnclamped(a, b, Mathf.Clamp01(t));
        }
        public static Quaternion SlerpUnclamped(Quaternion quaternion1, Quaternion quaternion2, float t)
        {
            const float epsilon = 1e-6f;

            float cosOmega = quaternion1.x * quaternion2.x + quaternion1.y * quaternion2.y +
                             quaternion1.z * quaternion2.z + quaternion1.w * quaternion2.w;

            bool flip = false;

            if (cosOmega < 0.0f)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            float s1, s2;

            if (cosOmega > (1.0f - epsilon))
            {
                // Too close, do straight linear interpolation.
                s1 = 1.0f - t;
                s2 = (flip) ? -t : t;
            }
            else
            {
                float omega = (float)Math.Acos(cosOmega);
                float invSinOmega = (float)(1 / Math.Sin(omega));

                s1 = (float)Math.Sin((1.0f - t) * omega) * invSinOmega;
                s2 = (flip)
                    ? (float)-Math.Sin(t * omega) * invSinOmega
                    : (float)Math.Sin(t * omega) * invSinOmega;
            }

            Quaternion ans;

            ans.x = s1 * quaternion1.x + s2 * quaternion2.x;
            ans.y = s1 * quaternion1.y + s2 * quaternion2.y;
            ans.z = s1 * quaternion1.z + s2 * quaternion2.z;
            ans.w = s1 * quaternion1.w + s2 * quaternion2.w;

            return ans;
        }
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            return LerpUnclamped(a, b, Mathf.Clamp01(t));
        }
        public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            float t1 = 1.0f - t;

            Quaternion r = new Quaternion();

            float dot = a.x * b.x + a.y * b.y +
                        a.z * b.z + a.w * b.w;

            if (dot >= 0.0f)
            {
                r.x = t1 * a.x + t * b.x;
                r.y = t1 * a.y + t * b.y;
                r.z = t1 * a.z + t * b.z;
                r.w = t1 * a.w + t * b.w;
            }
            else
            {
                r.x = t1 * a.x - t * b.x;
                r.y = t1 * a.y - t * b.y;
                r.z = t1 * a.z - t * b.z;
                r.w = t1 * a.w - t * b.w;
            }

            // Normalize it.
            float ls = r.x * r.x + r.y * r.y + r.z * r.z + r.w * r.w;
            float invNorm = 1.0f / Mathf.Sqrt(ls);

            r.x *= invNorm;
            r.y *= invNorm;
            r.z *= invNorm;
            r.w *= invNorm;

            return r;
        }

        public static Quaternion LookRotation(Vector3 forward) => LookRotation(forward, Vector3.up);
        public static Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            Vector3 u = Vector3.Normalize(Vector3.Cross(up, forward));
            Vector3 v = Vector3.Cross(forward, u);

            return NewQuaternionV33(u, v, forward);
        }

        public static Quaternion Inverse(Quaternion rotation)
        {
            float lengthSq = GetLengthSquared(ref rotation);
            if (lengthSq != 0.0)
            {
                float i = 1.0f / lengthSq;
                return new Quaternion(rotation.x * -i, rotation.y * -i, rotation.z * -i, rotation.w * i);
            }
            return rotation;
        }

        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion result;

            float halfAngle = Mathf.Deg2Rad * angle * 0.5f;
            float s = (float)Math.Sin(halfAngle);
            float c = (float)Math.Cos(halfAngle);

            result.x = Mathf.Deg2Rad * axis.x * s;
            result.y = Mathf.Deg2Rad * axis.y * s;
            result.z = Mathf.Deg2Rad * axis.z * s;
            result.w = c;
            return result;
        }

        private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
        {
            float c = Mathf.Acos(q.w);
            float s = 1f / Mathf.Sin(c);
            angle = Mathf.Rad2Deg * c * 2;

            axis = new Vector3(q.x * s
                , q.y * s
                , q.z * s);
        }

        private static Quaternion Internal_FromEulerRad(Vector3 vector3)
        {
            Vector3 s = new Vector3((float)Math.Sin(vector3.x * 0.5f),
                                    (float)Math.Sin(vector3.y * 0.5f),
                                    (float)Math.Sin(vector3.z * 0.5f));

            Vector3 c = new Vector3((float)Math.Cos(vector3.x * 0.5f),
                                    (float)Math.Cos(vector3.y * 0.5f),
                                    (float)Math.Cos(vector3.z * 0.5f));

            // ZXY
            return new Quaternion(s.x * c.y * c.z + s.y * s.z * c.x,
                                  s.y * c.x * c.z - s.x * s.z * c.y,
                                  s.z * c.x * c.y - s.x * s.y * c.z,
                                  c.x * c.y * c.z + s.y * s.z * s.x);
        }

        private static Vector3 Internal_ToEulerRad(Quaternion quaternion)
        {
            float sqw = quaternion.w * quaternion.w;
            float sqx = quaternion.x * quaternion.x;
            float sqy = quaternion.y * quaternion.y;
            float sqz = quaternion.z * quaternion.z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = quaternion.x * quaternion.w - quaternion.y * quaternion.z;
            Vector3 v;

            if (test > 0.4999999f * unit)
            { // singularity at north pole
                v.y = (float)(2f * Math.Atan2(quaternion.y, quaternion.x));
                v.x = (float)(Math.PI / 2);
                v.z = 0;
                return v;
            }
            if (test < -0.4999999f * unit)
            { // singularity at south pole
                v.y = (float)(-2f * Math.Atan2(quaternion.y, quaternion.x));
                v.x = (float)(-Math.PI / 2);
                v.z = (float)0;
                return v;
            }
            Quaternion q = new Quaternion(quaternion.w, quaternion.z, quaternion.x, quaternion.y);
            v.y = (float)Math.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
            v.x = (float)Math.Asin(Clamping(2f * (q.x * q.z - q.w * q.y)));                             // Pitch
            v.z = (float)Math.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
            return v;

            float Clamping(float value)
            {
                for (; ; )
                {
                    if (value < -1)
                    {
                        value = -(value + 2);
                    }
                    else if (value > 1)
                    {
                        value = -(value - 2);
                    }
                    else
                    {
                        return value;
                    }
                }
            }
        }

        private static Quaternion NewQuaternionV33(Vector3 m10, Vector3 m20, Vector3 m30)
        {
            Quaternion r = default;

            float M11 = m10.x;
            float M12 = m10.y;
            float M13 = m10.z;
            float M21 = m20.x;
            float M22 = m20.y;
            float M23 = m20.z;
            float M31 = m30.x;
            float M32 = m30.y;
            float M33 = m30.z;

            float sqrt;
            float half;
            float scale = M11 + M22 + M33;

            if (scale > 0.0f)
            {
                sqrt = (float)Math.Sqrt(scale + 1.0f);
                r.w = sqrt * 0.5f;
                sqrt = 0.5f / sqrt;

                r.x = (M23 - M32) * sqrt;
                r.y = (M31 - M13) * sqrt;
                r.z = (M12 - M21) * sqrt;
            }
            else if ((M11 >= M22) && (M11 >= M33))
            {
                sqrt = (float)Math.Sqrt(1.0f + M11 - M22 - M33);
                half = 0.5f / sqrt;

                r.x = 0.5f * sqrt;
                r.y = (M12 + M21) * half;
                r.z = (M13 + M31) * half;
                r.w = (M23 - M32) * half;
            }
            else if (M22 > M33)
            {
                sqrt = (float)Math.Sqrt(1.0f + M22 - M11 - M33);
                half = 0.5f / sqrt;

                r.x = (M21 + M12) * half;
                r.y = 0.5f * sqrt;
                r.z = (M32 + M23) * half;
                r.w = (M31 - M13) * half;
            }
            else
            {
                sqrt = (float)Math.Sqrt(1.0f + M33 - M11 - M22);
                half = 0.5f / sqrt;

                r.x = (M31 + M13) * half;
                r.y = (M32 + M23) * half;
                r.z = 0.5f * sqrt;
                r.w = (M12 - M21) * half;
            }

            return r;
        }

        private static float GetLength(ref Quaternion q)
        {
            return (float)Math.Sqrt(GetLengthSquared(ref q));
        }
        private static float GetLengthSquared(ref Quaternion q)
        {
            return q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        }
    }

    public partial struct Bounds
    {
        [Obsolete("Not Support. please make extern code.", true)] public bool Contains(Vector3 point) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public float SqrDistance(Vector3 point) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] private static bool IntersectRayAABB(Ray ray, Bounds bounds, out float dist) { dist = default; return default; }
        [Obsolete("Not Support. please make extern code.", true)] public Vector3 ClosestPoint(Vector3 point) { return default; }
    }

    public partial struct Mathf
    {
        [Obsolete("Not Support. please make extern code.", true)] public static int ClosestPowerOfTwo(int value) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static bool IsPowerOfTwo(int value) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static int NextPowerOfTwo(int value) { return default; }

        [Obsolete("Not Support. please make extern code.", true)] public static float GammaToLinearSpace(float value) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static float LinearToGammaSpace(float value) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static Color CorrelatedColorTemperatureToRGB(float kelvin) { return default; }

        [Obsolete("Not Support. please make extern code.", true)] public static ushort FloatToHalf(float val) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static float HalfToFloat(ushort val) { return default; }

        [Obsolete("Not Support. please make extern code.", true)] public static float PerlinNoise(float x, float y) { return default; }
        [Obsolete("Not Support. please make extern code.", true)] public static float PerlinNoise1D(float x) { return default; }
    }
}
