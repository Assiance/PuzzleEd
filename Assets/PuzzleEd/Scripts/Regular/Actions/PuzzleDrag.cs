using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.Enums;
namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class PuzzleDrag : Drag
    {
        protected override void DragStart()
        {
            base.DragStart();

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnDragStop, Vector3.zero);
        }

        protected override void Dragging(Vector2 newPosition)
        {
            base.Dragging(newPosition);
        }

        protected override void DragStop()
        {
            base.DragStop();
        }
    }
}
