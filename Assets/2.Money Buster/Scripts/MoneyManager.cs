using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [SerializeField] private MoneyDesk moneyDesk;

    private void Awake()
    {
        Instance = this;
    }

    public void GetNewMoney()
    {
        moneyDesk.NewMoneyEvent.Invoke();
    }
}