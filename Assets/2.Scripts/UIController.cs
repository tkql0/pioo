using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController
{
    public UIObject uiObject = null;

    public void OnEnable()
    {
        uiObject = GameObject.FindObjectOfType<UIObject>();

    }
}
