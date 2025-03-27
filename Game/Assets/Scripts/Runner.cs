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
    [SerializeField] Rigidbody rb;
    [SerializeField] float avoidSpeed = 20f;
    [SerializeField] GameObject followTarget;
    [SerializeField] CapsuleCollider capsule;
    [SerializeField] float animationSpeed;
    private Vector3 defaultPos;
    private Quaternion defaultColliderRotate;
    private int positionX = 4;
    private float animSpeedInterval = 0.05f;
    private float defaultAnimSpeed = 1f;


    private bool alive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        alive = true;
        animationSpeed = 1f;
    }

    private void Start()
    {
        defaultPos = transform.position;
        defaultColliderRotate = GetComponent<CapsuleCollider>().transform.rotation;
    }

    private void OnEnable()
    {
        rb.freezeRotation = true;
        InputManager.Instance.pressedKeyA += AvoidLeft;
        InputManager.Instance.pressedKeyD += AvoidRight;
        SpeedManager.Instance.EIncreaseSpeed += IncreaseAnimationSpeed;
        SpeedManager.Instance.EResetSpeed += ResetAnimationSpeed;

    }
    private void OnDisable()
    {
        InputManager.Instance.pressedKeyA -= AvoidLeft;
        InputManager.Instance.pressedKeyD -= AvoidRight;
        SpeedManager.Instance.EIncreaseSpeed -= IncreaseAnimationSpeed;
        SpeedManager.Instance.EResetSpeed -= ResetAnimationSpeed;
    }
    private void LateUpdate()
    {
        followTarget.transform.position = transform.position;
        if(!alive)
        {
            //followTarget.transform.position = Vector3.Lerp(
            //    followTarget.transform.position, 
            //    transform.position + Vector3.forward * capsule.height, 
            //    Time.deltaTime * avoidSpeed
            //    );

            followTarget.transform.position = transform.position + Vector3.forward * capsule.height;
        }
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
        if (!GameManager.Instance.Playing) return;

        if (currentLine != RoadLine.LEFT)
        {
            currentLine--;
            animator.Play("Avoid Left");
        }
    }
    void AvoidRight()
    {
        if (!GameManager.Instance.Playing) return;

        if (currentLine != RoadLine.RIGHT)
        {
            currentLine++;
            animator.Play("Avoid Right");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.Playing)
        {
            Move();
        }
        else
        {
            if(alive)
            {
                Die();
                InputManager.Instance.pressedKeySpace += ResetRunner;
            }
        }
    }


    void Move()
    {
        rb.position = Vector3.Lerp(rb.position, new Vector3((int)currentLine * -positionX, 0, 5), Time.deltaTime * avoidSpeed);
    }

    void Die()
    {
        rb.freezeRotation = false;
        alive = false;
        animator.Play("Death");

        // Change follow target
        FindAnyObjectByType<VirtualCamera>().GetComponent<VirtualCamera>().OnCollision();

        // Applies knockback force on collision based on speed
        rb.AddRelativeForce(transform.forward * (3) + transform.up * (2), ForceMode.Impulse);
    }

    public void ResetRunner()
    {
        //transform.position = defaultPos;
        //currentLine = RoadLine.CENTER;
        //animator.Rebind();
        //GetComponent<CapsuleCollider>().transform.rotation = defaultColliderRotate;

        InputManager.Instance.pressedKeySpace -= ResetRunner;
    }

    public void IncreaseAnimationSpeed()
    {
        animationSpeed = Mathf.Clamp(animationSpeed + animSpeedInterval, 1, 2);
        animator.SetFloat("RunSpeed", animationSpeed);
        //Debug.Log("Animation Speed up : " + animator.GetFloat("RunSpeed"));
    }

    public void ResetAnimationSpeed()
    {
        animationSpeed = defaultAnimSpeed;
        animator.SetFloat("RunSpeed", animationSpeed);
    }
}
