using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int gridSizeX;
    public int gridSizeZ;

    private void Awake()
    {
        // Ensure there is only one instance of GridManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsWithinBounds(Vector3 position)
    {
        // Check if the position is within the bounds of the grid
        return position.x >= 0 && position.x < gridSizeX &&
               position.z >= 0 && position.z < gridSizeZ;
    }

    public bool IsOccupied(Vector3 position)
    {
        // Check if the position is occupied by another cube or obstacle
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Cube") || collider.gameObject.CompareTag("Obstacle"))
            {
                return true;
            }

        }
        return false;
    }
}
