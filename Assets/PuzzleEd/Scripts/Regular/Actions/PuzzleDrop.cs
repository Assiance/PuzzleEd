using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using Assets.PuzzleEd.Scripts.Regular.Enums;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class PuzzleDrop : Drop
    {
        protected override void SuccessDrop(Drag dragComponent)
        {
            base.SuccessDrop(dragComponent);

            var piece = dragComponent.GetComponent<Piece>();
            piece.IsPlaced = true;

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnPuzzleDropSuccess, Vector3.zero);
        }

        protected override void FailDrop(Drag dragComponent)
        {
            base.FailDrop(dragComponent);

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnDropFailure, Vector3.zero);
        }

        protected override void DropHover(Drag dragComponent)
        {
            base.DropHover(dragComponent);
        }

        protected override void DropOut(Drag dragComponent)
        {
            base.DropOut(dragComponent);

            var piece = dragComponent.GetComponent<Piece>();
            piece.IsPlaced = false;
        }
    }
}
