using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    [SerializeField] Road road;



    private void OnTriggerEnter(Collider other)
    {
        road = other.gameObject.GetComponent<Road>();
        road?.Activate();
    }
}
