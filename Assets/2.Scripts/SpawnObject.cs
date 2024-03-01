using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private long _mapSpawnConut = 0;
    private long _characterSpawnConut = 0;
    private long _weaponSpawnConut = 0;
    private long _testMapSpawnConut = 0;
    private long _testMapConut = 0;

    public void ObjectSpawnPool()
    {
        SpawnObjectPool(ObjectType.Player);
        SpawnObjectPool(ObjectType.EnemyWeapon);
        SpawnObjectPool(ObjectType.PlayerWeapon);
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
                GameObject playerObject = Instantiate(GetPrefabObject(Prefab_Player));

                Player player = playerObject.GetComponent<Player>();
                GameManager.OBJECT.player = player;

                player.mySpawnNumber = 1;

                _characterSpawnConut++;
                break;
            case ObjectType.Enemy:
                maxSize = 10;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemyObject = Instantiate(GetPrefabObject(Prefab_Enemy), transform);

                    Character character = EnemyObject.GetComponent<Character>();
                    if (GameManager.OBJECT.SetCharacterInfo(character, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    character.SetActiveObject(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject FishObject = Instantiate(GetPrefabObject(Prefab_Fish), transform);

                    Character character = FishObject.GetComponent<Character>();
                    if (GameManager.OBJECT.SetCharacterInfo(character, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    character.SetActiveObject(false);
                }
                break;
            case ObjectType.Map:
                maxSize = 3;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject MapObject = Instantiate(GetPrefabObject(Prefab_Map));

                    Map map = MapObject.GetComponent<Map>();
                    GameManager.OBJECT.mapDataList.Add(_mapSpawnConut, map);
                    _mapSpawnConut++;
                }

                int testMaxSize = 3;
                for (int i = 0; i < testMaxSize; i++)
                {
                    GameObject SpawnMapObject = Instantiate(GetPrefabObject(Prefab_MapSpawnObject));
                    SpawnPositionObject spawnPositionObject =
                        SpawnMapObject.GetComponent<SpawnPositionObject>();
                    GameManager.OBJECT.testSpawmPositionDataList.Add
                        (_testMapSpawnConut, spawnPositionObject);
                    _testMapSpawnConut++;

                    MapSpawnList(GetPrefabObject(Prefab_RandomMap));
                    MapSpawnList(GetPrefabObject(Prefab_PlayerSettingMap));
                    MapSpawnList(GetPrefabObject(Prefab_EventMap));
                }
                break;
            case ObjectType.EnemyWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject AttackObject = Instantiate(GetPrefabObject(Prefab_EnemyWeapon),
                        transform);

                    Weapon weapon = AttackObject.GetComponent<Weapon>();

                    if (GameManager.OBJECT.SetWeaponInfo(weapon, InObjectType, _weaponSpawnConut) 
                        == false)
                    {
                        Debug.Log("Null");
                    }
                    _weaponSpawnConut++;
                    weapon.SetActiveObject(false);
                }
                break;
            case ObjectType.PlayerWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject AttackObject = Instantiate(GetPrefabObject(Prefab_PlayerWeapon), transform);

                    Weapon weapon = AttackObject.GetComponent<Weapon>();

                    if (GameManager.OBJECT.SetWeaponInfo(weapon, InObjectType, _weaponSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }
                    _weaponSpawnConut++;

                    weapon.SetActiveObject(false);
                }
                break;
        }
    }

    public void MapSpawnList(GameObject InMapObject)
    {
        GameObject mapObject = Instantiate(InMapObject, transform);

        TestMap testMap = mapObject.GetComponent<TestMap>();

        GameManager.OBJECT.testMapDataList.Add(_testMapConut, testMap);

        mapObject.SetActive(false);

        _testMapConut++;
    }

    //public long itemMaxSize;
    //private void SpawnItemPool(ItemType InItemType)
    //{
    //    switch(InItemType)
    //    {
    //        case ItemType.Item_Fish:
    //            var itemFish = Instantiate(GetPrefabObject(Prefab_Drop_Fish).GetComponent<Item>());
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

    private const string Prefab_MapSpawnObject = "Prefabs/SpawnMap";
    private const string Prefab_RandomMap = "Prefabs/RandomMap";
    private const string Prefab_PlayerSettingMap = "Prefabs/PlayerSettingMap";
    private const string Prefab_EventMap = "Prefabs/EventMap";
}