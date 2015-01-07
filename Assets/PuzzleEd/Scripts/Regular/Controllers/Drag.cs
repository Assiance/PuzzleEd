using UnityEngine;
using System.Collections;
using Assets.PuzzleEd.Scripts.Regular.Extensions;
using Assets.PuzzleEd.Scripts.Regular.General;

public class Drag : ESMonoBehaviour{

    public bool MoveToOriginalPosition = false;
    public string Id = "";
    //starting position
    Vector2 startingposition;


    // Use this for initialization
    void Start()
    {
        //store transformation reference
        startingposition = CachedTransform.position;


    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }
    //movement function 
    void Move()
    {

        if (Input.touchCount > 0)
        {
            Vector2 changePosition = Input.GetTouch(0).deltaPosition;
            //catch all touch events
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    DragPuzzlePiece(changePosition);
                    break;
                case TouchPhase.Ended:
                    break;

            }
        }
    }

    void DragPuzzlePiece(Vector2 changePosition)
    {
        CachedTransform.position = Vector2.Lerp(CachedTransform.position, changePosition, 1.0f * Time.fixedDeltaTime);
    }
}
