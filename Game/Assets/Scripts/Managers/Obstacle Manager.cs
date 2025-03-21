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
    //[SerializeField] float speed = 10f;
    [SerializeField] public float spawnZ = -30f;

    int cap = 10;

    private void Start()
    {
        obstacles.Capacity = 10;
        countActivated = 0;
        Create();

        StartCoroutine(ActiveObstacle());
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.Playing)
        { 
            transform.position += new Vector3(0, 0, GameManager.Instance.speed * Time.fixedDeltaTime);
        }
    }

    private void Create()
    {
        if (prefabs != null)
        {
            prefabs = Resources.LoadAll<GameObject>("Obtacles");
        }

        for (int i = 0; i < defaultNumObst; i++)
        {
            CreateObs();
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

    private void CreateObs()
    {
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        obj.SetActive(false);

        obstacles.Add(obj);
    }

    IEnumerator ActiveObstacle()
    {
        int idx = Random.Range(0, obstacles.Count);

        while (true)
        {
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
                obstacles[idx].SetActive(true);
                countActivated++;

                idx = Random.Range(0, obstacles.Count);

                yield return new WaitForSeconds(2.5f);
            }
        }
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
