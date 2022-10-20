using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnifyingGlass : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private Vector3 screenPoint;
    private Vector3 startPos;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        startPos = transform.position;
    }

    #endregion

    #region PRIVATE METHODS

    private void OnMouseDown()
    {
        transform.DOMoveY(3f, 0.5f);
    }

    private void OnMouseUp()
    {
        transform.DOMove(startPos, 0.5f);
    }

    private void OnMouseDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 100f, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    #endregion
}