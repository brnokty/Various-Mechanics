using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UVLight : MonoBehaviour
{
    public GameObject[] ObjMasked;
    [SerializeField] private GameObject _UVLight;
    private Vector3 screenPoint;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        for (int i = 0; i < ObjMasked.Length; i++)
        {
            ObjMasked[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }


    // void Update()
    // {
    //     Vector3 mouse = Input.mousePosition;
    //     Ray castPoint = Camera.main.ScreenPointToRay(mouse);
    //     RaycastHit hit;
    //     if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
    //     {
    //         transform.position = hit.point;
    //     }
    // }

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
}