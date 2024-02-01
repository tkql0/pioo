using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public int key;

    private const float reSpawnTime =  20f;

    public int enemyMaxSize;
    public int fishMaxSize;

    private void Update()
    {
        MapRelocation();
    }

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        map = gameObject;

        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    public void OnDisable()
    {

    }

    private void MapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        targetPosition = GameManager.OBJECT.player.transform.position;
        myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 30.0f)
        {
            GameManager.SPAWN.DeSpawn(targetPosition);

            transform.Translate(Vector3.right * DistanceX * 60);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(int enemySize, int fishSize)
    {
        GameManager.SPAWN.Spawn(gameObject, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(reSpawnTime, enemySize, fishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, int enemySize, int fishSize)
    {
        yield return new WaitForSeconds(ReSpawnTime);

        GameManager.SPAWN.Spawn(gameObject, ReSpawnSize(enemySize,
            fishSize).Item1, ReSpawnSize(enemySize, fishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, enemySize, fishSize));
    }

    private (int, int) ReSpawnSize(int enemySize, int fishSize)
    {
        int enemyReSpawnSize = 0;
        int fishReSpawnSize = 0;

        for (int i = 0; i < GameManager.OBJECT.enemyDataList.Count; i++)
        {
            if (key == GameManager.OBJECT.enemyDataList[i].key
                && GameManager.OBJECT.enemyDataList[i].enemy.activeSelf)
            {
                enemyReSpawnSize++;
            }
        }
        for (int i = 0; i < GameManager.OBJECT.fishDataList.Count; i++)
        {
            if (key == GameManager.OBJECT.fishDataList[i].key
                && GameManager.OBJECT.fishDataList[i].Fish.activeSelf)
            {
                fishReSpawnSize++;
            }
        }

        int enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        int fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}
// 한 맵에서 랜덤한 수의 몬스터를 잡으면 맵 하늘에 현상금 포스터가 붙고
// 랜덤한 시간이 지나면 보스 몬스터 생성
// 바위오브젝트도 만들어서 랜덤한 수의 맵을 만들거나 랜덤맵 몇개 만들어서
// 만들어 놓은 맵 하나 생성되게?
// 일정한 맵이면 배열로 해도 되려나