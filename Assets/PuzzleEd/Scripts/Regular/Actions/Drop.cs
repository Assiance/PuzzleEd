using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions
{
    public class Drop : ESMonoBehaviour, IDroppable
    {
        public string DropId { get; set; }
        public string dropid = "";
        public Action OnDrop { get; private set; }
        public Action OnOver { get; private set; }
        public Action OnOut { get; private set; }

        void OnTriggerEnter2D(Collider2D coll)
        {

            Drag puzzlePiece=coll.gameObject.GetComponent<Drag>();
            puzzlePiece.LastObjectCollided = gameObject;
            ////get the game object we collided with class Drag and all is properties 
            //Drag puzzlePiece=coll.gameObject.GetComponent<Drag>();
            //if(dropid==puzzlePiece.id)
            //{
            //    //object match so we will remove the drag from object
            //    puzzlePiece.Draggable = false;
            //    //coll.gameObject.transform.position = gameObject.transform.position;
            //    iTween.MoveTo(coll.gameObject, iTween.Hash("x", gameObject.transform.position.x, "Y", gameObject.transform.position.y, "time",4));
            //}

       
            
            Debug.Log("collide");
        }
    }
}
