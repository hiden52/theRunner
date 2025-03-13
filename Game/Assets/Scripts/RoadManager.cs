using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadManager : MonoBehaviour
{
    [SerializeField] List<GameObject> roads;
    [SerializeField] float speed;
    [SerializeField] int currentRoad;
    [SerializeField] float distance;
    int roadCount;
    
    void Start()
    {
        roadCount = transform.childCount;
        roads.Capacity = (roadCount);
        for(int i = 0; i < roadCount; i++)
        {
            roads.Add(transform.GetChild(i).gameObject);
        }

        currentRoad = 0;
        distance = 0;
    }

    void FixedUpdate()
    {
        distance += speed * Time.fixedDeltaTime;
        transform.position += new Vector3(0, 0, speed * Time.fixedDeltaTime);

        if((Mathf.Floor(distance / 40) % 4) != currentRoad)
        {
            Debug.Log(distance / 40);
            roads[currentRoad].transform.position += new Vector3(0, 0, -160);
            currentRoad++;
            currentRoad %= roads.Count;
        }
    }
}
