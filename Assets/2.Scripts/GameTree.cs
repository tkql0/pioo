using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameTree : MonoSingleTon<GameTree>
{
    static public GameManager GAME { get; private set; }
    static public MapManager MAP { get; private set; }
    static public UIManager UI { get; private set; }

    //public ObjectPool objectPool;

    private void Awake()
    {
        GAME = new();
        MAP = new();
        UI = new();

        //GAME.objectController = gameObject.AddComponent<ObjectController>();
        //GAME.spawnController = gameObject.AddComponent<SpawnController>();

        //Init();
    }

    private void OnEnable()
    {
        GAME.OnEnable();
        MAP.OnEnable();
        UI.OnEnable();
    }

    private void OnDisable()
    {
        GAME.OnDisable();
        MAP.OnDisable();
        UI.OnDisable();
    }
}