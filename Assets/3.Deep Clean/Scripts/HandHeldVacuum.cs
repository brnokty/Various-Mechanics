using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeldVacuum : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private List<Transform> poses = new List<Transform>();

    #endregion

    #region UNITY METHODS

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cips"))
        {
            other.GetComponent<Cips>().GoInside(transform, poses);
        }
    }

    #endregion
}