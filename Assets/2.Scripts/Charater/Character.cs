using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public LayerMask targetMask;

    public ObjectType key;
    public long spawnNumberKey;
    public long mySpawnNumber;

    public GameObject characterObject;

    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public int Damage = 5;

    public float coolTimeMax;
    public float coolTime;

    public float maxHealth;
    public float curHealth;

    public bool isDie;
    public bool isDamage;

    public const string Fish = "Fish_exp";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Player_Attack = "Player_Attack";

    private void Update()
    {
        
    }

    private void OnEnable()
    {

    }

    public virtual void Movement()
    {
        if (!gameObject.activeSelf)
            return;

        int nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        float speed = Random.Range(0.5f, 5f);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
    }

    public IEnumerator OnDamage(SpriteRenderer InSprite)
    {
        InSprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        InSprite.color = Color.white;

        isDamage = false;
    }

    public void Die(Slider InHealthSlider)
    {
        if (InHealthSlider.value <= 0)
        {
            isDie = true;

            gameObject.SetActive(false);
            return;
        }
        else
            isDie = false;
    }
    public void SetActiveObject(bool InIsActive)
    {
        characterObject?.SetActive(InIsActive);
    }
}