using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Controllers
{
    public class SoundObject
    {
        public AudioSource Source;
        public GameObject SourceGameObject;
        public Transform SourceTransform;
        public AudioClip Clip;
        public string Name;

        public SoundObject(AudioClip clip, string name, float volume, GameObject parentObject)
        {
            SourceGameObject = new GameObject("AudioSource_" + name);
            SourceTransform = SourceGameObject.transform;
            SourceTransform.parent = parentObject.transform;
            Source = SourceGameObject.AddComponent<AudioSource>();
            Source.name = "AudioSource_" + name;
            Source.playOnAwake = false;
            Source.clip = clip;
            Source.volume = volume;
            Clip = clip;
            Name = name;
        }

        public void PlaySound(Vector3 atPosition)
        {
            SourceTransform.position = atPosition;
            Source.PlayOneShot(Clip);
        }
    }
}
