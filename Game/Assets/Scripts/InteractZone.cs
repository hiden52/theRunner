using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    [SerializeField] IHitable reposObj;



    private void OnTriggerEnter(Collider other)
    {
        reposObj = other.gameObject.GetComponent<IHitable>();
        reposObj?.Activate();
    }
}
