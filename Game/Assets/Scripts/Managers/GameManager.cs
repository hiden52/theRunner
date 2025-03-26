using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stage Manager�� �÷��� ����
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float gameSpeed;
    [SerializeField] private float activeTime;
    [SerializeField] public float gameTime;
    [SerializeField] Text timeText;
    [SerializeField] bool playing;
    [SerializeField] float offsetActiveTime = 0.25f;
    [SerializeField] float MaxActiveTime = 2.5f;
    [SerializeField] float MinActiveTime = 0.3f;
    public bool Playing { get { return playing; } }

    private void Update()
    {
        //if (playing)
        //{
        //    gameTime += Time.deltaTime;
        //    ClacTime();
        //    timeText.text = $"{m} : {s} . {ms}";
        //}
    }

    private void Start()
    {
        playing = true;
        gameTime = 0;
    }

    public void StartStage()
    {
        playing = true;
        MouseManager.Instance.SetState(1);
        InputManager.Instance.pressedKeySpace -= ResetGame;
        ObstacleManager.Instance.ActivateObstacles();
    }
    public void StopStage()
    {
        playing = false;
        MouseManager.Instance.SetState(0);
        InputManager.Instance.pressedKeySpace += ResetGame;
        
    }

    public void IncreasGameLevel()
    {
        activeTime = Mathf.Clamp(activeTime - offsetActiveTime, MinActiveTime, MaxActiveTime);
    }

    public void BindTimeManagerEvent()
    {
        TimeManager.Instance.updateTimeEvent += UpdateActiveTime;
    }

    // 1�� ���� �ε�Ǹ� �����ϵ��� ��������.
    // GameManager�� �����Ǵ� 0�� �������� TimeManager�� �������� �ʱ� ������ nullref ������ �߻�
    public void UnbindTimeManagerEvent()
    {
        TimeManager.Instance.updateTimeEvent += UpdateActiveTime;
    }
    public void UpdateGameSpeed()
    {
        gameSpeed = SpeedManager.Instance.Speed;
    }

    public void ResetGame()
    {
        Debug.Log("Reset Game");
        SceneryManager.Instance.loadEvent += ResetStage;
        StartCoroutine(SceneryManager.Instance.AsynchLoad(1));
    }



    // ���� �ٽý��� �� obstacle�� ���� �ʱ� ������ ���� ������ �߻� #2025.03.25
    public void ResetStage()
    {
        ObstacleManager.Instance.ResetObstacles();
        SpeedManager.Instance.ResetSpeed();
        TimeManager.Instance.ResetActiveTime();
        StartStage();
    }

    private void UpdateActiveTime()
    {
        activeTime = TimeManager.Instance.ActiveTime;
    }

}
