using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.General;

namespace Assets.PuzzleEd.Scripts.Regular.Scene
{
    public class MainMenuScreen : ESMonoBehaviour
    {
        public void PlayGame()
        {
            Application.LoadLevel("PlayScene");
        }
    }
}
