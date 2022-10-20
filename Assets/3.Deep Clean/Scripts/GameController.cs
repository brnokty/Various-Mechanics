using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private Transform handVacuum;
    [SerializeField] private Transform waterVacuum;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float turnAngle;
    [SerializeField] private float turnLerpSpeed;
    [SerializeField] private List<P3dChangeCounter> dirtChangeCounters = new List<P3dChangeCounter>();

    #endregion

    #region PUBLIC PROPERTIES

    public static GameController Instance;

    #endregion

    #region PRIVATE PROPERTIES

    private Transform activeVacuum;
    private Vector3 mouseFirstPosition;
    private Vector3 activeVacuumFirstPosition;
    private Vector3 distanceVector;
    private bool isWaterVacuum;
    private bool isOver;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activeVacuum = handVacuum;
        activeVacuumFirstPosition = activeVacuum.position;
    }


    private void Update()
    {
        if (isOver)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            distanceVector = Vector3.zero;
            mouseFirstPosition = Input.mousePosition;
            activeVacuumFirstPosition = activeVacuum.position;
        }

        if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
        {
            distanceVector = (mouseFirstPosition - Input.mousePosition) * 0.01f;
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
                Quaternion.Euler(new Vector3(0,
                    turnAngle * (isWaterVacuum ? -1 : 1) * Mathf.Abs(distanceVector.x * 0.1f), 0)),
                turnLerpSpeed * Time.deltaTime);
        }
        else if (distanceVector.x > 0)
        {
            activeVacuum.localRotation = Quaternion.Lerp(activeVacuum.rotation,
                Quaternion.Euler(new Vector3(0,
                    -turnAngle * (isWaterVacuum ? -1 : 1) * Mathf.Abs(distanceVector.x * 0.1f), 0)),
                turnLerpSpeed * Time.deltaTime);
        }


        if (isWaterVacuum)
        {
            DirtControl();
        }
    }

    #endregion

    #region PUBLIC METHODS

    public void CipsControl()
    {
        var list = GameObject.FindObjectsOfType<Cips>();

        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].GetIsCollected())
                return;
        }

        ChangeVacuum(1);
    }

    public void ChangeVacuum(int value)
    {
        if (value == 0)
        {
            activeVacuum = handVacuum;
            waterVacuum.gameObject.SetActive(false);
            handVacuum.gameObject.SetActive(true);
        }
        else
        {
            isWaterVacuum = true;
            activeVacuum = waterVacuum;
            waterVacuum.gameObject.SetActive(true);
            handVacuum.gameObject.SetActive(false);
        }

        mouseFirstPosition = Input.mousePosition;
        activeVacuumFirstPosition = activeVacuum.position;
        distanceVector = Vector3.zero;
    }

    public void DirtControl()
    {
        var ratio = P3dChangeCounter.GetRatio(dirtChangeCounters);
        print("Ratio : " + ratio);
        if (ratio > 0.99f)
        {
            UIManager.Instance.Win();
            isOver = true;
        }
    }

    #endregion
}