﻿using System;
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
        public string EnglishPuzzleName;
        public string SpanishPuzzleName;

        [HideInInspector]
        public List<PuzzlePiece> PuzzlePieces { get; set; }
        public List<PuzzleDrop> PuzzleDrops { get; set; }
        
        [HideInInspector]
        public List<LetterPiece> LetterPieces { get; set; }

        public List<GameObject> PuzzlePlacements { get; set; }
        public List<GameObject> LetterPlacements { get; set; }
        public List<LetterDrop> LetterDrops { get; set; }
        public GameObject CompletedPuzzle { get; set; }

        private string _puzzleName;

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
            _puzzleName = GameController.Instance.IsSpanish ? SpanishPuzzleName : EnglishPuzzleName;

            PuzzlePieces = FindObjectsOfType<PuzzlePiece>().ToList();
            PuzzleDrops = FindObjectsOfType<PuzzleDrop>().ToList();
            LetterPieces = FindObjectsOfType<LetterPiece>().Where(x => x.IsEnglish != GameController.Instance.IsSpanish).ToList();

            var tempList = new List<LetterPiece>();

            //Sort By PuzzleName
            foreach (var letter in _puzzleName)
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

            StartCoroutine(PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnMoveTo, Vector3.zero, 2f));
        }

        public void PuzzleFinished()
        {
            StopCoroutine(CheckIfPuzzlePiecesFinished());

            CompletedPuzzle.SetActive(false);
            PuzzleDrops.ForEach(x => x.gameObject.SetActive(false));

            BubblePuzzlePieces();
        }

        private void BubblePuzzlePieces()
        {
            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnBubblePuzzle, Vector3.zero);
            for (int i = 0; i < PuzzlePieces.Count; i++)
            {
                iTween.PunchScale(PuzzlePieces[i].gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 2f,
                    "delay", (i / 8f),
                    "oncomplete", "MovePuzzleToCenter",
                    "oncompletetarget", GameController.Instance.PuzzleManager.gameObject));
            }
        }

        private void MovePuzzleToCenter()
        {
            PuzzlePieces.ForEach(x => x.gameObject.SetActive(false));
            var completedPuzzleRenderer = CompletedPuzzle.GetComponent<SpriteRenderer>();
            completedPuzzleRenderer.color = Color.white;
            completedPuzzleRenderer.sortingOrder = 0;

            CompletedPuzzle.SetActive(true);

            iTween.MoveTo(CompletedPuzzle, iTween.Hash("position", Vector3.zero,
                                                                      "time", 2f,
                                                                      "delay", 2f));

            iTween.PunchScale(CompletedPuzzle, iTween.Hash("amount", new Vector3(.1f, .1f, 0f),
                                                                      "time", 1f,
                                                                      "delay", 3f,
                                                                      "oncomplete", "LoadLetters",
                                                                      "oncompletetarget", GameController.Instance.PuzzleManager.gameObject));

            StartCoroutine(PuzzleSoundController.PlayAnimalSound(EnglishPuzzleName, !GameController.Instance.IsSpanish, 3f));
        }

        private void LoadLetters()
        {
            //Say animal name
            //Make animal noise
            LetterDrops.ForEach(x => x.GetComponent<SpriteRenderer>().sortingOrder = 0);

            StartCoroutine(PlaceLettersAnimation());

            //One letter piece comes to bottom of screen at a time.
            //Says Letter when it comes down.
            //Move letters in a random order to the top of screen.
        }

        private IEnumerator PlaceLettersAnimation()
        {
            LetterDrops = LetterDrops.Where(x => x.IsEnglish != GameController.Instance.IsSpanish).ToList();

            var tempList = new List<LetterDrop>();
            tempList.AddRange(LetterDrops);

            //Order letter drops
            tempList = tempList.OrderBy(x => x.LetterOrder).ToList();

            foreach (var letterPiece in LetterPieces)
            {
                letterPiece.GetComponentInChildren<MeshRenderer>().sortingOrder = 1;

                var item = tempList.First(x => x.DropId == letterPiece.Character);
                letterPiece.transform.position = new Vector2(item.CachedTransform.position.x, item.CachedTransform.position.y);
                tempList.Remove(item);

                iTween.PunchScale(letterPiece.gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 1f));

                PuzzleSoundController.PlayLetterSound(letterPiece.Character.ToUpper(), !GameController.Instance.IsSpanish);

                yield return new WaitForSeconds(1.1f);
            }

            foreach (var letterPiece in LetterPieces)
            {
                iTween.PunchScale(letterPiece.gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 1f));
            }

            PuzzleSoundController.PlayAnimalSound(EnglishPuzzleName, !GameController.Instance.IsSpanish);

            StartCoroutine(MoveLettersToPlacements());
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

                randomLetters[i].GetComponent<Drag>().RestorePosition = LetterPlacements[i].transform.position;
            }

            yield return new WaitForSeconds(3.5f);

            LetterDrops.ForEach(x => x.gameObject.SetActive(true));
            LetterDrops.ForEach(x => x.GetComponent<BoxCollider2D>().enabled = false);

            yield return new WaitForSeconds(0.5f);

            var firstLetter = LetterDrops.First(x => x.LetterOrder == 1);
            firstLetter.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            firstLetter.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void LettersFinished()
        {
            StopCoroutine(CheckIfLetterPiecesFinished());
    
            StartCoroutine(FinishingLevel());
        }

        public IEnumerator FinishingLevel()
        {
            yield return new WaitForSeconds(0.5f);

            //Punch all Letters
            foreach (var letterPiece in LetterPieces)
            {
                iTween.PunchScale(letterPiece.gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, 0f),
                    "time", 1f));
            }

            //Punch completed puzzle
            iTween.PunchScale(CompletedPuzzle, iTween.Hash("amount", new Vector3(.1f, .1f, 0f),
                                                                      "time", 1f));

            //Say Animal Name
            PuzzleSoundController.PlayAnimalSound(EnglishPuzzleName, !GameController.Instance.IsSpanish);

            yield return new WaitForSeconds(1.2f);

            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.OnWinSound, Vector3.zero);

            yield return new WaitForSeconds(1.5f);

            PuzzleSoundController.Instance.PlaySoundByIndex(SoundStruct.YouWinVoice, Vector3.zero);

            //yield return new WaitForSeconds(2f);

            //GameController.Instance.PlayAd("defaultZone");

            yield return new WaitForSeconds(2f);

            GameController.Instance.LevelFinished();
        }


    }
}
