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
    [SerializeField] GameObject followTarget;
    [SerializeField] CapsuleCollider capsule;
    private Vector3 defaultPos;
    private Quaternion defaultColliderRotate;

    private bool alive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        alive = true;
    }

    private void Start()
    {
        defaultPos = transform.position;
        defaultColliderRotate = GetComponent<CapsuleCollider>().transform.rotation;
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
        //rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        alive = false;
        rb.AddRelativeForce(transform.forward * 5 + transform.up * 2, ForceMode.Impulse);
        animator.Play("Death");

    }

    public void ResetRunner()
    {
        //transform.position = defaultPos;
        //currentLine = RoadLine.CENTER;
        //animator.Rebind();
        //GetComponent<CapsuleCollider>().transform.rotation = defaultColliderRotate;

        InputManager.Instance.pressedKeySpace -= ResetRunner;
    }
}
