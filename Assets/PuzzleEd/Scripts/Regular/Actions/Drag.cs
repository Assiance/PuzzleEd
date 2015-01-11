using System;
using Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drag : ESMonoBehaviour, IDraggable
    {
        public string DragId { get; set; }
        public string id = "";
        public bool RevertPosition { get; set; }
        public Vector3 StartingPosition { get; set; }
        public bool Draggable { get; set; } 
        public Action OnStart{ get { return Start; } }
        private bool AlreadyPosition { get; set; }
        public Drag()
        {
            Draggable = true;
            AlreadyPosition = false;
           
        }
        private void Start()
        {
            StartingPosition = gameObject.transform.position;
            Debug.Log("Start");
        }

        public Action OnStop { get { return Stop; } }

        private void Stop()
        {
            if (LastObjectCollided != null)
            {
                Drop puzzlePieceBase = LastObjectCollided.gameObject.GetComponent<Drop>();
                if (this.id == puzzlePieceBase.dropid&& !AlreadyPosition)
                {
                    //object match so we will remove the drag from object
                    Draggable = false;
                    AlreadyPosition = true;
                    //coll.gameObject.transform.position = gameObject.transform.position;
                    iTween.MoveTo(gameObject, iTween.Hash("x", LastObjectCollided.gameObject.transform.position.x, "Y", LastObjectCollided.gameObject.transform.position.y, "time", 4));
                }
                else
                {
                    iTween.MoveTo(gameObject, iTween.Hash("x", StartingPosition.x, "Y", StartingPosition.y, "time", 4));
                }
            }
            else
            {
                iTween.MoveTo(gameObject, iTween.Hash("x", StartingPosition.x, "Y", StartingPosition.y, "time", 4));
            }
            Debug.Log("Stop");
        }

        public Action OnDrag { get { return Dragging; } }

        private void Dragging()
        {
            Debug.Log("Dragging");
        }




        public GameObject LastObjectCollided { get; set; }
    }
}
