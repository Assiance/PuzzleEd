using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.PuzzleEd.Scripts.Regular.Managers
{
    public class AdManager : ESMonoBehaviour
    {
        [SerializeField]
        private string _gameID = "36688";

        void Awake()
        {
            Advertisement.Initialize(_gameID, true);
        }

        public void ShowAd(string zone = "", Action<ShowResult> adCallback = null)
        {
#if UNITY_EDITOR
            StartCoroutine(WaitForAd());
#endif

            if (string.Equals(zone, string.Empty))
                zone = null;

            ShowOptions options = new ShowOptions();
            options.resultCallback = adCallback;

            if (Advertisement.isReady(zone))
                Advertisement.Show(zone, options);
        }

        //void AdCallbackhandler(ShowResult result)
        //{
        //    switch (result)
        //    {
        //        case ShowResult.Finished:
        //            Debug.Log("Ad Finished. Rewarding player...");
        //            break;
        //        case ShowResult.Skipped:
        //            Debug.Log("Ad skipped. Son, I am dissapointed in you");
        //            break;
        //        case ShowResult.Failed:
        //            Debug.Log("I swear this has never happened to me before");
        //            break;
        //    }
        //}

        IEnumerator WaitForAd()
        {
            float currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            yield return null;

            while (Advertisement.isShowing)
                yield return null;

            Time.timeScale = currentTimeScale;
        }
    }
}
