using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private ObjectController _objectController;
    SpawnController _spawnController;
    MapController _mapController;

    private void Update()
    {
        if (!Input.anyKey)
            return;
        mapRelocation();
    }

    public void Init()
    {
        _objectController = GameTree.GAME.objectController;
        _spawnController = GameTree.GAME.spawnController;
        _mapController = GameTree.GAME.mapController;
        
    }

    public void OnEnable()
    {
        //mapRelocation();
    }

    public void OnDisable()
    {


    }

    public void mapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        for (int i = 0; i < _objectController.playerList.Count; i++)
        {
            targetPosition = _objectController.playerList[i].transform.position;

            for (int j = 0; j < _mapController.mapList.Count; j++)
            {
                if (i == _mapController.mapList.Count % 3)
                {
                    myPosition = _mapController.mapList[j].transform.position;

                    float DistanceX = targetPosition.x - myPosition.x;
                    float differenceX = Mathf.Abs(DistanceX);

                    DistanceX = DistanceX > 0 ? 1 : -1;

                    if (differenceX > 30.0f)
                    {
                        _mapController.mapList[j].transform.Translate(Vector3.right * DistanceX * 60);
                        _spawnController.Spawn(_mapController.mapList[j].gameObject);
                    }
                }
            }
        }
    }
}
