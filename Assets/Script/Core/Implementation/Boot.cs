using Script.Data;
using Script.Installer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Script.Core.Implementation
{
    public class Boot : MonoBehaviour, IBoot
    {
        public GameSettings Settings { get; private set; }
        public GameData GameData { get; private set; }
        public bool IsLoadByLevelId => GameData == null;

        [Inject]
        private void Construct(GameSettings settings)
        {
            Settings = settings;
        }

        public void LoadByGameData(GameData gameData)
        {
            GameData = gameData;
            SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
        }

        public void LoadByLevelId(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= Settings.LevelAmount)
            {
                levelIndex = 0;
            }

            GameData = null;
            Settings.StartLevelindex = levelIndex;
            SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
        }

        public void ReLoad()
        {
            LoadByLevelId(Settings.StartLevelindex);
        }
    }
}