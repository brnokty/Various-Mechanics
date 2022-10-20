using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Panel successPanel;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void Win()
    {
        successPanel.Appear();
    }
    
}