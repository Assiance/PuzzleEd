using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Enums;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine.UI;

namespace Assets.PuzzleEd.Scripts.Regular.Scene
{
    public class MainMenuScreen : ESMonoBehaviour
    {
        public string GamePrefsName = "DefaultGame";
        public Toggle IsSpanish;
        //going make this static so I can keep same instance when the new object gets created
        //if we dont do this we actually have multiple instance of them.
        static bool MenuOn;
        static PanelActive  panelActive;

        public void PlayGame()
        {
            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnSelectUI, Vector3.zero);
            PlayerPrefs.SetInt(GamePrefsName + "_Language", Convert.ToInt32(IsSpanish.isOn));
            StartCoroutine("LoadLevel");
        }

        public IEnumerator LoadLevel()
        {
            yield return new WaitForSeconds(1.4f);
            Application.LoadLevel("Level1");
        }

        public void MainMenu()
        {
            Destroy(GameController.Instance);

            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnSelectUI, Vector3.zero);
            Application.LoadLevel("MainMenuScene");
        }

        private void ToggleMenu(PanelActive menuactive)
        {
            // display menu
            if (!MenuOn)
            {
                panelActive.PaneOn();
                MenuOn = true;
            }
            else
            {
                panelActive.PanelOff();
                MenuOn = false;
            }
        }

        public void Goback()
        {
            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnSelectUI, Vector3.zero);
            var canvasgroup = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<CanvasGroup>();
            canvasgroup.alpha = 0;
            canvasgroup.blocksRaycasts = false;
            //set the main menu display
            var Menupanel = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.GetComponent<CanvasGroup>();
            Menupanel.interactable = true;
        }

        public void Showcredits()
        {
            var creditpanel = gameObject.transform.parent.gameObject.transform.Find("creditsPanel");
            //disable Menupanel 
            gameObject.transform.parent.gameObject.GetComponent<CanvasGroup>().interactable=false;
            //set the creditpanel to display and then set blockraycast to true
            creditpanel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            creditpanel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            
        }

        public void OpenUrl()
        {
            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnSelectUI, Vector3.zero);
            Application.OpenURL("http://everfluxstudios.com/");
        }

        public void DisplayMenu()
        {
            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnSelectUI, Vector3.zero);

            if(panelActive==null)
            {
                panelActive = gameObject.transform.parent.gameObject.GetComponentInChildren<PanelActive>();
                ToggleMenu(panelActive);       
            }
            else
            {
                ToggleMenu(panelActive); 
            }
        }
    }
}
