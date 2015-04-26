using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class MusicController : ESMonoBehaviour
    {
        private float _volume;
        private AudioSource _source;
        private GameObject _sourceGameObject;
        private int _fadeState;
        private int _targetFadeState;
        private float _volumeOn = 100f;
        private float _targetVolume;

        public string GamePrefsName = "DefaultGame";
        public AudioClip Music;
        public bool LoopMusic;
        public float FadeTime = 15f;
        public bool ShouldFadeInAtStart = true;

        #region Singleton
        private static MusicController _instance;
        public static MusicController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(MusicController)) as MusicController;

                    if (_instance == null)
                        _instance = new GameObject("MusicController Temporary Instance", typeof(MusicController)).GetComponent<MusicController>();
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
        }
        #endregion

        void Start()
        {
            DontDestroyOnLoad(_instance.gameObject);

            _volumeOn = PlayerPrefs.GetFloat(GamePrefsName + "_MusicVol", _volumeOn);
            _sourceGameObject = new GameObject("Music_AudioSource");
            _source = _sourceGameObject.AddComponent<AudioSource>();
            _source.name = "MusicAudioSource";
            _source.playOnAwake = true;
            _source.clip = Music;
            _source.volume = _volume;

            _sourceGameObject.transform.parent = CachedTransform;

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
