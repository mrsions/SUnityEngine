
namespace UnityEngine
{
    public class Object 
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    STATIC
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public static void Destroy(Object obj, float t = 0f)
        {
            obj.m_Destroyed = true;
        }

        public static void DestroyImmediate(Object obj)
        {
            obj.m_Destroyed = true;
        }

        public static T[] FindObjectsOfType<T>(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public static T FindObjectOfType<T>(bool includeInactive)
        {
            throw new NotImplementedException();
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private int m_InstanceId;

        private bool m_Destroyed;

        public string name { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    METHODS
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public Object()
        {
            m_InstanceId = Application.NewInstanceId();
        }

        public int GetInstanceID()
        {
            return m_InstanceId;
        }

        public override int GetHashCode()
        {
            return m_InstanceId;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override string ToString()
        {
            return name;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    OPERATOR
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public static implicit operator bool(Object exists)
        {
            return !exists.m_Destroyed;
        }

        public static bool operator ==(Object x, Object y)
        {
            return ReferenceEquals(x, y);
        }

        public static bool operator !=(Object x, Object y)
        {
            return !ReferenceEquals(x, y);
        }

    }
}