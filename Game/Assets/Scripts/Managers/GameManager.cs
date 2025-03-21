using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stage Manager로 플레이 관리
public class GameManager : Singleton<GameManager>
{
    [SerializeField] public float speed = 20;
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

    

}
