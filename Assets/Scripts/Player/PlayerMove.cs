using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    public Vector3 direction; // The direction in which the car moves (e.g., Vector3.right for right)
    public Vector3 targetPosition; // The target position the car aims to reach
    public float speed = 5f; // Speed of the car
    private bool isMoving = false;

    private void Update()
    {
        // Detect touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform && !isMoving)
                    {
                        Debug.Log("PLAYER START MOVE EVENT");
                        isMoving = true;
                        MoveCar();
                    }
                }
            }
        }
    }

    private void MoveCar()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = currentPosition + direction;

        // Continuously move the car as long as it can move to the next position and hasn't reached the target
        MoveToNextPosition(currentPosition, nextPosition);
    }

    private void MoveToNextPosition(Vector3 currentPosition, Vector3 nextPosition)
    {
        // Check if the next position is valid and if the car hasn't reached the target
        if (CanMoveTo(nextPosition) && nextPosition != targetPosition)
        {
            Debug.Log("PLAYER MOVE EVENT");
            transform.DOJump(nextPosition, 1,1,1 / speed).OnComplete(() =>
            {
                // Update the current position to the new position
                currentPosition = nextPosition;
                // Calculate the next position
                nextPosition = currentPosition + direction;

                // Check if the next position is the target or not valid
                if (nextPosition == targetPosition || !CanMoveTo(nextPosition))
                {
                    isMoving = false; // Stop the movement
                    Debug.Log("PLAYER MOVE ENDED EVENT");
                    return;
                }

                if(nextPosition==targetPosition)
                {
                    //Buraya color path deki mekanigi getir
                    Debug.Log("CORRECT");
                }

                // Move to the next position
                MoveToNextPosition(currentPosition, nextPosition);
            });
        }
        else
        {
            isMoving = false; // Stop the movement if it cannot move further
            
        }
    }

    private bool CanMoveTo(Vector3 position)
    {
        // Check if the position is within bounds and occupied by a cube
        return GridManager.Instance.IsWithinBounds(position) && GridManager.Instance.IsOccupied(position);
    }
}
