using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController
{
    //public MyCharater myCharacter;
    public Dictionary<int, GameObject> playerList;
    public Dictionary<long, EnemyCharater> enemyList;
    public Dictionary<long, AttackUnableCharacter> FishList;

    private void OnEnable()
    {

    }
    public void OnDisable()
    {


    }

    virtual public void ObjectMove(Rigidbody2D rigid, SpriteRenderer sprite)
    {
        int nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        float speed = Random.Range(0.1f, 5);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
    }

    //void DistanceFromPlayer()
    //{
    //    Vector3 playerPos = myCharacter.transform.position;
    //    Vector3 myPos = transform.position;

    //    float DirX = playerPos.x - myPos.x;
    //    float diffX = Mathf.Abs(DirX);

    //    if (diffX > 70.0f)
    //        gameObject.SetActive(false);
    //}
}