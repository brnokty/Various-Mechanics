using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private List<GameObject> subObjects = new List<GameObject>();
    [SerializeField] private bool isReal;
    [SerializeField] private bool isUseable;

    #endregion

    #region PRIVATE PROPERTIES

    private Vector3 screenPoint;
    private Vector3 startPos;
    private Tween rotateTween;
    private PaperShredder _paperShredder;
    private MoneyStack _moneyStack;
    private bool isDragable;
    private Tween lastTween;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        meshRenderer.material.renderQueue = 3002;
        startPos = transform.position;
        ChooseRealOrFake();
    }

    #endregion

    #region PUBLIC METHODS

    public bool GetIsReal()
    {
        return isReal;
    }

    public void SetMoneyUseable(bool value)
    {
        isUseable = value;
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        startPos = startPosition;
    }

    public void ChooseRealOrFake()
    {
        isReal = Random.Range(0, 2) == 1;

        if (isReal)
        {
            subObjects[0].SetActive(true);
            subObjects[3].SetActive(true);
        }
        else
        {
            if (Random.Range(0, 2) == 1)
            {
                subObjects[0].SetActive(true);
                subObjects[2].SetActive(true);
            }
            else
            {
                subObjects[1].SetActive(true);
                subObjects[3].SetActive(true);
            }
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

    #endregion

    #region PRIVATE METHODS

    private void OnMouseDown()
    {
        if (!isUseable)
            return;
        lastTween?.Kill();
        lastTween = transform.DOMoveY(3f, 0.5f).OnComplete(() => { isDragable = true; });
        // transform.DORotate(new Vector3(0, 0, -75f), 0.4f);
    }

    private void OnMouseUp()
    {
        if (!isUseable)
            return;

        if (_paperShredder == null && _moneyStack == null)
        {
            lastTween?.Kill();
            lastTween = transform.DOMove(startPos, 0.5f);
        }
        else if (_paperShredder != null)
            _paperShredder.DestroyMoney(this);
        else
            _moneyStack.AddMoney(this);

        isDragable = false;
        // transform.DORotate(Vector3.zero, 0.4f);
    }

    private void OnMouseDrag()
    {
        if (!isUseable)
            return;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUseable)
            return;

        if (other.CompareTag("PaperShredder") || other.CompareTag("MoneyDeck"))
        {
            rotateTween?.Kill();
            rotateTween = transform.DORotate(new Vector3(0, 270, 0), 0.5f);

            if (other.CompareTag("PaperShredder"))
            {
                _paperShredder = other.GetComponent<PaperShredder>();
                _moneyStack = null;
            }
            else
            {
                _moneyStack = other.GetComponent<MoneyStack>();
                _paperShredder = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isUseable)
            return;

        if (other.CompareTag("PaperShredder") || other.CompareTag("MoneyDeck"))
        {
            if (other.CompareTag("PaperShredder"))
                _paperShredder = null;
            else
                _moneyStack = null;


            if (_moneyStack == null && _paperShredder == null)
            {
                rotateTween?.Kill();
                rotateTween = transform.DORotate(new Vector3(0, 180, 0), 0.5f);
            }
        }
    }

    #endregion
}