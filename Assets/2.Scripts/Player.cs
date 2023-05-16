using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public bool isDie;
    public bool isJump;
    public bool isDamage;
    public bool isLvUp;

    public int melee_damage = 5;
    public int ranged_damage = 5;

    private void Start()
    {
        GameTree.Instance.gameManager.player = this;
    }

    void OnEnable()
    {
        isDie = false;
        isJump = false;
        isDamage = false;
        isLvUp = false;
    }
}
