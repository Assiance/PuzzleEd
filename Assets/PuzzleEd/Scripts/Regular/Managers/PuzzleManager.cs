using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.PuzzleEd.Scripts.Regular.Actions;
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

        public List<GameObject> PuzzlePlacements { get; set; }
        public List<GameObject> LetterPlacements { get; set; } 

        private void Start()
        {
            PuzzlePieces = FindObjectsOfType<PuzzlePiece>().ToList();
            LetterPieces = FindObjectsOfType<LetterPiece>().ToList();
            PuzzlePlacements = GameObject.FindGameObjectsWithTag("PuzzlePlacement").ToList();
            LetterPlacements = GameObject.FindGameObjectsWithTag("LetterPlacement").ToList();

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
            GameController.Instance.LettersFinished();
        }

        public void InitiatePuzzle(string puzzleName)
        {
            foreach (var puzzlePiece in PuzzlePieces)
            {
                puzzlePiece.gameObject.transform.position = new Vector2(0f, 30f);
            }

            foreach (var letterPiece in LetterPieces)
            {
                letterPiece.gameObject.transform.position = new Vector2(0f, 30f);
            }

            //LoadLetters();
            LoadPuzzle();
        }

        private void LoadPuzzle()
        {
            foreach (var piece in PuzzlePieces)
            {
                iTween.MoveTo(piece.gameObject, Vector3.zero, 2f);
            }

            var rnd = new System.Random();
            var randomPieces = PuzzlePieces.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < randomPieces.Count; i++)
            {
                iTween.MoveTo(randomPieces[i].gameObject, iTween.Hash("position", new Vector3(PuzzlePlacements[i].transform.position.x, PuzzlePlacements[i].transform.position.y, 0f), 
                                                                      "time", 2f, 
                                                                      "delay", 2f));

                var dragComponent = randomPieces[i].GetComponent<Drag>();
                dragComponent.RestorePosition = PuzzlePlacements[i].gameObject.transform.position;
            }
        }

        public void PuzzleFinished()
        {
            //bubble puzzle pieces
            //replaces pieces with full pic and move to center of screen
            //Say animal name
            //Make animal noise
            LoadLetters();
        }

        private void LoadLetters()
        {
            for (int i = 0; i < LetterPieces.Count; i++)
            {
                iTween.MoveTo(LetterPieces[i].gameObject, iTween.Hash("position", Vector3.zero,
                                                                      "time", 2f,
                                                                      "delay", i * 2f));
            }

            //One letter piece comes to bottom of screen at a time.
            //Says Letter when it comes down.
            //Move letters in a random order to the top of screen.
            var rnd = new System.Random();
            var randomLetters = LetterPieces.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < randomLetters.Count; i++)
            {
                iTween.MoveTo(randomLetters[i].gameObject, iTween.Hash("position", new Vector3(LetterPlacements[i].transform.position.x, LetterPlacements[i].transform.position.y, 0f),
                                                                      "time", 2f,
                                                                      "delay", 10f));
            }
        }

        public void LettersFinished()
        {
            //Spell Animal Name
            //Say Animal Name
            //Make Animal Noise
            //Go to next level
            GameController.Instance.LevelFinished();
            
        }


    }
}
