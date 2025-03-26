using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] GameObject[] prefabs;
    //[SerializeField] List<string> prefabNames = new List<string>();     // 하나하나 이름을 써넣는 건 귀찮기 때문에 Resource.LoadAll을 이용하는게 편한듯 
    [SerializeField] int defaultNumObst = 5;
    [SerializeField] int countActivated;
    [SerializeField] public float spawnZ = -120f;
    //[SerializeField] public float respawnTime = 2.5f;

    int cap = 10;

    private void Start()
    {
        obstacles.Capacity = 10;
        countActivated = 0;
        Create();

        ActivateObstacles();
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.Playing)
        { 
            transform.position += new Vector3(0, 0, SpeedManager.Instance.Speed * Time.fixedDeltaTime);
        }
    }

    private void Create()
    {
        if (prefabs != null)
        {
            prefabs = ResourcesManager.Instance.LoadAll<GameObject>("Obtacles");
        }

        for (int i = 0; i < defaultNumObst; i++)
        {
            CreateObs();
        }
        countActivated = 0;

        #region 이전코드
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject obj = Instantiate(Resources.Load<GameObject>(prefabNames[Random.Range(0, prefabNames.Count)]));
        //    obj.SetActive(false);
        //    obstacles.Add(obj);
        //}
        #endregion
    }
    public void ResetObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            Debug.Log("Destroy " + obstacle.name);
            Destroy(obstacle);
        }
        obstacles.Clear();

        Create();
    }

    private void CreateObs()
    {
        GameObject obj = ResourcesManager.Instance.Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        obj.SetActive(false);

        obstacles.Add(obj);
    }

    IEnumerator ActiveObstacle()
    {
        int idx = Random.Range(0, obstacles.Count);

        while (true)
        {
            if (!GameManager.Instance.Playing)
            {
                yield break;
            }

            if (obstacles[idx].activeSelf)
            {
                if (IsAllActivated())
                {
                    if (obstacles.Capacity <= obstacles.Count)
                    {
                        obstacles.Capacity += cap;
                    }

                    CreateObs();
                }

                idx = (idx + 1) % obstacles.Count;
            }
            else
            {
                if (GameManager.Instance.Playing)
                {
                    obstacles[idx].SetActive(true);
                    countActivated++;

                    idx = Random.Range(0, obstacles.Count);
                }
                // WaitForRoutine이 계속 생성됨
                // yield return new WaitForSeconds(GameManager.Instance.RespawnTime);

                yield return CoroutineCache.WaitForSeconds(TimeManager.Instance.ActiveTime);
            }
        }
    }

    public void ActivateObstacles()
    {
        StartCoroutine(ActiveObstacle());
    }
    public void DisableObstacle(GameObject obs)
    {
        obs.SetActive(false);
        countActivated--;
    }

    private bool IsAllActivated()
    {
        if (countActivated < obstacles.Count)
            return false;
        else
            return true;
    }

    


}
