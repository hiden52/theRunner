using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadManager : MonoBehaviour
{
    [SerializeField] List<GameObject> roads;
    //[SerializeField] float speed;
    [SerializeField] int currentRoad;
    [SerializeField] int lastestRaod;
    [SerializeField] float distance;
    int offset = -40;
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
        lastestRaod = roadCount - 1;
        distance = 0;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.Playing)
        {
            distance += GameManager.Instance.speed * Time.fixedDeltaTime;
            transform.position += new Vector3(0, 0, GameManager.Instance.speed * Time.fixedDeltaTime);
        }

        //if((Mathf.Floor(distance / 40) % 4) != currentRoad)
        //{
        //    roads[currentRoad].transform.position += new Vector3(0, 0, -160);
        //    currentRoad++;
        //    currentRoad %= roads.Count;
        //}
    }
    public void InitializePosition()
    {
        roads[currentRoad].transform.position = roads[lastestRaod].transform.position + new Vector3(0, 0, offset);
        currentRoad = (currentRoad + 1) % roads.Count;
        lastestRaod = (lastestRaod + 1) % roads.Count;
    }

    

}
