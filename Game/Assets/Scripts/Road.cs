using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

public class Road : MonoBehaviour
{
    [SerializeField] UnityEvent callback;
    

    public void Activate()
    {
        callback?.Invoke();
    }
    
}
