using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class PuzzleSoundController : BaseSoundController
    {
        public static void PlayAnimalSound(string animal, bool isEnglish)
        {
            int animalIndex = GetAnimalSoundIndex(animal, isEnglish);

            Instance.PlaySoundByIndex(animalIndex, Vector3.zero);
        }

        public static IEnumerator PlayAnimalSound(string animal, bool isEnglish, float seconds)
        {
            int animalIndex = GetAnimalSoundIndex(animal, isEnglish);

            return Instance.PlaySoundByIndex(animalIndex, Vector3.zero, seconds);
        }

        public static void PlayLetterSound(string letter, bool isEnglish)
        {
            int animalIndex = GetLetterSoundIndex(letter, isEnglish);

            Instance.PlaySoundByIndex(animalIndex, Vector3.zero);
        }

        public static IEnumerator PlayLetterSound(string letter, bool isEnglish, float seconds)
        {
            int animalIndex = GetLetterSoundIndex(letter, isEnglish);

            return Instance.PlaySoundByIndex(animalIndex, Vector3.zero, seconds);
        }

        private static int GetAnimalSoundIndex(string animal, bool isEnglish)
        {
            int animalIndex = 0;

            switch (animal)
            {
                case "Fox":
                    animalIndex = isEnglish ? 5 : 6;
                    break;
                case "Buffalo":
                    animalIndex = isEnglish ? 7 : 8;
                    break;
                case "Cow":
                    animalIndex = isEnglish ? 9 : 10;
                    break;
                case "Duck":
                    animalIndex = isEnglish ? 11 : 12;
                    break;
                case "Goat":
                    animalIndex = isEnglish ? 13 : 14;
                    break;
                case "Horse":
                    animalIndex = isEnglish ? 15 : 16;
                    break;
                case "Rabbit":
                    animalIndex = isEnglish ? 17 : 18;
                    break;
                case "Sheep":
                    animalIndex = isEnglish ? 19 : 20;
                    break;
                case "Squirrel":
                    animalIndex = isEnglish ? 21 : 22;
                    break;
                case "Stork":
                    animalIndex = isEnglish ? 23 : 24;
                    break;
                default:
                    animalIndex = 0;
                    break;
            }

            return animalIndex;
        }

        private static int GetLetterSoundIndex(string letter, bool isEnglish)
        {
            int letterIndex = 0;

            switch (letter)
            {
                case "A":
                    letterIndex = isEnglish ? 25 : 52;
                    break;
                case "B":
                    letterIndex = isEnglish ? 26 : 53;
                    break;
                case "C":
                    letterIndex = isEnglish ? 27 : 54;
                    break;
                case "D":
                    letterIndex = isEnglish ? 28 : 55;
                    break;
                case "E":
                    letterIndex = isEnglish ? 29 : 56;
                    break;
                case "F":
                    letterIndex = isEnglish ? 30 : 57;
                    break;
                case "G":
                    letterIndex = isEnglish ? 31 : 58;
                    break;
                case "H":
                    letterIndex = isEnglish ? 32 : 59;
                    break;
                case "I":
                    letterIndex = isEnglish ? 33 : 60;
                    break;
                case "J":
                    letterIndex = isEnglish ? 34 : 61;
                    break;
                case "K":
                    letterIndex = isEnglish ? 35 : 62;
                    break;
                case "L":
                    letterIndex = isEnglish ? 36 : 63;
                    break;
                case "M":
                    letterIndex = isEnglish ? 37 : 64;
                    break;
                case "N":
                    letterIndex = isEnglish ? 38 : 65;
                    break;
                case "O":
                    letterIndex = isEnglish ? 39 : 66;
                    break;
                case "P":
                    letterIndex = isEnglish ? 40 : 67;
                    break;
                case "Q":
                    letterIndex = isEnglish ? 41 : 68;
                    break;
                case "R":
                    letterIndex = isEnglish ? 42 : 69;
                    break;
                case "S":
                    letterIndex = isEnglish ? 43 : 70;
                    break;
                case "T":
                    letterIndex = isEnglish ? 44 : 71;
                    break;
                case "U":
                    letterIndex = isEnglish ? 45 : 72;
                    break;
                case "V":
                    letterIndex = isEnglish ? 46 : 73;
                    break;
                case "W":
                    letterIndex = isEnglish ? 47 : 74;
                    break;
                case "X":
                    letterIndex = isEnglish ? 48 : 75;
                    break;
                case "Y":
                    letterIndex = isEnglish ? 49 : 76;
                    break;
                case "Z":
                    letterIndex = isEnglish ? 50 : 77;
                    break;
                default:
                    letterIndex = 0;
                    break;
            }

            return letterIndex;
        }
    }
}
