using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    internal class ApplicationWorker
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    MEMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private List<Component> m_Components = new List<Component>();
        private Queue<Component> m_Starts = new Queue<Component>();


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    LIFECYCLE
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        internal void UpdateFrame()
        {
            StartAll();
            UpdateAll();

            StartAll();
            LateUpdateAll();
        }

        private void StartAll()
        {
            while (m_Starts.Count > 0)
            {
                Component comp = m_Starts.Dequeue();

                // Check Enabled
                if (!comp.enabled)
                {
                    continue;
                }

                // Call Start
                try
                {
                    comp.Start();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        private void UpdateAll()
        {
            foreach (Component comp in m_Components)
            {
                try
                {
                    comp.Update();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        private void LateUpdateAll()
        {
            foreach (Component comp in m_Components)
            {
                try
                {
                    comp.LateUpdate();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    CONTAINER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        internal void AddWork(Component component)
        {
            // Add Entry
            m_Components.Add(component);

            // Call OnEnable
            try
            {
                component.OnEnable();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // Add Start Queue
            if (!component.m_hasStarted)
            {
                m_Starts.Enqueue(component);
            }
        }

        internal void RemoveWork(Component component)
        {
            // Call OnDisabled
            try
            {
                component.OnDisabled();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // Remove Entry
            m_Components.Remove(component);
        }
    }
}