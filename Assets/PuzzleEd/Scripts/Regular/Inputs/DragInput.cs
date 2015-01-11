using System.Linq;
using Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Inputs
{
    public class DragInput : ESMonoBehaviour
    {
        private RaycastHit2D _hit;
        private IDraggable _draggableObject;
        private Vector3 _newPosition;
        private GameObject _gameObject;
        

        void Update()
        {
            Move();
        }
 
        void Move()
        {
            if (Input.touchCount > 0)
            {
                _newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                //catch all touch events
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        GetHitObject();
                        break;
                    case TouchPhase.Moved:
                        DragPuzzlePiece(_newPosition);
                        break;
                    case TouchPhase.Ended:
                        if (_draggableObject != null)
                            _draggableObject.OnStop();

                        _gameObject = null;
                        break;
                }
            }
        }

        private void GetHitObject()
        {
            _hit = Physics2D.Raycast(_newPosition, Vector2.zero);
            if (_hit != null && _hit.collider != null)
            {
                Debug.Log("I'm hitting " + _hit.collider.name);

                _draggableObject = _hit.transform.gameObject.GetComponent(typeof(IDraggable)) as IDraggable;

                if (_draggableObject != null)
                {
                    _gameObject = _hit.transform.gameObject;
                    //_draggableObject.StartingPosition = _hit.transform.position;
                 
                    _draggableObject.OnStart();
                }
            }
        }

        void DragPuzzlePiece(Vector2 changePosition)
        {

            if (_gameObject != null && _draggableObject.Draggable)
                _gameObject.transform.position = new Vector2(_newPosition.x, _newPosition.y);
            if (_draggableObject != null)
                _draggableObject.OnDrag();
        }
    }
}