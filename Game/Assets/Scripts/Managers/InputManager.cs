using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action pressedKeyA;
    public Action pressedKeyD;
    public Action action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey == false) return;

        //action?.Invoke();
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedKeyA?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedKeyD?.Invoke();
        }

    }
}
