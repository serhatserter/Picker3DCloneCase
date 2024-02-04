using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(10, 500)] float horizontalMoveFactor = 100;
    [SerializeField][Range(1, 10)] float horizontalMoveLimit = 4;


    private Vector3 startPos;
    private Vector3 startClickPos;
    private Vector3 currentClickPos;

    private Vector3 clickPrevious;
    private Vector3 clickNext;
    private Vector3 clickDelta;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0)) clickPrevious = Input.mousePosition;

        SetHorizontalMovement();
    }

    private void FixedUpdate()
    {
        clickNext = Input.mousePosition;
        if (Input.GetMouseButton(0)) clickDelta = clickNext - clickPrevious;

    }


    void SetHorizontalMovement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startClickPos = Input.mousePosition;
            startPos = transform.localPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentClickPos = Input.mousePosition;

            var diffPos = (currentClickPos - startClickPos) / horizontalMoveFactor;

            if (CheckHorizontalLimit())
            {
                var newPos = startPos + Vector3.right * diffPos.x;
                if (Mathf.Abs(newPos.x) <= horizontalMoveLimit) 
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, Time.deltaTime * 50);
            }

        }
        else if (!Input.GetMouseButtonUp(0))
        {
            startClickPos = Vector3.zero;
        }

    }

    bool CheckHorizontalLimit()
    {
        if (transform.localPosition.x >= horizontalMoveLimit)
        {
            if (clickDelta.x < 1f) return true;
            else return false;
        }
        else if (transform.localPosition.x <= -horizontalMoveLimit)
        {
            if (clickDelta.x > 1f) return true;
            else return false;
        }
        else { return true; }


    }
}
