using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Money : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private List<GameObject> subObjects = new List<GameObject>();
    [SerializeField] private bool isReal;
    private Vector3 screenPoint;
    private Vector3 startPos;
    private Tween rotateTween;
    private PaperShredder _paperShredder;

    private void Start()
    {
        meshRenderer.material.renderQueue = 3002;
        startPos = transform.position;

        subObjects[isReal ? 0 : 1].SetActive(true);
    }

    public bool GetIsReal()
    {
        return isReal;
    }

    private void OnMouseDown()
    {
        transform.DOMoveY(3f, 0.5f);
        // transform.DORotate(new Vector3(0, 0, -75f), 0.4f);
    }

    private void OnMouseUp()
    {
        if (_paperShredder == null)
            transform.DOMove(startPos, 0.5f);
        else
            _paperShredder.DestroyMoney(this);
        // transform.DORotate(Vector3.zero, 0.4f);
    }

    private void OnMouseDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaperShredder"))
        {
            rotateTween?.Kill();
            rotateTween = transform.DORotate(new Vector3(0, 270, 0), 0.5f);
            _paperShredder = other.GetComponent<PaperShredder>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PaperShredder"))
        {
            rotateTween?.Kill();
            rotateTween = transform.DORotate(new Vector3(0, 180, 0), 0.5f);
            _paperShredder = null;
        }
    }

    public void HideSubObjects()
    {
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        for (int i = 0; i < subObjects.Count; i++)
        {
            subObjects[i].SetActive(false);
        }
    }
}