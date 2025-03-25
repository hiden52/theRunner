using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] GameObject followTarget;
    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnCollision()
    {
        cinemachine.Follow = followTarget.transform;
    }
}
