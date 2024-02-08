using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private long _mapSpawnConut = 0;
    private long _characterSpawnConut = 0;
    private long _weaponSpawnConut = 0;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";
    private const string Prefab_EnemyWapon = "Prefabs/EnemyAttack";
    private const string Prefab_PlayerWapon = "Prefabs/PlayerAttack";

    public void OnEnable()
    {
        ObjectSpawnPool();
    }

    public void OnDisable()
    {

    }

    public void ObjectSpawnPool()
    {
        ObjectController _objectController = GameManager.OBJECT;

        SpawnPool(ObjectType.Player);
        SpawnPool(ObjectType.Map);
        SpawnPool(ObjectType.EnemyWeapon);
        SpawnPool(ObjectType.PlayerWeapon);

        foreach (KeyValuePair<long, Map> mapNumber in _objectController.mapDataList)
        {
            SpawnPool(ObjectType.Enemy);
            SpawnPool(ObjectType.Fish);

            mapNumber.Value.key = mapNumber.Key;
        }
    }

    private void SpawnPool(ObjectType objectType)
    {
        int maxSize = 0;

        ObjectController _objectController = GameManager.OBJECT;

        switch (objectType)
        {
            case ObjectType.Player:
                GameObject playerObject = Resources.Load<GameObject>(Prefab_Player);
                GameObject playersObjects = Instantiate(playerObject);

                _objectController.player = playersObjects.GetComponent<Player>();
                _characterSpawnConut++;
                break;
            case ObjectType.Enemy:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
                    GameObject EnemysObjects = Instantiate(EnemysObject, transform);

                    _objectController.characterDataList.Add(_characterSpawnConut,
                        EnemysObjects.GetComponent<EnemyCharacter>());
                    _objectController.characterDataList[_characterSpawnConut].key = ObjectType.Enemy;
                    _characterSpawnConut++;
                    EnemysObjects.SetActive(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 50;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
                    GameObject FishsObjects = Instantiate(FishsObject, transform);

                    _objectController.characterDataList.Add(_characterSpawnConut,
                        FishsObjects.GetComponent<FishCharacter>());
                    _objectController.characterDataList[_characterSpawnConut].key = ObjectType.Fish;
                    _characterSpawnConut++;
                    FishsObjects.SetActive(false);
                }
                break;
            case ObjectType.Map:
                maxSize = 3;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
                    GameObject MapsObjects = Instantiate(MapsObject);

                    _objectController.mapDataList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
                    _mapSpawnConut++;
                }
                break;
            case ObjectType.EnemyWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
                    GameObject EnemysAttackObject = Instantiate(EnemyAttackObject, transform);

                    _objectController.weaponDataList.Add(_weaponSpawnConut,
                        EnemysAttackObject.GetComponent<Weapon>());
                    _objectController.weaponDataList[_weaponSpawnConut].key = ObjectType.EnemyWeapon;
                    _weaponSpawnConut++;
                    EnemysAttackObject.SetActive(false);
                }
                break;
            case ObjectType.PlayerWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
                    GameObject PlayersAttackObject = Instantiate(PlayerAttackObject, transform);

                    _objectController.weaponDataList.Add(_weaponSpawnConut,
                        PlayersAttackObject.GetComponent<Weapon>());
                    _objectController.weaponDataList[_weaponSpawnConut].key = ObjectType.PlayerWeapon;
                    _weaponSpawnConut++;
                    PlayersAttackObject.SetActive(false);
                }
                break;
        }
    }
}