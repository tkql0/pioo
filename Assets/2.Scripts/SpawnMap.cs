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
    public GameObject mapObject;
    // �� ������Ʈ�� ���� �÷��̾� ���� 3�辿 �����ǰ�
    // �� �̵��� stayPlayer�� 2�̻��� �ƴ϶��
    // ��Ȱ��ȭ �Ǿ��ִ� �� ������Ʈ �߿��� �������� Ȱ��ȭ

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player�� mySpawnNumber�� ���� ����

    //private bool isRelocation = false;
    //public int stayPlayer;
    // �ش� �ʿ� �ӹ����� �ִ� Player�� 0�̶�� ��Ȱ��ȭ
    // �ش� �ʿ� Player�� �ӹ����ִ��� Ȯ���� �ϴ� ����
    // ���� x��ǥ�� 40(�ʰ��� ����)�� ������
    // MapController Ŭ������ �ִ� mapPositionKey[mySpawnNumber]�� ���� ��
    // ���� ���ڰ� �ִ��� for������ Ȯ��



    //private void Update()
    //{
    //    MapPosition();
    //}

    //private void MapRelocation()
    private void MapPosition()
    { // ���� �ƴ� ���� ���� ������ �̵�
        GameManager.Map.SetMapPositionKey(mySpawnNumber, myMapPosition);
        // ���� ���� ��ġ Ȯ��

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary�� ���� Player���� mySpawnNumber�� �ް�
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // ���� ������ Player�� ����ٴϴ� MapPosition()

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // ���� �̵����� ������ ���ʹ� �Ÿ��� ���� ��Ȱ��ȭ

        if (differenceX > DeSpawn_Distance)
        { // ���� Palyer���Լ� �־�������
            if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            { // �̵� �� �ʿ� �����ϴ� �÷��̾ 1 ������ �� (�ڽ� ����)
                mapObject.SetActive(false);
                GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
                // �ʰ� ���͸� ��Ȱ��ȭ
            }

            mapSpawnObject.Translate(Vector2.right * DistanceX * 120);
            // MapPosition()�� �̵�

            if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            { // �̵� �� �ʿ� �����ϴ� �÷��̾ 1 ������ �� (�ڽ� ����)

            }
        }
    }

    private void MapRandomSpawn()
    { // ��Ȱ��ȭ�� �� ������Ʈ�� �ϳ� �������� Ȱ��ȭ

    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 myMapPosition => transform.position;
    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
