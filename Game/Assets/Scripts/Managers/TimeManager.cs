using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TimeManager : Singleton<TimeManager>
{

    //[SerializeField] GameObject watchObject;
    bool timeUpdated;
    [SerializeField] float activeTime = 2.5f;
    [SerializeField] float intervalActiveTime = 0.25f;
    [SerializeField] float maxActiveTime = 2.5f;
    [SerializeField] float minActiveTime = 0.3f;
    float defaultActiveTime = 2.5f;
    public float ActiveTime { get { return activeTime; } }

    public event Action updateTimeEvent;
    public float time { get { return watch.GameTime; } }
    private Watch watch;
    private float bestTime;

    protected override void Awake()
    {
        base.Awake();
        timeUpdated = false;
    }

    public void SetTime(float time)
    {
        bestTime = PlayerPrefs.GetFloat("Time");
        if (bestTime < time)
        {
            PlayerPrefs.SetFloat("Time", time);
        }

        Debug.Log("Current Time : " + time);
        Debug.Log("Best Time : " + PlayerPrefs.GetFloat("Time"));
    }
    private void Update()
    {
        if(watch == null)
        {
            // OnEnable 에서 할당하려고 했지만, 오브젝트를 찾지 못함.
            watch = GameObject.Find("ScreenCanvas").GetComponentInChildren<Watch>();
            return;
        }

        if (!timeUpdated && (watch.s > 0) && (watch.s % 5 == 0))
        {
            timeUpdated = true;
            SpeedManager.Instance.SpeedUp();
            UpdateTime();
        }
        else if (watch.s % 5 != 0)
        {
            timeUpdated = false;
        }
    }

    private void UpdateTime()
    {
        activeTime = Mathf.Clamp(activeTime - intervalActiveTime, minActiveTime, maxActiveTime);
        updateTimeEvent?.Invoke();
    }

    public void ResetActiveTime()
    {
        activeTime = defaultActiveTime;
        updateTimeEvent?.Invoke();
    }

}
