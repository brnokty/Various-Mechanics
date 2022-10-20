using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cips : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private bool isCollected;

    #endregion

    #region PUBLIC METHODS

    public void GoInside(Transform vacuum, List<Transform> poses)
    {
        if (isCollected)
            return;

        isCollected = true;

        transform.SetParent(vacuum);
        transform.DOLocalMove(poses[0].localPosition, 0.2f).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.09f);
            transform.DOLocalMove(poses[1].localPosition, 0.1f).OnComplete(() =>
            {
                GameController.Instance.CipsControl();
                Destroy(gameObject, 0.1f);
            });
        });
    }

    #endregion
}