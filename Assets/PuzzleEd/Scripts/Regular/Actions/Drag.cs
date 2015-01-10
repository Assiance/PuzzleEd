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

        public Action OnStart{ get { return Start; } }

        private void Start()
        {
            Debug.Log("Start");
        }

        public Action OnStop { get { return Stop; } }

        private void Stop()
        {
            Debug.Log("Stop");
        }

        public Action OnDrag { get { return Dragging; } }

        private void Dragging()
        {
            Debug.Log("Dragging");
        }

   
    }
}
