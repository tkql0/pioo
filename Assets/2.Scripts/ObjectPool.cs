//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using KeyType = System.String;

//[System.Serializable]
//public class objectPoolData
//{
//    public int initialSize = 4;
//    public int maxSize = 20;

//    public GameObject objectBase;
//    public KeyType key;
//}

//public class ObjectPool : MonoBehaviour
//{
//    private string key;

//    public ObjectPool Clone()
//    {
//        GameObject cloneObject = Instantiate(gameObject);
//        if (!cloneObject.TryGetComponent(out ObjectPool pool))
//            pool = cloneObject.AddComponent<ObjectPool>();
//        cloneObject.SetActive(false);

//        return pool;
//    }
//    public void Activate()
//    {
//        gameObject.SetActive(true);
//    }

//    public void DestroyPool()
//    {
//        gameObject.SetActive(false);
//    }
//}

//public class ObjectPoolManager : MonoBehaviour
//{
//    [SerializeField]
//    List<objectPoolData> objectPoolDataList = new List<objectPoolData>(4);

//    Dictionary<KeyType, ObjectPool> baseDict;
//    Dictionary<KeyType, objectPoolData> dataaDict;
//    Dictionary<KeyType, Stack<ObjectPool>> poolDict;

//    private void Start()
//    {
//        Init();
//    }

//    private void Init()
//    {
//        int len = objectPoolDataList.Count;
//        if (len == 0) return;

//        baseDict = new Dictionary<KeyType, ObjectPool>(len);
//        dataaDict = new Dictionary<KeyType, objectPoolData>(len);
//        poolDict = new Dictionary<KeyType, Stack<ObjectPool>>(len);

//        foreach (var data in objectPoolDataList)
//        {
//            Register(data);
//        }
//    }
//    private void Register(PoolObjectData data)
//    {
//        if (_poolDict.ContainsKey(data.key))
//        {
//            return;
//        }

//        GameObject sample = Instantiate(data.prefab);
//        if (!sample.TryGetComponent(out PoolObject po))
//        {
//            po = sample.AddComponent<PoolObject>();
//            po.key = data.key;
//        }
//        sample.SetActive(false);

//        Stack<PoolObject> pool = new Stack<PoolObject>(data.maxObjectCount);
//        for (int i = 0; i < data.initialObjectCount; i++)
//        {
//            PoolObject clone = po.Clone();
//            pool.Push(clone);
//        }

//        _sampleDict.Add(data.key, po);
//        _dataDict.Add(data.key, data);
//        _poolDict.Add(data.key, pool);
//    }

//    public PoolObject Spawn(KeyType key)
//    {
//        if (!_poolDict.TryGetValue(key, out var pool))
//        {
//            return null;
//        }

//        PoolObject po;

//        if (pool.Count > 0)
//        {
//            po = pool.Pop();
//        }
//        else
//        {
//            po = _sampleDict[key].Clone();
//        }

//        po.Activate();

//        return po;
//    }

//    public void Despawn(PoolObject po)
//    {
//        if (!_poolDict.TryGetValue(po.key, out var pool))
//        {
//            return;
//        }

//        KeyType key = po.key;

//        if (pool.Count < _dataDict[key].maxObjectCount)
//        {
//            pool.Push(po);
//            po.Deactivate();
//        }
//        else
//        {
//            Destroy(po.gameObject);
//        }
//    }
//}