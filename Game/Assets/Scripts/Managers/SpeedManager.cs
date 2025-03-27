using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : Singleton<SpeedManager>
{
    [SerializeField] float speed;
    [SerializeField] float timeForSpeedup = 5;
    [SerializeField] float defaultSpeed = 30;
    public float Speed { get { return speed; } }
    public event Action EIncreaseSpeed;
    public event Action EResetSpeed;

    protected override void Awake()
    {
        base.Awake();
        speed = defaultSpeed;
    }

    public void SpeedUp()
    {
        speed = Mathf.Clamp(speed + 2.5f, 30, 60);
        GameManager.Instance.UpdateGameSpeed();
        EIncreaseSpeed?.Invoke();
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
        EResetSpeed?.Invoke();
    }
}
