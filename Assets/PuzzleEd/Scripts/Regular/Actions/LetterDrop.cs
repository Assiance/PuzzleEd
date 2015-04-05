using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Entities;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class LetterDrop : Drop
    {
        public bool IsEnglish = true;

        protected override void SuccessDrop(Drag dragComponent)
        {
            base.SuccessDrop(dragComponent);

            var piece = dragComponent.GetComponent<Piece>();
            piece.IsPlaced = true;
        }

        protected override void FailDrop(Drag dragComponent)
        {
            base.FailDrop(dragComponent);
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
