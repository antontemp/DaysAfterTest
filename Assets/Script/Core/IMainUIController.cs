using System;
using Script.Popup;

namespace Script.Core
{
    public interface IMainUIController
    {
        event Action<int> SaveGame;
        event Action<int> LoadGame;
        event Action ReLoadGame;
        event Action ClosedPopup;
        void OpenPopup(PopupType type);
        void CloseAllPopup();
    }
}