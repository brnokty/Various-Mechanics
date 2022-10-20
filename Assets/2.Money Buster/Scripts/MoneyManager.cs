using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private MoneyDesk moneyDesk;

    #endregion

    #region PUBLIC PROPERTIES

    public static MoneyManager Instance;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region PUBLIC METHODS

    public void GetNewMoney()
    {
        moneyDesk.NewMoneyEvent.Invoke();
    }

    #endregion
}