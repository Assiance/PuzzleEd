using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.PuzzleEd.Scripts.Regular.Actions;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Entities;
using Assets.PuzzleEd.Scripts.Regular.Enums;
using Assets.PuzzleEd.Scripts.Regular.General;
using Assets.PuzzleEd.Scripts.Regular.Helpers;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Managers
{
    public class PuzzleManager : ESMonoBehaviour
    {
        public string PuzzleName;

        [HideInInspector]
        public List<PuzzlePiece> PuzzlePieces { get; set; }
        public List<PuzzleDrop> PuzzleDrops { get; set; }
        
        [HideInInspector]
        public List<LetterPiece> LetterPieces { get; set; }

        public List<GameObject> PuzzlePlacements { get; set; }
        public List<GameObject> LetterPlacements { get; set; }
        public List<LetterDrop> LetterDrops { get; set; }
        public GameObject CompletedPuzzle { get; set; }

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

        public void InitiatePuzzle()
        {
            PuzzlePieces = FindObjectsOfType<PuzzlePiece>().ToList();
            PuzzleDrops = FindObjectsOfType<PuzzleDrop>().ToList();
            LetterPieces = FindObjectsOfType<LetterPiece>().Where(x => x.IsEnglish != GameController.Instance.IsSpanish).ToList();

            var tempList = new List<LetterPiece>();

            //Sort By PuzzleName
            foreach (var letter in PuzzleName)
            {
                var item = LetterPieces.First(x => x.Character.ToLower() == letter.ToString().ToLower());

                tempList.Add(item);
                LetterPieces.Remove(item);
            }

            LetterPieces.AddRange(tempList);

            LetterDrops = FindObjectsOfType<LetterDrop>().ToList();
            CompletedPuzzle = GameObject.FindGameObjectWithTag("CompletedPuzzle");

            LetterDrops.ForEach(x => x.gameObject.SetActive(false));

            PuzzlePlacements = GameObject.FindGameObjectsWithTag("PuzzlePlacement").ToList();

            if (GameController.Instance.IsSpanish == true)
            {
                LetterPlacements = GameObject.FindGameObjectsWithTag("LetterPlacementSpanish").ToList();
            }
            else
            {
                LetterPlacements = GameObject.FindGameObjectsWithTag("LetterPlacement").ToList();
            }

            StartCoroutine(CheckIfPuzzlePiecesFinished());

            foreach (var letterPiece in LetterPieces)
            {
                letterPiece.gameObject.transform.position = new Vector2(0f, 30f);
            }

            LoadPuzzle();
        }

        private void LoadPuzzle()
        {
            foreach (var piece in PuzzlePieces)
            {
                iTween.MoveBy(piece.gameObject, new Vector3(0f, -9.44f), 2f);
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

            StartCoroutine(PlayMoveToSound(2f));
        }

        public IEnumerator PlayMoveToSound(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnMoveTo, Vector3.zero);
        }

        public void PuzzleFinished()
        {
            StopCoroutine(CheckIfPuzzlePiecesFinished());

            CompletedPuzzle.SetActive(false);
            PuzzleDrops.ForEach(x => x.gameObject.SetActive(false));

            BubblePuzzlePieces();
        }

        private void MovePuzzleToCenter()
        {
            PuzzlePieces.ForEach(x => x.gameObject.SetActive(false));
            var completedPuzzleRenderer = CompletedPuzzle.GetComponent<SpriteRenderer>();
            completedPuzzleRenderer.color = Color.white;
            completedPuzzleRenderer.sortingOrder = -1;

            CompletedPuzzle.SetActive(true);

            iTween.MoveTo(CompletedPuzzle, iTween.Hash("position", Vector3.zero,
                                                                      "time", 2f,
                                                                      "delay", 2f));

            iTween.PunchScale(CompletedPuzzle, iTween.Hash("amount", new Vector3(.1f, .1f, 0f),
                                                                      "time", 1f,
                                                                      "delay", 3f,
                                                                      "oncomplete", "LoadLetters",
                                                                      "oncompletetarget", GameController.Instance.gameObject));
        }

        private void BubblePuzzlePieces()
        {
            for (int i = 0; i < PuzzlePieces.Count; i++)
            {
                iTween.PunchScale(PuzzlePieces[i].gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 2f,
                    "delay", (i/8f),
                    "oncomplete", "MovePuzzleToCenter",
                    "oncompletetarget", GameController.Instance.gameObject));
            }
        }

        private IEnumerator PlaceLettersAnimation()
        {
            var tempList = new List<LetterDrop>();
            tempList.AddRange(LetterDrops);

            foreach (var letterPiece in LetterPieces)
            {
                var item = tempList.First(x => x.DropId == letterPiece.Character);
                letterPiece.transform.position = new Vector2(item.CachedTransform.position.x, item.CachedTransform.position.y);
                tempList.Remove(item);

                iTween.PunchScale(letterPiece.gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 1f));

                yield return new WaitForSeconds(1.1f);
            }

            foreach (var letterPiece in LetterPieces)
            {
                iTween.PunchScale(letterPiece.gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 1f));
            }

            StartCoroutine(MoveLettersToPlacements());
        }

        private void LoadLetters()
        {
            //Say animal name
            //Make animal noise
            StartCoroutine(PlaceLettersAnimation());

            //One letter piece comes to bottom of screen at a time.
            //Says Letter when it comes down.
            //Move letters in a random order to the top of screen.
        }

        private IEnumerator MoveLettersToPlacements()
        {
            var rnd = new System.Random();
            var randomLetters = LetterPieces.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < randomLetters.Count; i++)
            {
                iTween.MoveTo(randomLetters[i].gameObject, iTween.Hash("position", new Vector3(LetterPlacements[i].transform.position.x, LetterPlacements[i].transform.position.y, 0f),
                                                                      "time", 2f,
                                                                      "delay", 3f));
            }

            yield return new WaitForSeconds(3.5f);

            LetterDrops.Where(x => x.IsEnglish != GameController.Instance.IsSpanish).ToList().
                ForEach(x => x.gameObject.SetActive(true));
        }

        public void LettersFinished()
        {
            StopCoroutine(CheckIfLetterPiecesFinished());
            //Spell Animal Name
            //Say Animal Name
            //Make Animal Noise
            //Go to next level
            GameController.Instance.LevelFinished();
            
        }


    }
}
