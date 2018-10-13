using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    public int initialPoolSize = 50;
    public int requestStep = 10;
    public GameObject poolObjectPrefab;

	List<PoolableObject> pool;

    internal void RequestObject(GameObject gameObject)
    {
        
    }
    internal void Free(GameObject gameObject)
    {
        
    }

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject go = Instantiate(poolObjectPrefab);
            go.SetActive(false);
            PoolableObject po = go.GetComponent<PoolableObject>();
            po.parentPool = this;
            pool.Add(po);
        }
    }

}
