using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    NULL,
    RandomMap,
    EventMap,
    PlayerSettingMap,
}

public class SpawnMap : MonoBehaviour
{
    public Transform mapSpawnObject;
    public GameObject mapObject;

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player�� mySpawnNumber�� ���� ����

    //private bool isRelocation = false;
    public int stayPlayer = 0;
    // �ش� �ʿ� �ӹ����� �ִ� Player�� 0�̶�� ��Ȱ��ȭ
    // �ش� �ʿ� Player�� �ӹ����ִ��� Ȯ���� �ϴ� ����
    // ���� x��ǥ�� 40(�ʰ��� ����)�� ������
    // MapController Ŭ������ �ִ� mapPositionKey[mySpawnNumber]�� ���� ��
    // ���� ���ڰ� �ִ� for������ Ȯ��



    private void Update()
    {
        MapPosition();
    }

    //private void MapRelocation()
    private void MapPosition()
    { // ���� �ƴ� ���� ���� ������ �̵�
        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary�� ���� Player���� mySpawnNumber�� �ް�
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // ���� ������ Player�� ����ٴϴ� MapPosition()

        float DistanceX = targetPosition.x - mapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // ���� �̵����� ������ ���ʹ� �Ÿ��� ���� ��Ȱ��ȭ

        if (differenceX > DeSpawn_Distance)
        { // ���� Palyer���Լ� �־�������
            mapObject.SetActive(false);
            GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
            // �ʰ� ���͸� ��Ȱ��ȭ

            transform.Translate(Vector2.right * DistanceX * 120);
            // MapPosition()�� �̵�
        }
    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 mapPosition => transform.position;

    private const float DeSpawn_Distance = 60f;
}
