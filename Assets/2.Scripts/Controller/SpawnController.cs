using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private SpawnObject spawnGroupObject;

    private const float DeSpawn_Distance = 45f;

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        spawnGroupObject = GameObject.FindObjectOfType<SpawnObject>();
        // 이걸로 게임 매니저에서 new() 대신 설정해야겠네
        // 이름도 서로 바꾸고
        spawnGroupObject.Init();
        spawnGroupObject.StartSpawn();

    }

    public void OnDisable()
    {

    }

    public void Spawn(Vector2 spawnCenterPosition, long enemyCount, long fishCount, long key)
    {
        for (long i = 0; i < enemyCount; i++)
        {
            SpawnObject(spawnCenterPosition, key, ObjectType.Enemy);
        }

        for (long i = 0; i < fishCount; i++)
        {
            SpawnObject(spawnCenterPosition, key, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 target)
    {
        DistanceObjectDeSpawn(target, ObjectType.Enemy);
        DistanceObjectDeSpawn(target, ObjectType.Fish);
    }

    private GameObject SpawnObject(Vector2 spawnCenterPosition, long key, ObjectType character)
    {
        ObjectController _objectController = GameManager.OBJECT;

        switch (character)
        {
            case ObjectType.Enemy:
                foreach (KeyValuePair<long, EnemyCharacter> enemyNumber in _objectController.enemyDataList)
                {
                    if (!_objectController.GetisActive(enemyNumber.Key, character))
                    {
                        _objectController.SetActive(enemyNumber.Key, character, true);
                        _objectController.SetSpawnPosition(enemyNumber.Key, character, spawnCenterPosition);

                        enemyNumber.Value.spawnObjectKey = key;

                        return enemyNumber.Value.enemy;
                    }
                }
                break;
            case ObjectType.Fish:
                foreach (KeyValuePair<long, FishCharacter> fishNumber in _objectController.fishDataList)
                {
                    if (!_objectController.GetisActive(fishNumber.Key, character))
                    {
                        _objectController.SetActive(fishNumber.Key, character, true);
                        _objectController.SetSpawnPosition(fishNumber.Key, character, spawnCenterPosition);

                        fishNumber.Value.spawnObjectKey = key;

                        return fishNumber.Value.fish;
                    }
                }
                break;
        }
        return null;
    }

    private void DistanceObjectDeSpawn(Vector2 target, ObjectType character)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector2 myPosition;

        switch (character)
        {
            case ObjectType.Enemy:
                foreach (KeyValuePair<long, EnemyCharacter> enemyNumber in _objectController.enemyDataList)
                {
                    myPosition = enemyNumber.Value.transform.position;

                    float DistanceX = target.x - myPosition.x;
                    float differenceX = Mathf.Abs(DistanceX);

                    if (differenceX > DeSpawn_Distance)
                    {
                        _objectController.SetActive(enemyNumber.Key, character, false);
                        enemyNumber.Value.spawnObjectKey = 99;
                    }
                }
                break;
            case ObjectType.Fish:
                foreach (KeyValuePair<long, FishCharacter> fishNumber in _objectController.fishDataList)
                {
                    myPosition = fishNumber.Value.transform.position;

                    float DistanceX = target.x - myPosition.x;
                    float differenceX = Mathf.Abs(DistanceX);

                    if (differenceX > DeSpawn_Distance)
                    {
                        _objectController.SetActive(fishNumber.Key, character, false);
                        fishNumber.Value.spawnObjectKey = 99;
                    }
                }
                break;
        }
    }

    public GameObject SpawnWapon(Vector2 spawnObject, ObjectType character)
    {
        ObjectController _objectController = GameManager.OBJECT;

        foreach (KeyValuePair<long, Weapon> weaponNumber in _objectController.weaponDataList)
        {
            switch(character)
            {
                case ObjectType.PlayerWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, character) && weaponNumber.Value.key == character)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(spawnObject.x, spawnObject.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
                case ObjectType.EnemyWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, character) && weaponNumber.Value.key == character)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(spawnObject.x, spawnObject.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
            }
        }
        return null;
    }

    // switch 안에 내용이 똑같은데 합치고 싶다 어떻게 하지 반환형 함수 하나 더 만들어서 해야되나
    // 리스트로 활성화된 오브젝트 내용 담고 전체 개수를 넘기면 활성화 안되게 하거나 더 생성했다가 삭제 할 수 있나

    private void MapSpawn(long InIndex)
    {
        ObjectController _objectController = GameManager.OBJECT;

        if (_objectController.mapDataList.TryGetValue(InIndex, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
    }
}