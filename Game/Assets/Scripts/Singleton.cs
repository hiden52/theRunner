using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where : 제네릭 형식 제약
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Awake() -> 오버라이딩 주의
    //parent, child 호출 순서 확인

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
