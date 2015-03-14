using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine.UI;

namespace Assets.PuzzleEd.Scripts.Regular.Scene
{
    public class MainMenuScreen : ESMonoBehaviour
    {
        public string GamePrefsName = "DefaultGame";
        public Toggle IsSpanish;

        public void PlayGame()
        {
            PlayerPrefs.SetInt(GamePrefsName + "_Language", Convert.ToInt32(IsSpanish.isOn));
            Application.LoadLevel("Level1");
        }
    }
}
