using System;
using System.Collections;
using Assets.PuzzleEd.Scripts.Regular.Controllers;
using Assets.PuzzleEd.Scripts.Regular.Enums;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drag : ESMonoBehaviour
    {
        public string DragId;
        public bool RestorePositionOnMissDrop;
        public float RestoreSpeed = 1.3f;
        public Vector3 RestorePosition;
        public bool Draggable = true;
        public bool BeingTouched = false;
        public bool Dropped = false;
        public GameObject CollidingDropArea { get; set; }
        private Vector2 _tempPosition;

        void Awake()
        {
            RestorePosition = gameObject.transform.position;
        }

        #region Start Drag
        /// <summary>
        ///     Callback called when starting the drag
        /// </summary>

        public Action OnStart { get { return DragStart; } }

        protected virtual void DragStart()
        {
            Debug.Log("Start");
        }
        #endregion

        #region Dragging
        /// <summary>
        ///     Callback called when dragging
        /// </summary>

        public Action<Vector2> OnDrag { get { return Dragging; } }

        protected virtual void Dragging(Vector2 newPosition)
        {
            if (this.Draggable)
            {
                Debug.Log("Dragging");
                _tempPosition.x = newPosition.x;
                _tempPosition.y = newPosition.y;

                CachedTransform.position = _tempPosition;
            }
        }
        #endregion

        #region Stop Drag
        /// <summary>
        ///     Callback called when stopping the drag
        /// </summary>
      
        public Action OnStop { get { return DragStop; } }

        protected virtual void DragStop()
        {
            if (CollidingDropArea != null)
            {
                iTween.MoveTo(gameObject, iTween.Hash("x", CollidingDropArea.gameObject.transform.position.x, "Y", CollidingDropArea.gameObject.transform.position.y, "time", RestoreSpeed, "easetype", "easeOutBack"));
                BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnMoveTo, Vector3.zero);
            }
            else
            {
                RestoreToInitialPosition();
            }

            Debug.Log("Stop");
        }
        #endregion

        public void RestoreToInitialPosition()
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", RestorePosition.x, "Y", RestorePosition.y, "time", RestoreSpeed, "easetype", "easeOutBack"));
            BaseSoundController.Instance.PlaySoundByIndex(SoundStruct.OnMoveTo, Vector3.zero);
        }
    }
}
