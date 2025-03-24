using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stage Manager로 플레이 관리
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float speed;
    [SerializeField] public float Speed { get { return speed; } }
    [SerializeField] private float respawnTime;
    [SerializeField] public float RespawnTime { get { return respawnTime; } }

    [SerializeField] public float gameTime;
    [SerializeField] Text timeText;
    [SerializeField] bool playing;
    public bool Playing { get { return playing; } }
    int m, s, ms, real;

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
        speed = 20;
        respawnTime = 2.5f;
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
    }
    public void StopStage()
    {
        playing = false;
        MouseManager.Instance.SetState(0);
    }

    public void IncreasGameLevel()
    {
        respawnTime = Mathf.Clamp(respawnTime - 0.2f, 1, 3);
        speed = Mathf.Clamp(speed + 2, 10, 50);
    }

    

}
