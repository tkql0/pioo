using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTree : MonoSingleTon<GameTree>
{
    public GameManager gameManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public ObjectManager objectManager { get; private set; }

    private void Awake()
    {
        gameManager = new();
        uiManager = new();
        objectManager = new();
    }
}
