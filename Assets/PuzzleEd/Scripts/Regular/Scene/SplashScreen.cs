using System.Collections;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Scene
{
    public class SplashScreen : ESMonoBehaviour
    {
        private void Start()
        {
            Invoke("LoadMainMenu", 2f);
        }

        private void LoadMainMenu()
        {
            GameController.Instance.StartGame();
            Debug.Log("Start Game");
        }
    }
}
