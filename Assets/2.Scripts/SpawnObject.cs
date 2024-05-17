using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private List<CharacterData> characterDatas;

    private long _mapSpawnConut = 0;
    private long _characterSpawnConut = 0;
    private long _weaponSpawnConut = 0;
    private long _itemSpawnConut = 0;

    public void ObjectSpawnPool()
    {
        SpawnObjectPool(ObjectType.Player);
        SpawnObjectPool(ObjectType.EnemyWeapon);
        SpawnObjectPool(ObjectType.PlayerWeapon);
        SpawnObjectPool(ObjectType.Item_Fish);
    }

    public void SpawnMapPool()
    {
        SpawnObjectPool(ObjectType.Map);

        foreach (KeyValuePair<long, Map> outMapData in GameManager.OBJECT.mapDataList)
        {
            SpawnObjectPool(ObjectType.Enemy);
            SpawnObjectPool(ObjectType.Fish);
        }
    }

    private void SpawnObjectPool(ObjectType InObjectType)
    {
        int maxSize = 0;

        switch (InObjectType)
        {
            case ObjectType.Player:
                var playerObject = Instantiate(GetPrefabObject(Prefab_Player)).GetComponent<Player>();

                GameManager.OBJECT.player = playerObject;

                playerObject.mySpawnNumber = 1;

                _characterSpawnConut++;
                break;
            case ObjectType.Enemy:
                maxSize = 10;

                for (int i = 0; i < maxSize; i++)
                {
                    var EnemyObject = Instantiate(GetPrefabObject(Prefab_Enemy), transform).GetComponent<Character>();
                    EnemyObject.characterData = characterDatas[(int)InObjectType];

                    if (GameManager.OBJECT.SetCharacterInfo(EnemyObject, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    EnemyObject.SetActiveObject(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    var FishObject = Instantiate(GetPrefabObject(Prefab_Fish), transform).GetComponent<Character>();
                    FishObject.characterData = characterDatas[(int)InObjectType];

                    if (GameManager.OBJECT.SetCharacterInfo(FishObject, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    FishObject.SetActiveObject(false);
                }
                break;
            case ObjectType.Map:
                maxSize = 3;

                for (int i = 0; i < maxSize; i++)
                {
                    var MapObject = Instantiate(GetPrefabObject(Prefab_Map)).GetComponent<Map>();

                    GameManager.OBJECT.mapDataList.Add(_mapSpawnConut, MapObject);
                    _mapSpawnConut++;
                }
                break;
            case ObjectType.EnemyWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    var AttackObject = Instantiate(GetPrefabObject(Prefab_EnemyWeapon),
                        transform).GetComponent<Weapon>();

                    if (GameManager.OBJECT.SetWeaponInfo(AttackObject, InObjectType, _weaponSpawnConut) 
                        == false)
                    {
                        Debug.Log("Null");
                    }
                    _weaponSpawnConut++;
                    AttackObject.SetActiveObject(false);
                }
                break;
            case ObjectType.PlayerWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    var AttackObject = Instantiate(GetPrefabObject(Prefab_PlayerWeapon),
                        transform).GetComponent<Weapon>();

                    if (GameManager.OBJECT.SetWeaponInfo(AttackObject, InObjectType, _weaponSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }
                    _weaponSpawnConut++;

                    AttackObject.SetActiveObject(false);
                }
                break;
            case ObjectType.Item_Fish:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    var itemFish = Instantiate(GetPrefabObject(Prefab_Drop_Fish).GetComponent<Item>());

                    GameManager.OBJECT.itemDataList.Add(_itemSpawnConut, itemFish);
                    _itemSpawnConut++;

                    itemFish.SetActiveObject(false);
                }
                break;
        }
    }

    //public long itemMaxSize;

    //private void SpawnItemPool(ItemType InItemType)
    //{ // 현재 있는 물고기뿐만 아니라 나중에 지상 인류와 거래할 수 있는 화폐나
    //    // 물건을 만들 재료들을 더 추가 할거니까 swith로 해야지
    //    switch(InItemType)
    //    {
    //        case ItemType.Item_Fish:
    //            itemMaxSize = 20;

    //            for (int i = 0; i < itemMaxSize; i++)
    //            {
    //                var itemFish = Instantiate(GetPrefabObject(Prefab_Drop_Fish).GetComponent<Item>());

    //                GameManager.OBJECT.itemDataList.Add(_itemSpawnConut, itemFish);
    //                _itemSpawnConut++;

    //                itemFish.SetActiveObject(false);
    //            }
    //            break;
    //    }
    //}

    public GameObject GetPrefabObject(string InPrefabObject)
    {
        GameObject prefabObject = Resources.Load<GameObject>(InPrefabObject);

        return prefabObject;
    }

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";
    private const string Prefab_EnemyWeapon = "Prefabs/EnemyAttack";
    private const string Prefab_PlayerWeapon = "Prefabs/PlayerAttack";
    private const string Prefab_Drop_Fish = "Prefabs/FishItem";

    // 오브젝트 풀을 사용할 때 스택에 넣어서 스택이 비었을 때 더 생성하고 스택 최고치보다 스택의 수가 많으면
    // for로 스택 최고치 만큼 지워주는 걸로 바꿔야겠다
    // 그럼 맵마다 몬스터에 키를 주는 걸 바꾸거나 맵마다 오브젝트 풀을 갖고 있는 방식 중 골라서 해야겠네
    // 근데 그러면 여러 오브젝트를 한곳에 넣는건 안좋으려나 나중에 알아봐야겠다
}