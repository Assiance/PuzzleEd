using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class MusicController : MonoBehaviour
    {
        private float _volume;
        private AudioSource _source;
        private GameObject _sourceGameObject;
        private int _fadeState;
        private int _targetFadeState;
        private float _volumeOn;
        private float _targetVolume;

        public string GamePrefsName = "DefaultGame";
        public AudioClip Music;
        public bool LoopMusic;
        public float FadeTime = 15f;
        public bool ShouldFadeInAtStart = true;

        void Start()
        {
            //Will Fix Later
            //_volumeOn = PlayerPrefs.GetFloat(GamePrefsName + "_MusicVol");
            _volumeOn = 100;
            _sourceGameObject = new GameObject("Music_AudioSource");
            _source = _sourceGameObject.AddComponent<AudioSource>();
            _source.name = "MusicAudioSource";
            _source.playOnAwake = true;
            _source.clip = Music;
            _source.volume = _volume;

            if (ShouldFadeInAtStart)
            {
                _fadeState = 0;
                _volume = 0;
            }
            else
            {
                _fadeState = 1;
                _volume = _volumeOn;
            }

            _targetFadeState = 1;
            _targetVolume = _volumeOn;
            _source.volume = _volume;
        }

        void Update()
        {
            if(!_source.isPlaying && LoopMusic)
                _source.Play();

            if (_fadeState != _targetFadeState)
            {
                if (_targetFadeState == 1)
                {
                    if (_volume == _volumeOn)
                        _fadeState = 1;
                }
                else
                {
                    if (_volume == 0.0f)
                        _fadeState = 0;
                }

                _volume = Mathf.Lerp(_volume, _targetVolume, Time.deltaTime * FadeTime);
                _source.volume = _volume;
            }
        }

        public void FadeIn(float fadeAmount)
        {
            _volume = 0;
            _fadeState = 0;
            _targetFadeState = 1;
            _targetVolume = _volumeOn;
            FadeTime = fadeAmount;
        }

        public void FadeOut(float fadeAmount)
        {
            _volume = _volumeOn;
            _fadeState = 1;
            _targetFadeState = 0;
            _targetVolume = 0;
            FadeTime = fadeAmount;
        }
    }
}
