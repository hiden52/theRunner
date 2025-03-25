using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : Singleton<SpeedManager>
{
    [SerializeField] float speed;
    [SerializeField] float timeForSpeedup = 5;
    [SerializeField] float defaultSpeed = 20;
    public float Speed { get { return speed; } }

    protected override void Awake()
    {
        base.Awake();
        speed = defaultSpeed;
    }

    public void SpeedUp()
    {
        speed = Mathf.Clamp(speed + 2.5f, 20, 60);
        GameManager.Instance.UpdateGameSpeed();
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }
}
