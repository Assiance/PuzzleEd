using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PuzzleEd.Scripts.Regular.General
{
    public class RelayActivateLetterMessage : ESMonoBehaviour
    {
        public void LetterDone(int order)
        {
            print("Broadcasting");
            BroadcastMessage("ActivateLetter", order);
        }
    }
}
