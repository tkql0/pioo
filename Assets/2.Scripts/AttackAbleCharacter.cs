using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCharacter : MonoBehaviour
{
    RaycastHit2D[] inTarget;
    [SerializeField]
    float hostileAreaAnge = 3;
    [SerializeField]
    LayerMask targetMask;

    float Rate_Of_Fire;
    float CoolDown_Time;
    private void Update()
    {
        if (Rate_Of_Fire < CoolDown_Time)
        {
            CoolDown_Time = 0f;
            Attack();
        }
    }
    private void FixedUpdate()
    {
        Search();
    }
    private void OnEnable()
    {
        Rate_Of_Fire = 1.5f;
        CoolDown_Time = 0f;
    }

    void Search()
    { // 플레이어 탐색
        inTarget = Physics2D.CircleCastAll(transform.position + new Vector3(0, 1, 0), hostileAreaAnge, Vector2.zero, 0, targetMask);

        if (inTarget.Length != 0)
            CoolDown_Time += Time.deltaTime;
    }
    void Attack()
    {
        if (inTarget.Length == 0)
            return;
    }
}