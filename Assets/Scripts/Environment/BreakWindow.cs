using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BreakWindow : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> breaks=new List<Rigidbody>();

    internal Color color;

    private void Start()
    {
        for (int i = 0; i < breaks.Count; i++)
        {
            breaks[i].GetComponent<MeshRenderer>().material.color = color;
            breaks[i].isKinematic=false;
        }

        transform.DOScale(Vector3.zero,2).OnComplete(()=>{
            Destroy(gameObject);
        });
    }



}
