﻿using System;
using System.Collections.Generic;
using Statman.Engines;

namespace Statman
{
    class EngineManager : IDisposable
    {
        private readonly Dictionary<string, IEngine> m_Engines; 

        public EngineManager()
        {
            m_Engines = new Dictionary<string, IEngine>();
            MainApp.Loop.Update += Update;
        }

        public void RegisterEngine(IEngine p_Engine)
        {
            m_Engines.Add(p_Engine.Name, p_Engine);
        }

        private void Update(object p_Sender, EventArgs p_EventArgs)
        {
            var s_ActiveEngines = false;

            foreach (var s_Engine in m_Engines)
            {
                s_Engine.Value.Update();

                if (!s_Engine.Value.Active) 
                    continue;

                // Skip updating other engines when an engine is active.
                s_ActiveEngines = true;
                break;
            }

            // Remove any and all engine controls.
            if (!s_ActiveEngines)
                MainApp.MainWindow.ResetEngineControls();
        }

        public void OnMessage(string p_Message)
        {
            var s_Parts = p_Message.Split('|');

            if (s_Parts.Length != 3)
                return;

            IEngine s_Engine;
            if (!m_Engines.TryGetValue(s_Parts[0], out s_Engine))
                return;

            s_Engine.OnMessage(s_Parts[1], s_Parts[2]);
        }

        public void Dispose()
        {
            foreach (var s_Engine in m_Engines)
                s_Engine.Value.Dispose();

            m_Engines.Clear();
        }
    }
}