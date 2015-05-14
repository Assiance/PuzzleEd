using Assets.PuzzleEd.Scripts.Regular.Actions;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Inputs
{
    public class DragMouseInput : ESMonoBehaviour
    {
        private Collider2D _hitCollider;
        private Drag _dragComponent;
        private Vector3 _newPosition;

        public float ClickRadius = 1f;
        
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
            _hitCollider = Physics2D.OverlapCircle(_newPosition, ClickRadius);
            if (_hitCollider != null)
            {
                Debug.Log("I'm hitting " + _hitCollider.name);
                _dragComponent = _hitCollider.transform.gameObject.GetComponent<Drag>();
            }
        }
    }
}