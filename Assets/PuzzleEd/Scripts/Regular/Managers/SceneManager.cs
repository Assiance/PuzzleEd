using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Managers
{
    public class SceneManager : ESMonoBehaviour
    {
        public string[] LevelNames;
        public int GameLevelNum;

        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadLevel(string sceneName)
        {
            Application.LoadLevel(sceneName);
        }

        public void LoadLevel(int indexNum)
        {
            LoadLevel(LevelNames[indexNum]);
        }

        public void ResetGame()
        {
            GameLevelNum = 0;
        }

        public void GoToNextLevel()
        {
            if (GameLevelNum >= LevelNames.Length)
                GameLevelNum = 0;

            LoadLevel(GameLevelNum);

            GameLevelNum++;
        }

    }
}
