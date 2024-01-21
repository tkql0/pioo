using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameTree : MonoSingleTon<GameTree>
{
    static public GameManager GAME { get; private set; }
    static public MapManager MAP { get; private set; }
    static public UIManager UI { get; private set; }

    public ObjectPool objectPool;

    private void Awake()
    {
        GAME = new();
        MAP = new();
        UI = new();

        GAME.obhectController = gameObject.AddComponent<ObjectController>();
        GAME.spawnController = gameObject.AddComponent<SpawnController>();

        //Init();
    }

    private void OnEnable()
    {
        GAME.Init();
        MAP.Init();
        UI.Init();
    }

    private void OnDisable()
    {

    }
}