using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private List<Money> moneyDeck = new List<Money>();

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        StartCoroutine(SortMoneys());
    }

    #endregion

    #region PUBLIC METHODS

    public void AddMoney(Money money)
    {
        moneyDeck.Add(money);
        money.transform.SetParent(transform);
        StartCoroutine(SortMoneys());
        MoneyManager.Instance.GetNewMoney();
    }

    #endregion

    #region PRIVATE METHODS

    private IEnumerator SortMoneys()
    {
        for (int i = 0; i < moneyDeck.Count; i++)
        {
            float waitTime = 0;
            if (i == (moneyDeck.Count - 1))
                waitTime = 0.3f;

            moneyDeck[i].transform.DOLocalMove(new Vector3(0, -1.2f + i * 0.1f, 0), waitTime);
            yield return new WaitForSeconds(waitTime + 0.01f);
        }
    }

    #endregion
}