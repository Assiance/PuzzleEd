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

        void OnCollisionEnter2D(Collision2D coll)
        {
            //get the game object we collided with class Drag and all is properties 
            Drag collidedDragObj=coll.gameObject.GetComponent<Drag>();
            if(dropid==collidedDragObj.id)
            {
                //
                Transform puzzlepiece = coll.gameObject.transform;
                Vector2 puzzlepiecePosition = puzzlepiece.position;
                coll.gameObject.rigidbody2D.isKinematic = false;
                //GameObject.Destroy(gameObject);
                gameObject.transform.position = puzzlepiecePosition;


            }
            Debug.Log("collide");
        }
    }
}
