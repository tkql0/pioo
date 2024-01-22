using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public bool gmaeStrat = false;
    public void OnEnable()
    {
        gmaeStrat = true;
    }

    public void OnDisable()
    {
        gmaeStrat = false;
    }
}