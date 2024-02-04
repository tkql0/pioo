using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    NULL,
    Player,
    Enemy,
    Fish,
    Weapon,
}

public class ObjectController
{
    //public Dictionary<long, AttackableCharacter> characterList
    //    = new Dictionary<long, AttackableCharacter>();

    public Player player;
    public Dictionary<long, EnemyCharater> enemyDataList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Weapon> playerWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<long, Weapon> enemyWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<int, Map> mapDataList = new Dictionary<int, Map>();
    //public Dictionary<long, Weapon> WaponDataList = new Dictionary<long, Weapon>();

    private const int Position_X_Min = -20;
    private const int Position_X_Max = 21;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -40;

    public void OnEnable()
    {
        //characterList.Add(Type.Player, Player);
        //characterList.Add(Type.Enemy, new EnemyCharater());

        //characterList[Type.Player].OnDamage();
        //characterList[Type.Enemy].OnDamage();

        for (int i = 0; i < mapDataList.Count; i++)
        {
            MapSpawn(i);
        }
    }

    public void OnDisable()
    {

    }

    public void Update()
    {
            
    }

    private void MapSpawn(int InCharacterld)
    {
        if (mapDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
        {
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
        }
    }

    public void SetActive(int InIndex, CharacterType character, bool InIsActive)
    {
        switch (character)
        {
            case CharacterType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                outEnemyData.SetActiveObject(InIsActive);
                break;
            case CharacterType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                outFishData.SetActiveObject(InIsActive);
                break;
        }
    }

    public bool GetisActive(int InIndex, CharacterType character)
    {
        bool active = false;

        if (character == CharacterType.Enemy)
            active = enemyDataList[InIndex].enemy.activeSelf;
        else if (character == CharacterType.Fish)
            active = fishDataList[InIndex].fish.activeSelf;

        return active;
    }

    public void SetSpawnPosition(int InIndex, CharacterType character, Vector3 centerPosition)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        switch (character)
        {
            case CharacterType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                outEnemyData.transform.position =
                    new Vector3(randomPositionX + centerPosition.x, 0, 0);
                break;
            case CharacterType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                outFishData.transform.position =
                    new Vector3(randomPositionX + centerPosition.x, randomPositionY, 0);
                break;
        }
    }

    public Vector3 SetNewPosition(int InIndex, CharacterType character,Vector3 myPosition, float randomPosition, Vector3 getCenterPosition)
    {
        if (character == CharacterType.Enemy)
            myPosition = enemyDataList[InIndex].transform.position;
        else if (character == CharacterType.Fish)
            myPosition = fishDataList[InIndex].transform.position;

        return myPosition;
    }
}