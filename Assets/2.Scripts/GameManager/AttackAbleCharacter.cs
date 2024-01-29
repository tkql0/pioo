using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCharacter : MonoBehaviour
{
    public LayerMask targetMask;

    public int playerDamage = 5;

    public float Rate_Of_Fire;
    public float CoolDown_Time;
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
    private void OnEnable()
    {
        Rate_Of_Fire = 5f;
        CoolDown_Time = 0f;
    }

    private void EnemyCommand(long InCharacterld)
    {
        if (GameTree.GAME.objectController.enemyDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;
    }
}