using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using Assets.PuzzleEd.Scripts.Regular.Enums;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class LetterDrop : Drop
    {
        public bool IsEnglish = true;
        public int LetterOrder;

        protected override void SuccessDrop(Drag dragComponent)
        {
            base.SuccessDrop(dragComponent);

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnPuzzleDropSuccess, Vector3.zero);

            var piece = dragComponent.GetComponent<Piece>();
            piece.IsPlaced = true;

            SendMessageUpwards("LetterDone", LetterOrder);
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

        public void ActivateLetter(int order)
        {
            if (LetterOrder == order + 1)
            {
                StartCoroutine(InitiateDrop());
            }
        }

        public IEnumerator InitiateDrop()
        {
            yield return new WaitForSeconds(0.5f);

            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
