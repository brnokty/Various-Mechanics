using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform handVacuum;
    [SerializeField] private Transform waterVacuum;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float turnAngle;
    [SerializeField] private float turnLerpSpeed;

    private Transform activeVacuum;

    private Vector3 mouseFirstPosition;
    private Vector3 activeVacuumFirstPosition;
    private Vector3 distanceVector;

    private void Start()
    {
        activeVacuum = handVacuum;
        activeVacuumFirstPosition = activeVacuum.position;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            distanceVector = Vector3.zero;
            mouseFirstPosition = Input.mousePosition;
            activeVacuumFirstPosition = activeVacuum.position;
        }

        if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
        {
            distanceVector = (mouseFirstPosition - Input.mousePosition) * 0.01f;
            print("Mouse :" + Input.mousePosition);
        }


        if (Input.GetMouseButtonUp(0))
        {
            mouseFirstPosition = Input.mousePosition;
            activeVacuumFirstPosition = activeVacuum.position;
            distanceVector = Vector3.zero;
        }


        activeVacuum.position = Vector3.Lerp(activeVacuum.position,
            activeVacuumFirstPosition - new Vector3(distanceVector.x, 0, distanceVector.y),
            lerpSpeed * Time.deltaTime);


        if (distanceVector.x < 0)
        {
            activeVacuum.localRotation = Quaternion.Lerp(activeVacuum.rotation,
                Quaternion.Euler(new Vector3(0, turnAngle * Mathf.Abs(distanceVector.x * 0.1f), 0)),
                turnLerpSpeed * Time.deltaTime);
        }
        else if (distanceVector.x > 0)
        {
            activeVacuum.localRotation = Quaternion.Lerp(activeVacuum.rotation,
                Quaternion.Euler(new Vector3(0, -turnAngle * Mathf.Abs(distanceVector.x * 0.1f), 0)),
                turnLerpSpeed * Time.deltaTime);
        }
        // else if (distanceVector.x == 0)
        // {
        //     activeVacuum.localRotation = Quaternion.Lerp(activeVacuum.rotation,
        //         Quaternion.Euler(Vector3.zero),
        //         turnLerpSpeed * Time.deltaTime);
        // }
    }
}