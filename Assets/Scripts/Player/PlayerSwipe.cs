using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isTouchingCube;
    private Block currentCube;

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Check if the touch hits a cube
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Block cube = hit.collider.GetComponent<Block>();
                    if (cube != null)
                    {
                        // Set the touch start position and mark as touching a cube
                        touchStartPos = touch.position;
                        isTouchingCube = true;
                        currentCube = cube;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended && isTouchingCube)
            {
                // Calculate swipe direction
                Vector2 swipeDirection = touch.position - touchStartPos;

                // Determine the dominant direction of the swipe
                Vector3 direction = Vector3.zero;

                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    direction = (swipeDirection.x > 0) ? Vector3.right : Vector3.left;
                }
                else
                {
                    direction = (swipeDirection.y > 0) ? Vector3.forward : Vector3.back;
                }

                // Move the cube in the determined direction
                if (direction != Vector3.zero)
                {
                    currentCube.Slide(direction);
                }

                // Reset touch state
                isTouchingCube = false;
                currentCube = null;
            }
        }
    }
}
