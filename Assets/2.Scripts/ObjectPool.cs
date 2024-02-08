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
                GameObject playerObject = Instantiate(Prefab_Player);

                _objectController.player = playerObject.GetComponent<Player>();
                _characterSpawnConut++;
                break;
            case ObjectType.Enemy:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysObject = Instantiate(Prefab_Enemy, transform);

                    _objectController.characterDataList.Add(_characterSpawnConut,
                        EnemysObject.GetComponent<EnemyCharacter>());
                    _objectController.characterDataList[_characterSpawnConut].key = ObjectType.Enemy;
                    _characterSpawnConut++;
                    EnemysObject.SetActive(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 50;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject FishsObject = Instantiate(Prefab_Fish, transform);

                    _objectController.characterDataList.Add(_characterSpawnConut,
                        FishsObject.GetComponent<FishCharacter>());
                    _objectController.characterDataList[_characterSpawnConut].key = ObjectType.Fish;
                    _characterSpawnConut++;
                    FishsObject.SetActive(false);
                }
                break;
            case ObjectType.Map:
                maxSize = 3;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject MapsObject = Instantiate(Prefab_Map);

                    _objectController.mapDataList.Add(_mapSpawnConut, MapsObject.GetComponent<Map>());
                    _mapSpawnConut++;
                }
                break;
            case ObjectType.EnemyWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysAttackObject = Instantiate(Prefab_EnemyWapon, transform);

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
                    GameObject PlayersAttackObject = Instantiate(Prefab_PlayerWapon, transform);

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