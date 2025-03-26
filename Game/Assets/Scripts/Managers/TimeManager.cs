using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TimeManager : Singleton<TimeManager>
{

    [SerializeField] GameObject watchObject;
    bool timeUpdated;
    [SerializeField] float activeTime = 2.5f;
    [SerializeField] float intervalActiveTime = 0.25f;
    [SerializeField] float maxActiveTime = 2.5f;
    [SerializeField] float minActiveTime = 0.3f;
    float defaultActiveTime = 2.5f;
    public float ActiveTime { get { return activeTime; } }

    public event Action updateTimeEvent;
    Watch watch;

    protected override void Awake()
    {
        base.Awake();
        timeUpdated = false;
    }
    private void Start()
    {
        watch = watchObject.GetComponent<Watch>();
    }

    private void Update()
    {
        if(watch == null)
        {
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
