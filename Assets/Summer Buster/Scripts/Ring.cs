using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        SetRing(_ringColor);
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


    private void OnMouseDown()
    {
        _outline.enabled = true;
    }

    private void OnMouseDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 150f, screenPoint.z);
        var pos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        pos.z = transform.position.z;

        transform.position = pos;

        // if (area != null)
        // {
        //     // var mat = area.GetComponent<MeshRenderer>().material;
        //     // mat.color = Color.green;
        //     var sp = area.sprite;
        //     // sp.color = Color.green;
        //     sp.gameObject.SetActive(true);
        //
        //     if (area.character != null)
        //     {
        //         var material = area.character.GetComponent<PlayerCharacter>().characterRenderer.material;
        //         material.color = changeColor;
        //         area.character.GetComponent<PlayerCharacter>().outline.enabled = true;
        //     }
        // }
    }

    private void OnMouseUp()
    {
        _outline.enabled = false;
    }
}