using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private long _mapSpawnConut;
    //private long _enemySpawnConut;
    //private long _fishSpawnConut;
    private long _characterSpawnConut;
    private long _weaponSpawnConut;

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 2;

    private const float DeSpawn_Distance = 60f;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";
    private const string Prefab_EnemyWapon = "Prefabs/EnemyAttack";
    private const string Prefab_PlayerWapon = "Prefabs/PlayerAttack";

    public void Init()
    {
        //_fishSpawnConut = 0;
        //_enemySpawnConut = 0;
        _characterSpawnConut = 0;
        _mapSpawnConut = 0;
       _weaponSpawnConut = 0;
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void StartSpawn()
    {
        ObjectController _objectController = GameManager.OBJECT;

        SpawnPool(ObjectType.Player);

        Vector2 targetPosition = _objectController.player.transform.position;

        SpawnMap(targetPosition);
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
                break;
            case ObjectType.Enemy:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
                    GameObject EnemysObjects = Instantiate(EnemysObject, transform);

                    _objectController.characterList.Add(_characterSpawnConut,
                        EnemysObjects.GetComponent<EnemyCharacter>());
                    _objectController.characterList[_characterSpawnConut].key = ObjectType.Enemy;
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

                    _objectController.characterList.Add(_characterSpawnConut,
                        FishsObjects.GetComponent<FishCharacter>());
                    _objectController.characterList[_characterSpawnConut].key = ObjectType.Fish;
                    _characterSpawnConut++;
                    FishsObjects.SetActive(false);
                }
                break;
            case ObjectType.Map:
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

    private void SpawnMap(Vector2 inSpawnPosition)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector2(inSpawnPosition.x + (i * 40), 0), Quaternion.identity);
            _objectController.mapDataList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }

    public GameObject Spawn(Vector2 inSpawnPosition, long key, ObjectType objectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterList)
        {
            switch (objectType)
            {
                case ObjectType.Enemy:
                    if (!_objectController.GetisActive(chatacterNumber.Key, objectType) && chatacterNumber.Value.key == objectType)
                    {
                        _objectController.SetActive(chatacterNumber.Key, objectType, true);
                        _objectController.SetSpawnPosition(chatacterNumber.Key, objectType, inSpawnPosition);

                        chatacterNumber.Value.spawnObjectKey = key;

                        return chatacterNumber.Value.characterObject;
                    }
                    break;
                case ObjectType.Fish:
                    if (!_objectController.GetisActive(chatacterNumber.Key, objectType) && chatacterNumber.Value.key == objectType)
                    {
                        _objectController.SetActive(chatacterNumber.Key, objectType, true);
                        _objectController.SetSpawnPosition(chatacterNumber.Key, objectType, inSpawnPosition);

                        chatacterNumber.Value.spawnObjectKey = key;

                        return chatacterNumber.Value.characterObject;
                    }
                    break;
            }
        }
        return null;
    }
    // 여기로 SpawnWeapon() 옮겨야겠다

    public void DistanceObjectDeSpawn(Vector2 target, ObjectType objectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector2 myPosition;

        float DistanceX = 0;
        float differenceX = 0;

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterList)
        {
            myPosition = chatacterNumber.Value.transform.position;

            DistanceX = target.x - myPosition.x;
            differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.SetActive(chatacterNumber.Key, objectType, false);
                chatacterNumber.Value.spawnObjectKey = 99;
            }
        }
    }

    public GameObject SpawnWeapon(Vector2 inSpawnPosition, ObjectType objectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        foreach (KeyValuePair<long, Weapon> weaponNumber in _objectController.weaponDataList)
        {
            switch (objectType)
            {
                case ObjectType.PlayerWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, objectType) && weaponNumber.Value.key == objectType)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(inSpawnPosition.x, inSpawnPosition.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
                case ObjectType.EnemyWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, objectType) && weaponNumber.Value.key == objectType)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(inSpawnPosition.x, inSpawnPosition.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
            }
        }
        return null;
    }
    // switch 안에 내용이 똑같은데 합치고 싶다 어떻게 하지 반환형 함수 하나 더 만들어서 해야되나
    // 리스트로 활성화된 오브젝트 내용 담고 전체 개수를 넘기면 활성화 안되게 하거나 더 생성했다가 삭제 할 수 있나
}