using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces;
using Assets.PuzzleEd.Scripts.Regular.General;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drop : ESMonoBehaviour, IDroppable
    {
        public string DropId { get; set; }

        public Action OnDrop { get; private set; }
        public Action OnOver { get; private set; }
        public Action OnOut { get; private set; }
    }
}
