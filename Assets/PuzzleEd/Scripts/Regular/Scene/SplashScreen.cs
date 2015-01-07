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
            StartCoroutine(LoadMainMenu());
        }
         
        private IEnumerator LoadMainMenu()
        {
            yield return new WaitForSeconds(2f);
            Application.LoadLevel("MainMenuScene");
        }
    }
}
