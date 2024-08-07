using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Train : MonoBehaviour
{
    public float speed = 5f;
    public int targetNumber;
    private List<Transform> pathPoints;
    private int currentPointIndex = 0;
    private bool isMoving = false;

    public List<string> collectedValues = new List<string>();
    [SerializeField] private int currentResult = 0;
    [SerializeField] private TextMeshPro resultText;
    private bool hasHandledCollision = false; // Flag to track collision handling

    public bool HasReachedEnd { get; private set; } = false;
    public bool IsSuccess { get; private set; } = false;

    public delegate void ReachEndHandler();
    public event ReachEndHandler OnReachEnd;

    private int initialValue; // Add this for the initial value
    private string currentOperation;

    void Start()
    {
        initialValue = currentResult; // Initialize the train's initial value
        UpdateResultText();
    }

    void Update()
    {
        if (!isMoving)
        {
            DetectTouch();
        }
    }

    public void Initialize(Transform[] waypoints)
    {
        pathPoints = new List<Transform>(waypoints);
        transform.position = pathPoints[0].position;
    }

    private void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        StartMoving();
                    }
                }
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            Transform targetPoint = pathPoints[currentPointIndex];
            transform.DOMove(targetPoint.position, speed).SetSpeedBased(true).OnComplete(() => 
            {
                currentPointIndex++;
                MoveToNextPoint();
            });
        }
        else
        {
            isMoving = false;
            HasReachedEnd = true;
            CheckGoalAchieved();
            OnReachEnd?.Invoke(); // Notify that this train has reached the end
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NumberOperation"))
        {
            collectedValues.Add(other.GetComponent<NumberOperation>().value);
            Destroy(other.gameObject);
            PerformMathOperation();
            UpdateResultText();
        }
        else if (other.CompareTag("Train"))
        {
            Train otherTrain = other.GetComponent<Train>();

            if (!hasHandledCollision && !otherTrain.hasHandledCollision)
            {
                hasHandledCollision = true;
                otherTrain.hasHandledCollision = true;
                HandleCollisionWithTrain(otherTrain.gameObject);
            }
        }
    }

    private void CheckGoalAchieved()
    {
        PerformMathOperation();
        IsSuccess = (currentResult == targetNumber);
        if (IsSuccess)
        {
            Debug.Log("Train achieved its goal!");
        }
        else
        {
            Debug.Log("Train did not achieve its goal. Current result: " + currentResult);
        }
    }

    private void HandleCollisionWithTrain(GameObject otherTrain)
    {
        Debug.Log("Collision detected with train: " + otherTrain.name);
        // Handle collision logic here
    }

    private void UpdateResultText()
    {
        resultText.SetText(currentResult.ToString());
    }

    private void PerformMathOperation()
    {
        currentResult = CalculateResult(collectedValues);

        if (currentResult == targetNumber)
        {
            Debug.Log("Goal achieved!");
        }
    }

    private int CalculateResult(List<string> values)
    {
        if (values.Count == 0)
        {
            Debug.LogWarning("No values to calculate.");
            return initialValue;
        }

        int result = initialValue; // Start with the initial value
        int currentNumber = 0;
        string lastOperation = null;
        bool hasOperation = false;

        foreach (var value in values)
        {
            if (int.TryParse(value, out currentNumber))
            {
                if (hasOperation)
                {
                    // Apply the last operation to the current number
                    switch (lastOperation)
                    {
                        case "+":
                            result += currentNumber;
                            break;
                        case "-":
                            result -= currentNumber;
                            break;
                        case "*":
                            result *= currentNumber;
                            break;
                        case "/":
                            if (currentNumber != 0)
                            {
                                result /= currentNumber;
                            }
                            else
                            {
                                Debug.LogWarning("Division by zero is not allowed.");
                            }
                            break;
                    }
                    // Clear the operation after applying it
                    hasOperation = false;
                }
            }
            else
            {
                // Update lastOperation if it's a valid operator
                if (value == "+" || value == "-" || value == "*" || value == "/")
                {
                    lastOperation = value;
                    hasOperation = true;
                }
            }
        }

        return result;
    }


}
