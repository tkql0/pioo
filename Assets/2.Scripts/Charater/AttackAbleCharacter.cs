using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCharacter : MonoBehaviour
{
    public LayerMask targetMask;

    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public int Damage = 5;

    public float Rate_Of_Fire;
    public float CoolDown_Time;

    public const string Fish = "Fish_exp";
    public const string Player = "Player";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Player_Attack = "Player_Attack";

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        Rate_Of_Fire = 5f;
        CoolDown_Time = 0f;
    }

    public IEnumerator OnDamage(SpriteRenderer sprite)
    {
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        sprite.color = Color.white;
    }
}