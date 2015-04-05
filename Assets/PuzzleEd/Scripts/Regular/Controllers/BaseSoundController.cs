using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class BaseSoundController : MonoBehaviour
    {
        public static BaseSoundController Instance;

        public AudioClip[] GameSounds;
        public float Volume = 100;
        public string GamePrefsName = "DefaultGame";
        public bool PersistOnLoad = false;

        private List<SoundObject> _soundObjectList;
        private SoundObject _tempSoundObject;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Volume = PlayerPrefs.GetFloat(GamePrefsName + "_SFXVol", Volume);
            _soundObjectList = new List<SoundObject>();

            foreach (var theSound in GameSounds)
            {
                _tempSoundObject = new SoundObject(theSound, theSound.name, Volume);
                _soundObjectList.Add(_tempSoundObject);
            }

            if (PersistOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public void PlaySoundByIndex(int indexNumber, Vector3 position)
        {
            if (indexNumber > _soundObjectList.Count)
            {
                indexNumber = _soundObjectList.Count - 1;
            }

            _tempSoundObject = _soundObjectList[indexNumber];
            _tempSoundObject.PlaySound(position);
        }

        public IEnumerator PlaySoundByIndex(int soundIndex, Vector3 position, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            PlaySoundByIndex(soundIndex, position);
        }
    }
}
