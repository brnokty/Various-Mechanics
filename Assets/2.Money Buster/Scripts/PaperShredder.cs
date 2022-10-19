using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PaperShredder : MonoBehaviour
{
    [SerializeField] private GameObject hider;

    public void DestroyMoney(Money money)
    {
        hider.SetActive(true);
        money.HideSubObjects();
        money.transform.SetParent(transform);
        money.transform.DOLocalMoveY(1.6f, 0.2f);
        money.transform.DOLocalMoveX(0, 0.2f).OnComplete(() =>
        {
            money.transform.DOLocalMoveZ(-7.2f, 0.2f).OnComplete(() =>
            {
                money.transform.DOLocalMoveZ(5f, 1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(money.gameObject);
                    hider.SetActive(false);
                    MoneyManager.Instance.GetNewMoney();
                });
            });
        });
    }
}