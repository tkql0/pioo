using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTon<GameManager>
{
    static public ObjectController OBJECT { get; private set; }
    static public SpawnController SPAWN { get; private set; }
    static public MapController Map { get; private set; }
    static public UIController UI { get; private set; }

    private void Awake()
    {
        OBJECT = new();
        SPAWN = new();
        Map = new();
        UI = new();

        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        SPAWN.OnEnable();
        OBJECT.OnEnable();
        Map.OnEnable();
        UI.OnEnable();
    }

    private void OnDisable()
    {

    }
}