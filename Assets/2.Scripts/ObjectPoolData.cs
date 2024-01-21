using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolData : MonoBehaviour
{
    private IObjectPool<ObjectPoolData> objectPool;

        public void SetPool(IObjectPool<ObjectPoolData> pool)
    {
        objectPool = pool;
    }

    public void DestroyPool()
    {
        objectPool.Release(this);
    }
}

public class ObjectPool : MonoBehaviour
{
    public SpawnController objectBase;

    private IObjectPool<ObjectPoolData> objectPool;
    private void Awake()
    {
        objectPool = new ObjectPool<ObjectPoolData>(CreatePool, OnGetPool, OnReleasePool, OnDestroyPool, maxSize: 4);
    }
    private ObjectPoolData CreatePool()
    {
        var PoolData = Instantiate(objectBase).GetComponent<ObjectPoolData>();
        PoolData.SetPool(objectPool);
        return PoolData;
    }

    private void OnGetPool(ObjectPoolData objectPoolData)
    {
        objectPoolData.gameObject.SetActive(true);
    }

    private void OnReleasePool(ObjectPoolData objectPoolData)
    {
        objectPoolData.gameObject.SetActive(false);
    }

    private void OnDestroyPool(ObjectPoolData objectPoolData)
    {
        Destroy(objectPoolData.gameObject);
    }
}