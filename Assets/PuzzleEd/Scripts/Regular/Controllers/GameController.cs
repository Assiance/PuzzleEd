using Assets.PuzzleEd.Scripts.Regular.Framework;
using Assets.PuzzleEd.Scripts.Regular.Managers;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class GameController : BaseGameController
    {
        private SceneManager _sceneManager;

        #region Singleton
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
                    if (_instance == null)
                        _instance = new GameObject("GameController Temporary Instance", typeof(GameController)).GetComponent<GameController>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        #endregion

        void OnEnable()
        {
            _sceneManager = FindObjectOfType<SceneManager>();
            _sceneManager.LevelNames = new string[4] {"MainMenuScene", "TestScene", "BradScene", "MarlonScene"};
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                _sceneManager.LoadLevel(_sceneManager.LevelNames[0]);

            if (Input.GetKeyDown(KeyCode.Space))
                _sceneManager.GoToNextLevel();
        }
    }
}
