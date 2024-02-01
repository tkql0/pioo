using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTon<GameManager>
{
    static public ObjectController OBJECT { get; private set; }
    static public SpawnController SPAWN { get; private set; }

    private void Awake()
    {
        OBJECT = new();
        SPAWN = new();
    }

    public void OnEnable()
    {
        SPAWN.OnEnable();
        OBJECT.OnEnable();
    }

    public void OnDisable()
    {


    }
}