using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    public class Transform : Component
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private List<Transform> m_Children;

        public Vector3 position { get; set; }
        public Vector3 localPosition { get; set; }

        public Vector3 eulerAngles
        {
            get => rotation.eulerAngles;
            set => rotation = Quaternion.Euler(value);
        }
        public Vector3 localEulerAngles
        {
            get => localRotation.eulerAngles;
            set => localRotation = Quaternion.Euler(value);
        }
        public Quaternion rotation { get; set; }
        public Quaternion localRotation { get; set; }

        public Vector3 localScale { get; set; }
        public Vector3 lossyScale { get; }

        public Vector3 right
        {
            get => rotation * Vector3.right;
            set => rotation = Quaternion.FromToRotation(Vector3.right, value);
        }
        public Vector3 up
        {
            get => rotation * Vector3.up;
            set => rotation = Quaternion.FromToRotation(Vector3.up, value);
        }
        public Vector3 forward
        {
            get => rotation * Vector3.forward;
            set => rotation = Quaternion.FromToRotation(Vector3.forward, value);
        }

        public Transform parent { get; set; }
        public Transform root { get; set; }

        public Matrix4x4 localToWorldMatrix => Matrix4x4.TRS(position, rotation, localScale);
        public Matrix4x4 worldToLocalMatrix => localToWorldMatrix.inverse;

        public bool hasChanged { get; set; }
        public int childCount { get => m_Children.Count; }


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHOD
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public void SetParent(Transform p, bool worldPositionStays = false)
        {
            if (worldPositionStays)
            {
                var oldPos = position;
                var oldRot = rotation;
                SetParent(p, false);
                position = oldPos;
                rotation = oldRot;
            }
            else
            {
                parent = p;
            }
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public void Translate(Vector3 translation, Space relationTo = Space.Self)
        {
            if (relationTo == Space.World)
            {
                position += translation;
            }
            else
            {
                position += TransformDirection(translation);
            }
        }
        public void Translate(Vector3 translation, Transform relationTo)
        {
            if (relationTo)
            {
                position += relationTo.TransformDirection(translation);
            }
            else
            {
                position += translation;
            }
        }

        public void Rotate(Vector3 eulers, Space relativeTo = Space.Self)
        {
            Quaternion quaternion = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
            if (relativeTo == Space.Self)
            {
                localRotation *= quaternion;
            }
            else
            {
                rotation *= Quaternion.Inverse(rotation) * quaternion * rotation;
            }
        }
        public void RotateAround(Vector3 point, Vector3 axis, float angle) { }

        public void LookAt(Transform target) { LookAt(target, Vector3.up); }
        public void LookAt(Transform target, Vector3 worldUp) { }

        public Vector3 TransformDirection(Vector3 direction)
        {
            return rotation * direction;
        }
        public Vector3 InverseTransformDirection(Vector3 direction)
        {
            return Quaternion.Inverse(rotation) * direction;
        }
        public Vector3 TransformVector(Vector3 vector)
        {
            return default;
        }
        public Vector3 InverseTransformVector(Vector3 vector)
        {
            return default;
        }
        public Vector3 TransformPoint(Vector3 point)
        {
            return default;
        }
        public Vector3 InverseTransformPoint(Vector3 point)
        {
            return default;
        }

        public Transform GetRoot() { return default; }
        public void DetachChildren() { }
        public void SetAsFirstSibling() { }
        public void SetAsLastSibling() { }
        public void SetSiblingIndex(int index) { }
        public int GetSiblingIndex() { return 0; }
        public bool IsChildOf(Transform parent)
        {
            return default;
        }
        public Transform GetChild(int index)
        {
            return m_Children[index];
        }

        public Transform Find(string n)
        {
            if (n == null)
            {
                throw new ArgumentNullException("Name cannot be null");
            }

            return default;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)m_Children).GetEnumerator();
        }
    }
}