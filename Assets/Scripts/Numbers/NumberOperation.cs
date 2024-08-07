using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberOperation : MonoBehaviour
{
    [SerializeField] private TextMeshPro numberOperationText;

    private void Start()
    {
        numberOperationText.SetText(value.ToString());
    }
    public string value; // E.g., "5", "+", "7"
}
