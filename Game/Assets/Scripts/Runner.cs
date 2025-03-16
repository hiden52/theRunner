using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum RoadLine
{
    LEFT = -1,
    CENTER = 0,
    RIGHT = 1,
}
public class Runner : MonoBehaviour
{
    [SerializeField] RoadLine currentLine;
    [SerializeField] int positionX = 4;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.freezeRotation = true;
    }
    private void Update()
    {
        onKeyUpdate();
    }

    void onKeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(currentLine != RoadLine.LEFT)
            {
                currentLine--;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (currentLine != RoadLine.RIGHT)
            {
                currentLine++;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        rb.position = new Vector3((int)currentLine * -positionX, 0, 5);
    }
}
