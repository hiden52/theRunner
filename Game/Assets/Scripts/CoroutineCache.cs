using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineCache
{
    // WaitForSeconds를 캐싱하고 딕셔너리를 이용해 빠르게 서치
    static Dictionary<float, WaitForSeconds> timeDic = new Dictionary<float, WaitForSeconds>();
        

    public static WaitForSeconds WaitForSeconds(float time)
    {
        WaitForSeconds result;

        if (!timeDic.TryGetValue(time, out result))
        {
            result = new WaitForSeconds(time);
            timeDic.Add(time, result);
        }

        return result;
    }

}
