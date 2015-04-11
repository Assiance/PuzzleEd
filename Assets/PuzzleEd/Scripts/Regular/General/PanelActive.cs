using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PuzzleEd.Scripts.Regular.General
{
    public class PanelActive: ESMonoBehaviour
    {

        public Image myPanel;



        public void PanelOff()
        {
            var camvasgroup=myPanel.GetComponent<CanvasGroup>();
            camvasgroup.alpha = 0;
            camvasgroup.interactable = false;

        }

        public void PaneOn()
        {
            var camvasgroup = myPanel.GetComponent<CanvasGroup>();
            camvasgroup.alpha = 1;
            camvasgroup.interactable = true;

        }
    }
}
