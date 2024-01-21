using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameTree : MonoSingleTon<GameTree>
{
    static public GameManager GAME { get; private set; }
    static public MapManager MAP { get; private set; }
    static public UIManager UI { get; private set; }

    private void Awake()
    {
        GAME = new();
        MAP = new();
        UI = new();

        GAME.obhectController = gameObject.AddComponent<ObjectController>();
        GAME.spawnController = gameObject.AddComponent<SpawnController>();

        //Init();
    }

    private void OnEnable()
    {
        GAME.Init();
        MAP.Init();
        UI.Init();
    }

    private void OnDisable()
    {

    }
}
//    [System.Serializable]
//    private class ObjectInfo
//    {
//        public string objectName;
//        public GameObject objectBase;
//        public int maxSize;
//    }
//    public bool IsReady { get; private set; }

//    [SerializeField]
//    private ObjectInfo[] objectInfos = null;

//    private string objectName;

//    private Dictionary<string, IObjectPool<GameObject>> ojbectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();

//    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();

//    private void Init()
//    {
//        IsReady = false;

//        for (int idx = 0; idx < objectInfos.Length; idx++)
//        {
//            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
//            OnDestroyPoolObject, true, objectInfos[idx].maxSize, objectInfos[idx].maxSize);

//            if (goDic.ContainsKey(objectInfos[idx].objectName))
//            {
//                Debug.LogFormat("{0} �̹� ��ϵ� ������Ʈ�Դϴ�.", objectInfos[idx].objectName);
//                return;
//            }

//            goDic.Add(objectInfos[idx].objectName, objectInfos[idx].objectBase);
//            ojbectPoolDic.Add(objectInfos[idx].objectName, pool);

//            // �̸� ������Ʈ ���� �س���
//            for (int i = 0; i < objectInfos[idx].maxSize; i++)
//            {
//                objectName = objectInfos[idx].objectName;
//                PoolAble poolAbleGo = CreatePooledItem().GetComponent<PoolAble>();
//                poolAbleGo.Pool.Release(poolAbleGo.gameObject);
//            }
//        }

//        Debug.Log("������ƮǮ�� �غ� �Ϸ�");
//        IsReady = true;
//    }
//    // ����
//    private GameObject CreatePooledItem()
//    {
//        GameObject poolGo = Instantiate(goDic[objectName]);
//        poolGo.GetComponent<PoolAble>().Pool = ojbectPoolDic[objectName];
//        return poolGo;
//    }

//    // �뿩
//    private void OnTakeFromPool(GameObject poolGo)
//    {
//        poolGo.SetActive(true);
//    }

//    // ��ȯ
//    private void OnReturnedToPool(GameObject poolGo)
//    {
//        poolGo.SetActive(false);
//    }

//    // ����
//    private void OnDestroyPoolObject(GameObject poolGo)
//    {
//        Destroy(poolGo);
//    }

//    public GameObject GetGo(string goName)
//    {
//        objectName = goName;

//        if (goDic.ContainsKey(goName) == false)
//        {
//            Debug.LogFormat("{0} ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", goName);
//            return null;
//        }

//        return ojbectPoolDic[goName].Get();
//    }
//}

//public class PoolAble : MonoBehaviour
//{
//    public IObjectPool<GameObject> Pool { get; set; }

//    public void ReleaseObject()
//    {
//        Pool.Release(gameObject);
//    }
//}