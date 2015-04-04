using System;
using Assets.PuzzleEd.Scripts.Regular.Framework;
using Assets.PuzzleEd.Scripts.Regular.General;
using Assets.PuzzleEd.Scripts.Regular.Managers;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class GameController : BaseGameController
    {
        private SceneManager _sceneManager;
        private PuzzleManager _puzzleManager;
        public bool IsSpanish = false;
        public string GamePrefsName = "DefaultGame";

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
            IsSpanish = Convert.ToBoolean(PlayerPrefs.GetInt(GamePrefsName + "_Language"));
            _sceneManager = FindObjectOfType<SceneManager>();
            _sceneManager.LevelNames = new string[2] { "Level1", "Level2" };

            _puzzleManager = FindObjectOfType<PuzzleManager>();
        }

        void Start()
        {
            StartGame();
        }

        public override void StartGame()
        {
            base.StartGame();
            _puzzleManager.InitiatePuzzle();
        }

        public void PuzzleFinished()
        {
            Debug.Log("Puzzle Finished");
            _puzzleManager.PuzzleFinished(); 
        }

        public void LettersFinished()
        {
            Debug.Log("Letters Finished");
            _puzzleManager.LettersFinished();
        }

        public void LevelFinished()
        {
            Debug.Log("Level Finished");
            //call fade script
            float fadeTime = gameObject.GetComponent<FadeInOut>().BeginFade(-1);

             new WaitForSeconds(fadeTime);
            _sceneManager.GoToNextLevel();
        }
    }
}
