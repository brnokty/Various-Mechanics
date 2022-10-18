using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RingManager : MonoBehaviour
{
    public static RingManager Instance;
    [SerializeField] private List<Body> bodys = new List<Body>();
    [HideInInspector] public UnityEvent ChangeRingBody;


    private void Awake()
    {
        Instance = this;
        ChangeRingBody.AddListener(ControlBodys);
    }

    public void ControlBodys()
    {
        for (int i = 0; i < bodys.Count; i++)
        {
            var body = bodys[i].GetRingList();

            if (body.Count < 3)
                if (body.Count > 0)
                    return;
                else
                    continue;


            RingColor _tempRingColor = RingColor.Blue;
            for (int j = 0; j < body.Count; j++)
            {
                if (j > 0)
                {
                    if (body[j].GetRingColor() != _tempRingColor)
                    {
                        return;
                    }
                }

                _tempRingColor = body[j].GetRingColor();
            }
        }

        print("Winnn");
    }
}