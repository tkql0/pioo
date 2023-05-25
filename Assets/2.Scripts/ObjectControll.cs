using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    public GameObject Map;
    public GameObject Player;
    public GameObject enemy_prefab;
    public GameObject enemy_exp_prefab;
    public GameObject fish_exp_prefab;

    public int re_fish = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameTree.Instance.objectManager.enemy = new GameObject[20];
        GameTree.Instance.objectManager.enemy_exp = new GameObject[60];
        GameTree.Instance.objectManager.fish_exp = new GameObject[50];

        Generate();

        StartCoroutine(spawn());
    }

    private void Update()
    {
        if (re_fish < 0)
        {
            spawn_fish();
            re_fish++;
        }
    }

    public void GameStart()
    {
        if (GameTree.Instance.uiManager.GameStart_Panel.activeSelf == false)
        {
            return;
        }

        PlayerSpawn();

        GameTree.Instance.uiManager.GameStart_Panel.SetActive(false);
        GameTree.Instance.gameManager.GameStart = true;
    }

    void Generate()
    {
        for (int i = 0; i < GameTree.Instance.objectManager.enemy.Length; i++)
        {
            GameTree.Instance.objectManager.enemy[i]
                = Instantiate(enemy_prefab, transform);
            GameTree.Instance.objectManager.enemy[i].SetActive(false);
        }
        for (int i = 0; i < GameTree.Instance.objectManager.enemy_exp.Length; i++)
        {
            GameTree.Instance.objectManager.enemy_exp[i]
                = Instantiate(enemy_exp_prefab, transform);
            GameTree.Instance.objectManager.enemy_exp[i].SetActive(false);
        }
        for (int i = 0; i < GameTree.Instance.objectManager.fish_exp.Length; i++)
        {
            GameTree.Instance.objectManager.fish_exp[i]
                = Instantiate(fish_exp_prefab, transform);
            GameTree.Instance.objectManager.fish_exp[i].SetActive(false);
        }
    }

    public GameObject MakeObj(Obj type)
    {
        switch (type)
        {
            case Obj.enemy_:
                GameTree.Instance.objectManager.targetPool = GameTree.Instance.objectManager.enemy;
                break;
            case Obj.enemy_exp_:
                GameTree.Instance.objectManager.targetPool = GameTree.Instance.objectManager.enemy_exp;
                break;
            case Obj.fish_exp_:
                GameTree.Instance.objectManager.targetPool = GameTree.Instance.objectManager.fish_exp;
                break;
        }
        for (int i = 0; i < GameTree.Instance.objectManager.targetPool.Length; i++)
        {
            if (!GameTree.Instance.objectManager.targetPool[i].activeSelf)
            {
                GameTree.Instance.objectManager.targetPool[i].SetActive(true);
                return GameTree.Instance.objectManager.targetPool[i];
            }
        }
        return null;
    }

    void PlayerSpawn()
    {
        GameObject PlayObj;

        PlayObj = Instantiate(Player, new Vector3(0, 1, 0), Quaternion.identity);
        GameTree.Instance.gameManager.cameraControll.player = PlayObj;
        for (int i = -1; i <= 1; i++)
        {
            Instantiate(Map, new Vector3(transform.position.x + (i * 20), 0, 0), Quaternion.identity);
        }
    }

    void spawn_enemy()
    {
        int ranEnemy = Random.Range(-20, 20);
        GameObject enemy = MakeObj(Obj.enemy_);
        enemy.transform.position = new Vector3(transform.position.x + ranEnemy, 0.3f, -2);
    }

    IEnumerator spawn()
    {
        spawn_enemy();
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(spawn());
    }

    void spawn_fish()
    {
        re_fish = GameTree.Instance.objectManager.fish_exp.Length;

        for (int i = 1; i <= re_fish; i++)
        {
            int ranX = Random.Range(-69, 70);
            int ranY = Random.Range(-25, -5);
            GameObject fish = MakeObj(Obj.fish_exp_);
            fish.transform.position = new Vector3(ranX + Player.transform.position.x, ranY, -1);
        }
        re_fish = 0;
    }
}
