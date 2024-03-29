﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.Enums;
namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class LetterDrag : Drag
    {
        private LetterPiece letterPiece;

        void Awake()
        {
            letterPiece = GetComponent<LetterPiece>();
            letterPiece.Character = DragId;
        }

        protected override void DragStart()
        {
            base.DragStart();

            PuzzleSoundController.PlayLetterSound(letterPiece.Character.ToUpper(), !GameController.Instance.IsSpanish);
        }

        protected override void Dragging(Vector2 newPosition)
        {
            base.Dragging(newPosition);

            if (letterPiece.ParticleTrail != null && letterPiece.ParticleTrail.isPlaying == false)
                letterPiece.ParticleTrail.Play();
        }

        protected override void DragStop()
        {
            base.DragStop();

            if (letterPiece.ParticleTrail != null && letterPiece.ParticleTrail.isPlaying)
                letterPiece.ParticleTrail.Stop();
        }
    }
}
