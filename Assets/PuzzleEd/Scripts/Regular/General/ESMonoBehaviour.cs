using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.General
{
    public abstract class ESMonoBehaviour : MonoBehaviour
    {
        private Transform _cachedTransform;

        protected Transform CachedTransform
        {
            get
            {
                if (_cachedTransform == null)
                    _cachedTransform = GetComponent<Transform>();

                return _cachedTransform;
            }
        }
    }
}

