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
        }
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
}