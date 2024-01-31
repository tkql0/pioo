using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTree : MonoSingleTon<GameTree>
{
    static public GameManager GAME { get; private set; }

    private void Awake()
    {
        GAME = new();
    }

    private void OnEnable()
    {
        GAME.OnEnable();
    }

    private void OnDisable()
    {
        GAME.OnDisable();
    }
}