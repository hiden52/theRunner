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
    [SerializeField] bool levelUp;

    private void Awake()
    {
        timeText = GetComponent<Text>();
        levelUp = false;
    }

    private void Start()
    {
        StartCoroutine(Measure());
    }

    IEnumerator Measure()
    {
        int real;

        int m, s, ms;

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

                if(!levelUp && (s > 0) && (s % 5 == 0))
                {
                    levelUp = true;
                    GameManager.Instance.IncreasGameLevel();
                    
                }
                else if (s % 10 != 0)
                {
                    levelUp = false;
                }
            }

            yield return null;
        }
    }
}
