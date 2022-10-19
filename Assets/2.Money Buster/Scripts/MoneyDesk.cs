using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MoneyDesk : MonoBehaviour
{
    [SerializeField] private Transform moneyDefaultTrasform;
    [SerializeField] private List<Money> moneyDeck = new List<Money>();
    public UnityEvent NewMoneyEvent;

    private void Start()
    {
        NewMoneyEvent.AddListener(GetNewMoney);
        SortMoneys();
    }

    public void SortMoneys()
    {
        for (int i = 0; i < moneyDeck.Count; i++)
        {
            moneyDeck[i].transform.localPosition = new Vector3(0, -1.2f + i * 0.1f, 0);
        }
    }

    public void GetNewMoney()
    {
        var tempMoney = moneyDeck[0];
        moneyDeck.RemoveAt(0);
        tempMoney.transform.SetParent(null);
        tempMoney.transform.DOJump(moneyDefaultTrasform.position, 3, 1, 1f).OnComplete(() =>
        {
            tempMoney.SetMoneyUseable(true);
        });
        tempMoney.transform.DORotate(moneyDefaultTrasform.rotation.eulerAngles, 1f);
        tempMoney.SetStartPosition(moneyDefaultTrasform.position);
    }
}