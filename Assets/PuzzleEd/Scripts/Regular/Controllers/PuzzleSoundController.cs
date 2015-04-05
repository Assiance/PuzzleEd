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

        public static void PlayLetterSound(string letter)
        {
            int animalIndex = GetLetterSoundIndex(letter);

            Instance.PlaySoundByIndex(animalIndex, Vector3.zero);
        }

        public static IEnumerator PlayLetterSound(string letter, float seconds)
        {
            int animalIndex = GetLetterSoundIndex(letter);

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

        private static int GetLetterSoundIndex(string letter)
        {
            int letterIndex = 0;

            switch (letter)
            {
                case "A":
                    letterIndex = 25;
                    break;
                case "B":
                    letterIndex = 26;
                    break;
                case "C":
                    letterIndex = 27;
                    break;
                case "D":
                    letterIndex =  28;
                    break;
                case "E":
                    letterIndex =  29;
                    break;
                case "F":
                    letterIndex =  30;
                    break;
                case "G":
                    letterIndex =  31;
                    break;
                case "H":
                    letterIndex =  32;
                    break;
                case "I":
                    letterIndex =  33;
                    break;
                case "J":
                    letterIndex =  34;
                    break;
                case "K":
                    letterIndex = 35;
                    break;
                case "L":
                    letterIndex = 36;
                    break;
                case "M":
                    letterIndex = 37;
                    break;
                case "N":
                    letterIndex = 38;
                    break;
                case "O":
                    letterIndex = 39;
                    break;
                case "P":
                    letterIndex = 40;
                    break;
                case "Q":
                    letterIndex = 41;
                    break;
                case "R":
                    letterIndex = 42;
                    break;
                case "S":
                    letterIndex = 43;
                    break;
                case "T":
                    letterIndex = 44;
                    break;
                case "U":
                    letterIndex = 45;
                    break;
                case "V":
                    letterIndex = 46;
                    break;
                case "W":
                    letterIndex = 47;
                    break;
                case "X":
                    letterIndex = 48;
                    break;
                case "Y":
                    letterIndex = 49;
                    break;
                case "Z":
                    letterIndex = 50;
                    break;
                default:
                    letterIndex = 0;
                    break;
            }

            return letterIndex;
        }
    }
}
