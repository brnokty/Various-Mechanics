using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Body : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private List<Ring> ringList = new List<Ring>();

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        for (int i = 0; i < ringList.Count; i++)
        {
            ringList[i].SetBody(this);
        }
    }

    #endregion

    #region PUBLIC METHODS

    public List<Ring> GetRingList()
    {
        return ringList;
    }

    public bool SetLastRing(Ring _ring)
    {
        if (ringList.Contains(_ring) || ringList.Count >= 3)
            return false;

        if (ringList.Count > 0)
        {
            if (ringList[ringList.Count - 1].GetRingColor() == _ring.GetRingColor())
            {
                ringList[ringList.Count - 1].SetHighest(false);
                ringList.Add(_ring);
                _ring.SetHighest(true);
                _ring.transform.DOMoveX(transform.position.x, 0.1f).OnComplete(() =>
                {
                    _ring.transform.DOMoveY(1.5f + (ringList.Count - 1) * 1.75f, 0.3f).SetEase(Ease.OutBounce)
                        .OnComplete(() => { _ring.SetLastPos(_ring.transform.position); });
                });

                return true;
            }
        }
        else
        {
            ringList.Add(_ring);
            _ring.SetHighest(true);
            _ring.transform.DOMoveX(transform.position.x, 0.1f).OnComplete(() =>
            {
                _ring.transform.DOMoveY(1.5f + (ringList.Count - 1) * 1.75f, 0.3f).SetEase(Ease.OutBounce)
                    .OnComplete(() => { _ring.SetLastPos(_ring.transform.position); });
            });

            return true;
        }

        return false;
    }

    public void RemoveLastRing()
    {
        if (ringList.Count <= 0)
            return;

        ringList.RemoveAt(ringList.Count - 1);
        if (ringList.Count > 0)
            ringList[ringList.Count - 1].SetHighest(true);
    }

    #endregion
}