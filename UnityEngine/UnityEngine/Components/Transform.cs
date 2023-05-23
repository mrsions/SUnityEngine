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

        private Vector3 m_Position;
        private Quaternion m_Rotation;
        private Vector3 m_Scale;
        private Transform? m_Parent;
        private List<Transform> m_Children = new List<Transform>();

        //-- Position
        public Vector3 localPosition
        {
            get => m_Position;
            set => m_Position = value;
        }
        public Vector3 position
        {
            get => localToWorldMatrix.GetPosition();
            set => m_Position = m_Parent != null ? m_Parent.worldToLocalMatrix.MultiplyPoint(value) : value;
        }

        //-- Rotation
        public Vector3 localEulerAngles
        {
            get => localRotation.eulerAngles;
            set => localRotation = Quaternion.Euler(value);
        }
        public Quaternion localRotation
        {
            get => m_Rotation;
            set => m_Rotation = value;
        }
        public Vector3 eulerAngles
        {
            get => rotation.eulerAngles;
            set => rotation = Quaternion.Euler(value);
        }
        public Quaternion rotation
        {
            get
            {
                Quaternion result = m_Rotation;
                Transform? parent = m_Parent;
                while (parent != null)
                {
                    result = parent.m_Rotation * result;
                    parent = parent.m_Parent;
                }
                return result;
            }
            set
            {
                if (m_Parent != null)
                {
                    value = Quaternion.Inverse(m_Parent.rotation) * value;
                }
                m_Rotation = value;
            }
        }

        //-- Scale
        public Vector3 localScale
        {
            get => m_Scale;
            set => m_Scale = value;
        }
        public Vector3 lossyScale
        {
            get => localToWorldMatrix.lossyScale;
        }

        //-- Direction
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

        public Transform? parent
        {
            get => m_Parent;
            set => SetParent(m_Parent, false);
        }
        public Transform? root
        {
            get
            {
                Transform cursor = this;
                while (cursor.parent != null)
                {
                    cursor = cursor.parent;
                }
                return cursor;
            }
        }

        public Matrix4x4 localToWorldMatrix
        {
            get
            {
                Matrix4x4 local = Matrix4x4.TRS(localPosition, localRotation, localScale);
                if (parent != null)
                {
                    return parent.localToWorldMatrix * local;
                }
                else
                {
                    return local;
                }
            }
        }
        public Matrix4x4 worldToLocalMatrix
        {
            get => localToWorldMatrix.inverse;
        }

        public bool hasChanged { get; set; }

        public int childCount { get => m_Children.Count; }


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHOD
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public void SetParent(Transform? p, bool worldPositionStays = false)
        {
            if (worldPositionStays)
            {
                var matrix = localToWorldMatrix;
                SetParent(p, false);
                var matrix2 = localToWorldMatrix;
                
                position = matrix.GetPosition();
                rotation = matrix.rotation;
                
            }
            else
            {
                parent = p;
            }
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            if (transform.parent != null)
            {
                var matrix = transform.parent.worldToLocalMatrix;
                m_Position = matrix.MultiplyPoint(position);
                m_Rotation = matrix.rotation * rotation;
            }
            else
            {
                m_Position = position;
                m_Rotation = rotation;
            }
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

        public Transform? Find(string n)
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