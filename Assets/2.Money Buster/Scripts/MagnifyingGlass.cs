using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnifyingGlass : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        transform.DOMoveY(3f, 0.5f);
        // transform.DORotate(new Vector3(0, 0, -75f), 0.4f);
    }

    private void OnMouseUp()
    {
        transform.DOMove(startPos, 0.5f);
        // transform.DORotate(Vector3.zero, 0.4f);
    }

    private void OnMouseDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y+100f, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }
}