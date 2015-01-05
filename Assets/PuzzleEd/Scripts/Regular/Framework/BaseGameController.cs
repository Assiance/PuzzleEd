using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Framework
{
    public class BaseGameController : ESMonoBehaviour
    {
        public virtual void StartGame() { }

        public virtual void RestartGameButtonPressed()
        {
            Application.LoadLevel(Application.loadedLevelName);
        }

        public bool IsPaused
        {
            get { return IsPaused; }
            set
            {
                IsPaused = value;

                Time.timeScale = IsPaused ? 0f : 1f;
            }
        }
    }
}
