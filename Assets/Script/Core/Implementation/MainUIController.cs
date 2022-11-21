using System;
using Script.Popup;
using UnityEngine;

namespace Script.Core.Implementation
{
    public class MainUIController : MonoBehaviour, IMainUIController
    {
        [SerializeField] private GameObject welcomePage;
        [SerializeField] private GameObject winPage;
        [SerializeField] private GameObject gameOverPage;
        [SerializeField] private GameObject pausePage;
        [SerializeField] private GameObject savePage;
        [SerializeField] private GameObject loadPage;
        [SerializeField] private GameObject errorPage;
        [SerializeField] private GameObject buttons;
        public event Action<int> SaveGame;
        public event Action<int> LoadGame;
        public event Action ReLoadGame;
        public event Action ClosedPopup;

        void Start()
        {
            OpenPopup(PopupType.Welcome);
        }

        public void OpenPopup(PopupType type)
        {
            CloseAllPopup();
            Time.timeScale = 0;
            gameObject.SetActive(true);
            ShowPopup(type);
        }

        public void Save(int slotId)
        {
            SaveGame?.Invoke(slotId);
        }

        public void Load(int slotId)
        {
            LoadGame?.Invoke(slotId);
        }
    
        public void ReLoad()
        {
            ReLoadGame?.Invoke();
        }

        public void ClosePopup()
        {
            
            CloseAllPopup();
            gameObject.SetActive(false);
            ClosedPopup?.Invoke();
        }

        private void ShowPopup(PopupType type)
        {
            switch (type)
            {
                case PopupType.Welcome:
                    welcomePage.SetActive(true);
                    ShowButton();
                    break;
                case PopupType.Win:
                    winPage.SetActive(true);
                    ShowButton();
                    break;
                case PopupType.GameOver:
                    gameOverPage.SetActive(true);
                    ShowButton();
                    break;
                case PopupType.Pause:
                    pausePage.SetActive(true);
                    ShowButton();
                    break;
                case PopupType.Save:
                    savePage.SetActive(true);
                    HideButton();
                    break;
                case PopupType.Load:
                    loadPage.SetActive(true);
                    HideButton();
                    break;
                case PopupType.Error:
                    errorPage.SetActive(true);
                    HideButton();
                    break;
            }
        }

        private void ShowButton()
        {
            buttons.SetActive(true);
        }
        
        private void HideButton()
        {
            buttons.SetActive(false);
        }

        public void CloseAllPopup()
        {
            Time.timeScale = 1;
            welcomePage.SetActive(false);
            winPage.SetActive(false);
            gameOverPage.SetActive(false);
            pausePage.SetActive(false);
            loadPage.SetActive(false);
            savePage.SetActive(false);
            errorPage.SetActive(false);
        }
    }
}