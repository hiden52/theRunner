using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stage Manager로 플레이 관리
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

    // 1번 씬이 로드되면 실행하도록 수정하자.
    // GameManager가 생성되는 0번 씬에서는 TimeManager가 존재하지 않기 때문에 nullref 에러가 발생
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



    // 현재 다시시작 시 obstacle의 수가 초기 값보다 많은 현상이 발생 #2025.03.25
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
