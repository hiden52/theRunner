using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject go = Resources.Load<GameObject>(path);

        if(go == null)
        {
            Debug.Log("Invalid Prefab path : " + path);
            return null;
        }

        GameObject clone = Object.Instantiate(go, parent);
        clone.name = clone.name.Substring(0, clone.name.Length - 7);

        return clone;
    }

    public GameObject Instantiate(GameObject go, Transform parent = null)
    {

        if (go == null)
        {
            Debug.Log("Invalid Prefab");
            return null;
        }

        GameObject clone = Object.Instantiate(go, parent);
        clone.name = clone.name.Substring(0, clone.name.Length - 7);

        return clone;
    }

}
