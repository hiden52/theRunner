using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }
    
    public void OnEnter()
    {
        text.fontSize = 80;
    }

    public void OnExit()
    {
        text.fontSize = 65;
        
    }

    public void OnBtnDown()
    {
        text.fontSize = 50;
        text.color = Color.gray;
    }
    public void OnBtnUp()
    {
        text.color = Color.white;
        OnEnter();
    }


    
}
