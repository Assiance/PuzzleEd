using System;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drop : ESMonoBehaviour
    {
        public string DropId;
        public bool EscapeOndrop = true;

        public Action<Drag> OnSuccessDrop { get { return SuccessDrop; } }
 
        protected virtual void SuccessDrop(Drag dragComponent)
        {
            Debug.Log("Successful Drop");
        }

        public Action<Drag> OnFailDrop { get { return FailDrop; } }

        protected virtual void FailDrop(Drag dragComponent)
        {
            Debug.Log("Fail Drop");
            dragComponent.RestoreToInitialPosition();
        }

        public Action<Drag> OnHover { get { return DropHover; } }

        protected virtual void DropHover(Drag dragComponent)
        {
            Debug.Log("Hover");
        }

        public Action<Drag> OnOut { get { return DropOut; } }

        protected virtual void DropOut(Drag dragComponent)
        {
            Debug.Log("Out");
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
           
        }

        void OnTriggerStay2D(Collider2D coll)
        {
            Drag dragComponent = coll.gameObject.GetComponent<Drag>();

            if (dragComponent != null)
            {
                if(dragComponent.CollidingDropArea == null)
                    dragComponent.CollidingDropArea = gameObject;

                //Hover
                if (dragComponent.Dropped == false && dragComponent.BeingTouched)
                {
                    OnHover(dragComponent);
                }

                //Drop
                if (dragComponent.Dropped == false && dragComponent.BeingTouched == false)
                {
                    dragComponent.Dropped = true;

                    if (dragComponent.DragId == DropId)
                    {
                        OnSuccessDrop(dragComponent);

                        if (EscapeOndrop == false)
                            dragComponent.Draggable = false;
                    }
                    else
                    {
                        OnFailDrop(dragComponent);
                    }
                }
            }

            Debug.Log("collide");
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            Drag dragComponent = coll.gameObject.GetComponent<Drag>();

            if (dragComponent != null)
            {
                dragComponent.CollidingDropArea = null;
                dragComponent.Dropped = false;

                OnOut(dragComponent);
            }

            Debug.Log("collide");
        }
    }
}
