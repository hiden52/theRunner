using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] float time;
    public int m, s, ms;

    private void Awake()
    {
        timeText = GetComponent<Text>();
        m = 0;
        s = 0;
        ms = 0;
    }

    private void Start()
    {
        StartCoroutine(Measure());
    }

    IEnumerator Measure()
    {
        int real;             

        while (true)
        {
            if (GameManager.Instance.Playing)
            {
                time += Time.deltaTime;
                real = (int)time;

                m = real / 60;
                s = real % 60;
                ms = Mathf.FloorToInt((time - real) * 100);

                timeText.text = $"{m} : {s} : {ms}";

                
            }

            yield return null;
        }
    }
}
