using Assets.PuzzleEd.Scripts.Regular.Actions;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Inputs
{
    public class DragTouchInput : ESMonoBehaviour
    {
        private RaycastHit2D _hit;
        private Drag _dragComponent;
        private Vector3 _newPosition;
        
        void Update()
        {
            UpdateTouchInputs();
        }
 
        void UpdateTouchInputs()
        {
            if (Input.touchCount > 0)
            {
                _newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                //catch all touch events
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        GetHitObject();
                        if (_dragComponent != null)
                        {
                            _dragComponent.BeingTouched = true;
                            _dragComponent.OnStart();
                        }
                        break;
                    case TouchPhase.Moved:
                        if (_dragComponent != null)
                            _dragComponent.OnDrag(_newPosition);
                        break;
                    case TouchPhase.Ended:
                        if (_dragComponent != null)
                        {
                            _dragComponent.BeingTouched = false;
                            _dragComponent.OnStop();
                            _dragComponent = null;
                        }
                        break;
                }
            }
        }

        private void GetHitObject()
        {
            _hit = Physics2D.Raycast(_newPosition, Vector2.zero);
            if (_hit.collider != null)
            {
                Debug.Log("I'm hitting " + _hit.collider.name);
                _dragComponent = _hit.transform.gameObject.GetComponent<Drag>();
            }
        }
    }
}