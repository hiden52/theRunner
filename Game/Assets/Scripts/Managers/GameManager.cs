using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stage Manager로 플레이 관리
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float gameSpeed;
    [SerializeField] private float respawnTime;
    [SerializeField] public float RespawnTime { get { return respawnTime; } }

    [SerializeField] public float gameTime;
    [SerializeField] Text timeText;
    [SerializeField] bool playing;
    public bool Playing { get { return playing; } }
    int m, s, ms, real;
    float defaultRespawnTime = 2.5f;

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
        respawnTime = defaultRespawnTime;
    }

    void ClacTime()
    {
        real = (int)gameTime;
        m = real / 60;
        s = real % 60;
        ms = Mathf.FloorToInt((gameTime - real) *100);
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
        respawnTime = Mathf.Clamp(respawnTime - 0.2f, 0.5f, 3);
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
        StartStage();
    }


}
