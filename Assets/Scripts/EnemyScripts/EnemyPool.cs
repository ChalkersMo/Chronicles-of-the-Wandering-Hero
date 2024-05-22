using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject prefab;

    public int poolSize = 10;

    private List<GameObject> objectPool;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(0, 5),
                transform.position.y + Random.Range(0, 6),
                transform.position.z + Random.Range(0, 7));

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        objectPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
    }
}
