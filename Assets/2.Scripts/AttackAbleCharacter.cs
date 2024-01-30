using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCharacter : MonoBehaviour
{
    public LayerMask targetMask;

    public int playerDamage = 5;

    public float Rate_Of_Fire;
    public float CoolDown_Time;

    public const string Fish = "Fish_exp";
    public const string Player = "Player";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Player_Attack = "Player_Attack";

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

    public IEnumerator OnDamage(SpriteRenderer sprite, bool isDamage)
    {
        isDamage = true;
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        isDamage = false;
        sprite.color = Color.white;
    }
}