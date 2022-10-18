using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public enum RingColor
{
    Yellow,
    Green,
    Blue
}

public class Ring : MonoBehaviour
{
    private Vector3 screenPoint;
    [SerializeField] private RingColor _ringColor;
    [SerializeField] private List<GameObject> _rings = new List<GameObject>();
    private Outline _outline;
    [SerializeField] private bool _isHighestRing;
    private Body _body;
    private Body _newBody;
    private Vector3 lastPos;

    private void Start()
    {
        SetRing(_ringColor);
        SetLastPos(transform.position);
    }

    public void SetBody(Body body)
    {
        _body = body;
    }

    public void SetLastPos(Vector3 pos)
    {
        lastPos = pos;
    }

    public void SetRing(RingColor ringColor)
    {
        _rings[(int) ringColor].SetActive(true);
        _outline = _rings[(int) ringColor].GetComponent<Outline>();
    }

    public RingColor GetRingColor()
    {
        return _ringColor;
    }


    public bool IsHighest()
    {
        return _isHighestRing;
    }

    public void SetHighest(bool value)
    {
        _isHighestRing = value;
    }


    private void OnMouseDown()
    {
        if (!IsHighest())
            return;
        transform.DOMoveY(10, 0.3f);
        _outline.enabled = true;
    }

    private void OnMouseDrag()
    {
        if (!IsHighest())
            return;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 150f, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        pos.z = transform.position.z;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    private void OnMouseUp()
    {
        if (!IsHighest())
            return;

        if (_newBody == null || !_newBody.SetLastRing(this) || _body == _newBody)
        {
            // transform.DOMove(lastPos, 0.3f).SetEase(Ease.OutBounce);
            transform.DOMoveX(lastPos.x, 0.1f).OnComplete(() =>
            {
                transform.DOMoveY(lastPos.y, 0.3f).SetEase(Ease.OutBounce);
            });
        }
        else
        {
            _body.RemoveLastRing();
            SetBody(_newBody);
            RingManager.Instance.ChangeRingBody.Invoke();
        }


        _outline.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            _newBody = other.GetComponent<Body>();
        }
    }
}