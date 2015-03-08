using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.General
{
    public class FadeInOut : ESMonoBehaviour
    {
        public Texture2D fadeOutTexture;	
        public float fadeSpeed = 0.8f;		

        private int drawDepth = -1000;		
        private float alpha = 1.0f;			
        private int fadeDir = -1;			

        void OnGUI()
        {
      
            alpha += fadeDir * fadeSpeed * Time.deltaTime;

            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;																
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		// draw the texture to fit the entire screen area
        }

        // sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return (fadeSpeed);
        }

        // OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
        void OnLevelWasLoaded()
        {
            // alpha = 1;		// use this if the alpha is not set to 1 by default
            BeginFade(-1);		// call the fade in function
        }
    }
}
