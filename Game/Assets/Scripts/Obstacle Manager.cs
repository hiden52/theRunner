using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] GameObject[] prefabs;
    //[SerializeField] List<string> prefabNames = new List<string>();     // 하나하나 이름을 써넣는 건 귀찮기 때문에 Resource.LoadAll을 이용하는게 편한듯 
    [SerializeField] int countOfObstacles = 5;
    [SerializeField] float speed = 10f;
    [SerializeField] public float spawnZ = -30f;


    private void Start()
    {
        obstacles.Capacity = 10;

        Create();

        StartCoroutine(ActiveObstacle());
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, speed * Time.fixedDeltaTime);
    }

    private void Create()
    {
        prefabs = Resources.LoadAll<GameObject>("Obtacles");
        
        for (int i = 0; i < countOfObstacles; i++)
        {
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            obj.SetActive(false);
         
            obstacles.Add(obj);
        }

        #region 이전코드
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject obj = Instantiate(Resources.Load<GameObject>(prefabNames[Random.Range(0, prefabNames.Count)]));
        //    obj.SetActive(false);
        //    obstacles.Add(obj);
        //}
        #endregion
    }

    IEnumerator ActiveObstacle()
    {
        int count = 0;
        int idx = Random.Range(0, obstacles.Count);

        while (count < countOfObstacles)
        {
            if (obstacles[idx].activeSelf)
            {
                //Debug.Log("Aleady Activated");
                idx = (idx + 1) % obstacles.Count;
            }
            else
            {
                //Debug.Log("Activate Obstacle " + obstacles[idx].name);
                count++;
                obstacles[idx].SetActive(true);

                idx = Random.Range(0, obstacles.Count);

                yield return new WaitForSeconds(2.5f);
            }
        }
    }


}
