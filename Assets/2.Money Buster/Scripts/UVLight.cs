using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UVLight : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private GameObject _UVLight;

    #endregion

    #region PRIVATE PROPERTIES

    private Vector3 screenPoint;
    private Vector3 startPos;

    #endregion

    #region UNITY METHODS

    void Start()
    {
        startPos = transform.position;
    }

    #endregion

    #region PRIVATE METHODS

    private void OnMouseDown()
    {
        transform.DOMoveY(4.5f, 0.5f);
        transform.DORotate(new Vector3(0, 0, -75f), 0.4f).OnComplete(() => { _UVLight.SetActive(true); });
    }

    private void OnMouseUp()
    {
        transform.DOMove(startPos, 0.5f).OnComplete(() => { });
        transform.DORotate(Vector3.zero, 0.4f);
        _UVLight.SetActive(false);
    }

    private void OnMouseDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 150f, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    #endregion
}