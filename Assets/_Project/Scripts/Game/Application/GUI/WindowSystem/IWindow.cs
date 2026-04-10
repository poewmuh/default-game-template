using System;
using UnityEngine;

namespace Game.Application.GUI.WindowSystem
{
    public interface IWindow
    {
        event Action<WindowBase> OnClosed;
        
        void Initialize(IWindowSystem windowSystem);
        void Show();
        void Close();
        void SetHidden(bool hidden);
        GameObject GetDefaultSelectable();
    }
}