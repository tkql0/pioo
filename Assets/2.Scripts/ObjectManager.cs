using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obj
{
    enemy_,
    enemy_exp_,
    fish_exp_
}

public class ObjectManager
{
    public ObjectControll objectControll;

    public GameObject[] enemy;
    public GameObject[] enemy_exp;
    public GameObject[] fish_exp;

    public GameObject[] targetPool;
}
