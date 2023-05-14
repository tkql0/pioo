using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    private void Start()
    {
        GameTree.Instance.uiManager.GameStart_Panel = transform.GetChild(0).gameObject;
        GameTree.Instance.uiManager.GameOver_Panel = transform.GetChild(1).gameObject;
    }
}
