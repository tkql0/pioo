using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private long _mapSpawnConut = 0;
    private long _characterSpawnConut = 0;
    private long _weaponSpawnConut = 0;

    [SerializeField]
    private GameObject Prefab_Player;
    [SerializeField]
    private GameObject Prefab_Map;
    [SerializeField]
    private GameObject Prefab_Enemy;
    [SerializeField]
    private GameObject Prefab_Fish;
    [SerializeField]
    private GameObject Prefab_EnemyWapon;
    [SerializeField]
    private GameObject Prefab_PlayerWapon;

    public void ObjectSpawnPool()
    {
        SpawnPool(ObjectType.Player);
        SpawnPool(ObjectType.EnemyWeapon);
        SpawnPool(ObjectType.PlayerWeapon);

        SpawnMapPool();
    }

    public void SpawnMapPool()
    {
        SpawnPool(ObjectType.Map);

        foreach (KeyValuePair<long, Map> outMapData in GameManager.OBJECT.mapDataList)
        {
            SpawnPool(ObjectType.Enemy);
            SpawnPool(ObjectType.Fish);

            outMapData.Value.mySpawnNumber = outMapData.Key;
        }
    }

    private void SpawnPool(ObjectType InObjectType)
    {
        int maxSize = 0;

        switch (InObjectType)
        {
            case ObjectType.Player:
                GameObject playerObject = Instantiate(Prefab_Player);

                GameManager.OBJECT.player = playerObject.GetComponent<Player>();
                _characterSpawnConut++;
                break;
            case ObjectType.Enemy:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysObject = Instantiate(Prefab_Enemy, transform);

                    Character character = EnemysObject.GetComponent<Character>();
                    if (GameManager.OBJECT.SetCharacterInfo(character, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    EnemysObject.SetActive(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 50;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject FishsObject = Instantiate(Prefab_Fish, transform);

                    Character character = FishsObject.GetComponent<Character>();
                    if (GameManager.OBJECT.SetCharacterInfo(character, InObjectType, _characterSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }

                    _characterSpawnConut++;
                    FishsObject.SetActive(false);
                }
                break;
            case ObjectType.Map:
                maxSize = 3;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject MapsObject = Instantiate(Prefab_Map);

                    GameManager.OBJECT.mapDataList.Add(_mapSpawnConut, MapsObject.GetComponent<Map>());
                    _mapSpawnConut++;
                }
                break;
            case ObjectType.EnemyWeapon:
            case ObjectType.PlayerWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject AttackObject = Instantiate(Prefab_PlayerWapon, transform);

                    Weapon weapon = AttackObject.GetComponent<Weapon>();
                    if (GameManager.OBJECT.SetWeaponInfo(weapon, InObjectType, _weaponSpawnConut) == false)
                    {
                        Debug.Log("Null");
                    }
                    _weaponSpawnConut++;

                    AttackObject.SetActive(false);
                }
                break;
        }
    }
}