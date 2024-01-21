using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using KeyType = System.String;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolObjectData
    {
        public int initialSize = 4;
        public int maxSize = 20;

        public GameObject objectBase;
        public KeyType key;
    }
    [SerializeField]
    private PoolObjectData[] objectInfos = null;

    private string key;

    Dictionary<KeyType, IObjectPool<GameObject>> ojbectPoolDic = new Dictionary<KeyType, IObjectPool<GameObject>>();

    Dictionary<KeyType, GameObject> saveObjectDic = new Dictionary<KeyType, GameObject>();

    private IObjectPool<GameObject> objectPool;
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < objectInfos.Length; i++)
        {
            objectPool = new ObjectPool<GameObject>(CreatePool, OnGetPool, OnReleasePool, OnDestroyPool,
                true, objectInfos[i].initialSize, objectInfos[i].maxSize);

            if (saveObjectDic.ContainsKey(objectInfos[i].key))
            {
                Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", objectInfos[i].key);
                return;
            }
            saveObjectDic.Add(objectInfos[i].key, objectInfos[i].objectBase);
            ojbectPoolDic.Add(objectInfos[i].key, objectPool);

            for (int j = 0; j < objectInfos[i].maxSize; j++)
            {
                key = objectInfos[i].key;
                ObjectPoolData poolData = CreatePool().GetComponent<ObjectPoolData>();
                poolData.objectPool.Release(poolData.gameObject);
            }
        }

    }

    private GameObject CreatePool()
    {
        GameObject _poolData = Instantiate(saveObjectDic[key]);
        _poolData.GetComponent<ObjectPoolData>().objectPool = ojbectPoolDic[key];
        return _poolData;
    }

    private void OnGetPool(GameObject _poolData)
    {
        _poolData.SetActive(true);
    }

    private void OnReleasePool(GameObject _poolData)
    {
        _poolData.SetActive(false);
    }

    private void OnDestroyPool(GameObject _poolData)
    {
        Destroy(_poolData);
    }

    public GameObject GetPool(KeyType _key)
    {
        key = _key;

        if (saveObjectDic.ContainsKey(_key) == false)
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", _key);
            return null;
        }

        return ojbectPoolDic[_key].Get();
    }
}

public class ObjectPoolData : MonoBehaviour
{
    public IObjectPool<GameObject> objectPool;

    public void DestroyPool()
    {
        objectPool.Release(gameObject);
    }
}

