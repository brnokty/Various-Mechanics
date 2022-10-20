using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeldVacuum : MonoBehaviour
{
    [SerializeField] private List<Transform> poses = new List<Transform>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cips"))
        {
            other.GetComponent<Cips>().GoInside(transform, poses);
        }
    }
}