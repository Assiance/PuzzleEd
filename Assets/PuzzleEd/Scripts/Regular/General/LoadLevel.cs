using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Managers;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.General
{
    public class LoadLevel : ESMonoBehaviour
    {
        public Transform SoundController;
        public Transform MusicController;

        void Awake()
        {
            var sceneManager = FindObjectOfType<SceneManager>();

            if (sceneManager == null)
                InitiateSceneManager();

            var soundController = FindObjectOfType<PuzzleSoundController>();

            if (soundController == null)
                InitiateSoundController();

            var musicController = FindObjectOfType<MusicController>();

            if(musicController == null)
                InitiateMusicController();

            var gameController = FindObjectOfType<GameController>();

            if (gameController == null)
                InitiateGameController();

        }

        private void InitiateSceneManager()
        {
            var go = new GameObject("SceneManager");
            go.AddComponent<SceneManager>();   
        }

        private void InitiateSoundController()
        {
            Instantiate(SoundController);
        }

        private void InitiateMusicController()
        {
            Instantiate(MusicController);
        }

        private void InitiateGameController()
        {
            var go = new GameObject("GameController");
            go.AddComponent<GameController>();
        }

    }
}
