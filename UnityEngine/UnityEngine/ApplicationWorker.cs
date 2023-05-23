using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    internal class ApplicationWorker
    {
        private List<Component> m_Components = new List<Component>();
        private List<Component> m_StartComponents = new List<Component>();

        internal void AddWork(Component component)
        {
            if (m_Components.Contains(component))
            {
                return;
            }

            if (!component.m_hasAwake)
            {
                try
                {
                    component.m_hasAwake = true;
                    component.Awake();
                }
                catch
                {
                    component.m_Enabled = false;
                    component.OnDisabled();
                    throw;
                }
            }

            if (!component.m_hasEnabled)
            {
                component.OnEnabled();
                component.m_hasEnabled = true;
            }

            if (!component.m_hasStarted)
            {
                m_StartComponents.Add(component);
            }

            m_Components.Add(component);
        }

        internal void RemoveWork(Component component)
        {
            if (!m_Components.Remove(component)) return;

            if (!component.m_hasStarted)
            {
                m_StartComponents.Remove(component);
            }

            if (component.m_hasEnabled)
            {
                component.m_hasEnabled = false;
                component.OnDisabled();
            }
        }

        internal void Run()
        {
            while (true)
            {
                UpdateFrame();
                Thread.Sleep(1);
            }
        }

        private void UpdateFrame()
        {
            Time.NextFrame();
            StartAll();
            UpdateAll();
            LateUpdateAll();
        }

        private void StartAll()
        {
            foreach (Component comp in m_StartComponents)
            {
                try
                {
                    comp.Start();
                    comp.m_hasStarted = true;
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
                    if (!comp.m_hasStarted)
                    {
                        comp.Start();
                        comp.m_hasStarted = true;
                    }

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
    }
}