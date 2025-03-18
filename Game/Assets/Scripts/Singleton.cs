using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where : ���׸� ���� ����
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Awake() -> �������̵� ����
    //parent, child ȣ�� ���� Ȯ��

    private static T instance;
    public static T Instance {  get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindAnyObjectByType(typeof(T));
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
