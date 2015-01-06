using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Managers
{
    public class PuzzleManager : ESMonoBehaviour
    {
        public string PuzzleName;

        [HideInInspector]
        public List<PuzzlePiece> PuzzlePieces { get; set; }

        [HideInInspector]
        public List<LetterPiece> LetterPieces { get; set; }

        private void Start()
        {
            PuzzlePieces = FindObjectsOfType<PuzzlePiece>().ToList();
            LetterPieces = FindObjectsOfType<LetterPiece>().ToList();

            StartCoroutine(CheckIfPuzzlePiecesFinished());
        }

        private IEnumerator CheckIfPuzzlePiecesFinished()
        {
            while (PuzzlePieces.Any(i => i.IsPlaced == false))
            {
                Debug.Log("Checked Puzzle Pieces");
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("All Puzzle Pieces In Position");
            StopCoroutine(CheckIfPuzzlePiecesFinished());
            GameController.Instance.PuzzleFinished();
            StartCoroutine(CheckIfLetterPiecesFinished());
        }

        private IEnumerator CheckIfLetterPiecesFinished()
        {
            while (LetterPieces.Any(i => i.IsPlaced == false))
            {
                Debug.Log("Checked Letter Pieces");
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("All Letter Pieces in Position");
            StopCoroutine(CheckIfLetterPiecesFinished());
            GameController.Instance.LettersFinished();
            GameController.Instance.LevelFinished();
        }
    }
}
