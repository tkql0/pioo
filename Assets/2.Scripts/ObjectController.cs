using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController
{
    public Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    //public MyCharater myCharater;
    //public GameObject myCharater;
    public Dictionary<long, GameObject> enemyList = new Dictionary<long, GameObject>();
    public Dictionary<long, GameObject> FishList = new Dictionary<long, GameObject>();

    public int playersCount = 1;

    private void OnEnable()
    {

    }
    public void OnDisable()
    {


    }

    public void ObjectMove(Rigidbody2D rigid, SpriteRenderer sprite)
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