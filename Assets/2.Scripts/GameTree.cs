using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTree : MonoSingleTon<GameTree>
{
    static public GameManager GAME { get; private set; }
    static public UIManager UI { get; private set; }

    private void Awake()
    {
        GAME = new();
        UI = new();
    }

    private void OnEnable()
    {
        GAME.OnEnable();
        UI.OnEnable();
    }

    private void OnDisable()
    {
        GAME.OnDisable();
        UI.OnDisable();
    }
}