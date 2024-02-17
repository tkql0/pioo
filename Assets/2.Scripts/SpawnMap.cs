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

    public LayerMask targetMask;
    private RaycastHit2D[] _inTarget;

    private bool isRelocation = false;
    public bool isStayPlayer = false;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isRelocation == false)
    //        return;

    //    //mapObject = collision.gameObject;
    //}

    private void SetMapSetting(GameObject mapObject)
    {
        if (isRelocation == false)
            return;

        _inTarget = Physics2D.CircleCastAll(mapSpawnObject.position, 1, Vector2.zero, 0, targetMask);

        if (_inTarget == null)
            return;

        isStayPlayer = true;

        Transform target = _inTarget[0].transform;
        mapObject = target.gameObject;

        switch(mapObject.tag)
        {
            case "RandomMap":
                break;
            case "EventMap":
                break;
            case "PlayerSettingMap":
                break;
        }
    }
}
