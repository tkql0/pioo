using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DeSpawn", 1f);
    }

    private void DeSpawn()
    {
        GameManager.SPAWN.StackAttackEffectDeSpwan(gameObject);

        gameObject.SetActive(false);
    }
}
