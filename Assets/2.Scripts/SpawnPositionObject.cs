using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionObject : MonoBehaviour
{
    public float mapPositionKey;

    private void Update()
    {
        MyPosition();
    }

    private void MyPosition()
    {
        mapPositionKey = myMapPosition.x / 40f;

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > DeSpawn_Distance)
        {
            Relocation(DistanceX);
        }
    }

    private void Relocation(float InDistance)
    {
        mapSpawnObject.Translate(Vector2.right * InDistance * 120);
    }

    private void RandomMap()
    {
        
    }

    public Vector2 myMapPosition => transform.position;
    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
