using System;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drop : ESMonoBehaviour
    {
        public string DropId;
        public Action OnDrop { get; private set; }
        public Action OnOver { get; private set; }
        public Action OnOut { get; private set; }

        void OnTriggerEnter2D(Collider2D coll)
        {
            Drag dragComponent = coll.gameObject.GetComponent<Drag>();

            if(dragComponent != null)
                dragComponent.CollidingDropArea = gameObject;
            Debug.Log("collide");
        }

        //Figure out how to call onDrop
        //void OnTriggerStay2D(Collider2D coll)
        //{
        //    Drag dragComponent = coll.gameObject.GetComponent<Drag>();

        //    if(dragComponent != null && dragComponent.Draggable !)
        //        dragComponent.

        //    OnOver();
        //}

        void OnTriggerExit2D(Collider2D coll)
        {
            Drag dragComponent = coll.gameObject.GetComponent<Drag>();

            if (dragComponent != null)
                dragComponent.CollidingDropArea = null;
            Debug.Log("collide");
        }
    }
}
