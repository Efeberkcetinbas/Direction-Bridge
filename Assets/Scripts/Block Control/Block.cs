using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public void Slide(Vector3 direction)
    {
        // Calculate the target position
        Vector3 targetPosition = transform.position + direction;
        Debug.Log("SELECT SOUND");
        // Slide the cube until it encounters an obstacle or reaches the grid boundary
        while (CanSlide(targetPosition))
        {
            // Use DOTween to animate the movement to the target position
            transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad);

            // Calculate the next target position
            targetPosition += direction;
            Debug.Log("MULTIPLE SOUND");
            // Break the loop if the next position is not within bounds or is occupied
            if (!CanSlide(targetPosition))
            {
                Debug.Log("MOVE SOUND");
                break;
            }
        }
    }

    private bool CanSlide(Vector3 position)
    {
        // Check if the new position is within bounds and not occupied
        return GridManager.Instance.IsWithinBounds(position) && !GridManager.Instance.IsOccupied(position);
    }
}
