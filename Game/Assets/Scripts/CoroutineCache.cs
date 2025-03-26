using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineCache
{
    // WaitForSeconds�� ĳ���ϰ� ��ųʸ��� �̿��� ������ ��ġ
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
