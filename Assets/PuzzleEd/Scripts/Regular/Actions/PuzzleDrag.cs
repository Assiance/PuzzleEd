using System;
using System.Collections;
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
        private PuzzlePiece _puzzlePiece;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _puzzlePiece = GetComponent<PuzzlePiece>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void DragStart()
        {
            base.DragStart();

            if (_spriteRenderer != null)
                _spriteRenderer.sortingOrder = 5;

            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnPuzzleDragStart, Vector3.zero);
        }

        protected override void Dragging(Vector2 newPosition)
        {
            base.Dragging(newPosition);

            if (_puzzlePiece.ParticleTrail != null && _puzzlePiece.ParticleTrail.isPlaying == false)
                _puzzlePiece.ParticleTrail.Play();
        }

        protected override void DragStop()
        {
            base.DragStop();

            StartCoroutine(SetToOriginalSortOrder());

            if (_puzzlePiece.ParticleTrail != null && _puzzlePiece.ParticleTrail.isPlaying)
                _puzzlePiece.ParticleTrail.Stop();
        }

        public IEnumerator SetToOriginalSortOrder()
        {
            yield return new WaitForSeconds(0.5f);

            if (_spriteRenderer != null)
                _spriteRenderer.sortingOrder = 0;
        }

    }
}
