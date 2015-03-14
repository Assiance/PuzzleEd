using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using UnityEngine;
using Assets.PuzzleEd.Scripts.Regular.Enums;
namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class PuzzleDrag : Drag
    {
        private PuzzlePiece puzzlePiece;

        void Awake()
        {
            puzzlePiece = GetComponent<PuzzlePiece>();
        }

        protected override void DragStart()
        {
            base.DragStart();

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnDragStop, Vector3.zero);
        }

        protected override void Dragging(Vector2 newPosition)
        {
            base.Dragging(newPosition);

            if (puzzlePiece.ParticleTrail != null && puzzlePiece.ParticleTrail.isPlaying == false)
                puzzlePiece.ParticleTrail.Play();
        }

        protected override void DragStop()
        {
            base.DragStop();

            if (puzzlePiece.ParticleTrail != null && puzzlePiece.ParticleTrail.isPlaying)
                puzzlePiece.ParticleTrail.Stop();
        }
    }
}
