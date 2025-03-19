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
    [SerializeField] Animator animator;
    [SerializeField] int positionX = 4;
    [SerializeField] Rigidbody rb;
    [SerializeField] float avoidSpeed = 20f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        rb.freezeRotation = true;
        //InputManager.Instance.action += onKeyUpdate;
        InputManager.Instance.pressedKeyA += AvoidLeft;
        InputManager.Instance.pressedKeyD += AvoidRight;

    }
    private void OnDisable()
    {
        //InputManager.Instance.action -= onKeyUpdate;
        InputManager.Instance.pressedKeyA -= AvoidLeft;
        InputManager.Instance.pressedKeyD -= AvoidRight;
    }
    private void Update()
    {
        
    }

    void onKeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(currentLine != RoadLine.LEFT)
            {
                currentLine--;
                animator.Play("Avoid Left");
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (currentLine != RoadLine.RIGHT)
            {
                currentLine++;
                animator.Play("Avoid Right");
            }
        }
    }

    void AvoidLeft()
    {
        if (currentLine != RoadLine.LEFT)
        {
            currentLine--;
            animator.Play("Avoid Left");
        }
    }
    void AvoidRight()
    {
        if (currentLine != RoadLine.RIGHT)
        {
            currentLine++;
            animator.Play("Avoid Right");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        rb.position = Vector3.Lerp(rb.position, new Vector3((int)currentLine * -positionX, 0, 5), Time.deltaTime * avoidSpeed);
    }
}
