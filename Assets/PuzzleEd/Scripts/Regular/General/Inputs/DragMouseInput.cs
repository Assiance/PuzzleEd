using Assets.PuzzleEd.Scripts.Regular.Actions;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Inputs
{
    public class DragMouseInput : ESMonoBehaviour
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
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {
                _newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //catch all touch events
                if (Input.GetMouseButtonDown(0))
                {
                    GetHitObject();
                    if (_dragComponent != null)
                    {
                        _dragComponent.BeingTouched = true;
                        _dragComponent.OnStart();
                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    if (_dragComponent != null)
                        _dragComponent.OnDrag(_newPosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (_dragComponent != null)
                    {
                        _dragComponent.BeingTouched = false;
                        _dragComponent.OnStop();
                        _dragComponent = null;
                    }
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