using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameTree.Instance.objectManager.enemy = new GameObject[20];
        GameTree.Instance.objectManager.enemy_exp = new GameObject[60];
        GameTree.Instance.objectManager.fish_exp = new GameObject[50];

        Generate();
    }

    private void Start()
    {

    }

    public GameObject Map;
    public GameObject Player;
    public GameObject enemy_prefab;
    public GameObject enemy_exp_prefab;
    public GameObject fish_exp_prefab;

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
}
