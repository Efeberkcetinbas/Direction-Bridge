using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [System.Serializable]
    public class TrainPath
    {
        public Train train;
        public Transform[] waypoints;
    }

    public TrainPath[] trainPaths;

    private bool allTrainsReachedEnd = false;
    private int trainsCompleted = 0;

    void Start()
    {
        InitializeTrains();
    }

     private void InitializeTrains()
    {
        foreach (var trainPath in trainPaths)
        {
            trainPath.train.Initialize(trainPath.waypoints);
            trainPath.train.OnReachEnd += OnTrainReachEnd; // Subscribe to the event
        }
    }

    private void OnTrainReachEnd()
    {
        trainsCompleted++;
        CheckAllTrainsStatus();
    }

    private void CheckAllTrainsStatus()
    {
        if (trainsCompleted == trainPaths.Length)
        {
            bool allTrainsSuccessful = true;
            foreach (var trainPath in trainPaths)
            {
                if (!trainPath.train.IsSuccess)
                {
                    allTrainsSuccessful = false;
                    break;
                }
            }

            if (allTrainsSuccessful)
            {
                Debug.Log("All trains have completed their paths and achieved their goals!");
            }
            else
            {
                Debug.Log("Not all trains achieved their goals.");
            }
        }
    }
}
