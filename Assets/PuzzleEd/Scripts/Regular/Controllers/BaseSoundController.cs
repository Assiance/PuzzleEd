using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class BaseSoundController : ESMonoBehaviour
    {
        public AudioClip[] GameSounds;
        public float Volume = 100;
        public string GamePrefsName = "DefaultGame";

        private List<SoundObject> _soundObjectList;
        private SoundObject _tempSoundObject;

        #region Singleton
        private static BaseSoundController _instance;
        public static BaseSoundController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(BaseSoundController)) as BaseSoundController;

                    if (_instance == null)
                        _instance = new GameObject("BaseSoundController Temporary Instance", typeof(BaseSoundController)).GetComponent<BaseSoundController>();

                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                if (this != _instance)
                    Destroy(this.gameObject);
            }

            Volume = PlayerPrefs.GetFloat(GamePrefsName + "_SFXVol", Volume);
            _soundObjectList = new List<SoundObject>();

            foreach (var theSound in GameSounds)
            {
                _tempSoundObject = new SoundObject(theSound, theSound.name, Volume, this.gameObject);
                _soundObjectList.Add(_tempSoundObject);
            }
        }
        #endregion

        private void Start()
        {
            DontDestroyOnLoad(_instance.gameObject);
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
