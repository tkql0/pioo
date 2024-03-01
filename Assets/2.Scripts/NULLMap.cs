using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum MapType
//{
//    NULL,
//    RandomMap,
//    EventMap,
//    PlayerSettingMap,
//}
// �ٽ�.. �� SpawnMap class�� ������ ���� �̵��� ���� ����

public class NULLMap : MonoBehaviour
{
    //public List<GameObject> mapObjects = new List<GameObject>();
    // �� ������Ʈ�� ���� �÷��̾� ���� 3�辿 �����ǰ�
    // �� �̵��� stayPlayer�� 2�̻��� �ƴ϶��
    // ��Ȱ��ȭ �Ǿ��ִ� �� ������Ʈ �߿��� �������� Ȱ��ȭ
    // ���� ��ü ������Ʈ�� ��Ȱ��ȭ�� ���� ã�°ͺ���
    // 3���� ���� �Ҵ��ؼ� �� 3�� �߿��� Ȯ���� �����ϸ� �ǰڳ�

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player�� mySpawnNumber�� ���� ����

    public float mapPositionKey;

    public GameObject mabObject;

    //private bool isRelocation = false;
    public int stayPlayer;
    // �ش� �ʿ� �ӹ����� �ִ� Player�� 0�̶�� ��Ȱ��ȭ
    // �ش� �ʿ� Player�� �ӹ����ִ��� Ȯ���� �ϴ� ����
    // ���� x��ǥ�� 40(�ʰ��� ����)�� ������
    // MapController Ŭ������ �ִ� mapPositionKey[mySpawnNumber]�� ���� ��
    // ���� ���ڰ� �ִ��� for������ Ȯ��


    private void Update()
    {
        MapPosition();
    }

    private void MapPosition()
    { // ���� �ƴ� ���� ���� ������ �̵�
        mapPositionKey = myMapPosition.x / 40f;

        //GameManager.Map.SetMapPositionKey(mySpawnNumber, myMapPosition);
        // ���� ���� ��ġ Ȯ��

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary�� ���� Player���� mySpawnNumber�� �ް�
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // ���� ������ Player�� ����ٴϴ� MapPosition()

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        MapRelocation(DistanceX, targetPosition);

        //GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // ���� �̵����� ������ ���ʹ� �Ÿ��� ���� ��Ȱ��ȭ

        if (differenceX > DeSpawn_Distance)
        { // ���� Palyer���Լ� �־�������
            MapRelocation(DistanceX, targetPosition);
        }
    }

    private void MapRelocation(float InDistance, Vector2 targetPosition)
    {
        //foreach (KeyValuePair<long, NULLMap> outMapData in GameManager.OBJECT.testMapDataList)
        //{
        //    if (outMapData.Value.mapPositionKey == mapPositionKey)
        //    {
        //        mabObject = outMapData.Value.mabObject;
        //        stayPlayer++;
        //        // ����
        //        // ���� �����ǰ� �� ������Ʈ�� ���������� �ΰ�
        //        // stayPlayer�� 1�̻��� ������Ʈ�� Ȱ��ȭ
        //        // �ƴϸ� ��Ȱ��ȭ
        //    }
        //}

            //if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            //{ // �̵� �� �ʿ� �����ϴ� �÷��̾ 1 ������ �� (�ڽ� ����)
            //foreach (GameObject map in mapObjects)
            //{ // �� ������ ���� �� �� �̵��� ����Ʈ�� �ִ� ������Ʈ���� ��� ��Ȱ��ȭ�ϱ�
            //    map.SetActive(false);
            //    // �� ��Ȳ���� 2��° �� �̵��� �Ҷ� ������ �ʿ� �÷��̾ �־��ٸ�
            //    // ���ڱ� ���� ������� ��Ȳ�� ��������?
            //    // ��ųʸ��� ���� �����ϰ� ��Ȱ��ȭ ������ ������ �ص� �ʱ�ȭ �Ǵϱ�
            //    // ��Ȱ��ȭ �� ������Ʈ �߿��� �����°� ��������
            //}
            //GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
            // �ʰ� ���͸� ��Ȱ��ȭ
        //}

        mapSpawnObject.Translate(Vector2.right * InDistance * 120);
        // MapPosition()�� �̵�

        //if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
        //{ // �̵� �� �ʿ� �����ϴ� �÷��̾ 1 ������ �� (�ڽ� ����)

        //}
        //else
        //{
        //    //otherMabObject = 
        //}
    }

    private void MapRandomSpawn()
    { // ��Ȱ��ȭ�� �� ������Ʈ�� �ϳ� �������� Ȱ��ȭ

    }

    public Vector2 myMapPosition => transform.position;

    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
