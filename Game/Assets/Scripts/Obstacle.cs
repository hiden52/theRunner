using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHitable
{
    [SerializeField] Vector3[] randomVec3 = new Vector3[3];
    

    private void Awake()
    {
        for (int i = 0; i < randomVec3.Length; i++)
        {
            randomVec3[i] = new Vector3(4 - (i * 4), 0, ObstacleManager.Instance.spawnZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Runner")
        {
            GameManager.Instance.StopStage();

        }
    }

    public void Activate()
    {
        Debug.Log("Activate " + gameObject.name);
        ObstacleManager.Instance.DisableObstacle(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.com)
    }



    void Start()
    {
    }
    private void OnEnable()
    {
        transform.position = randomVec3[Random.Range(0, randomVec3.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
