using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController
{
    public UIObject uiObject = null;

    public void OnEnable()
    {
        uiObject = GameObject.FindObjectOfType<UIObject>();
    }

    public void SetUIEnter(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.zero;
    }

    public void SetUIExit(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.up * 2000;
    }

    public void SetSliderUpdate(float InMaxValue, float InValue, Slider InSlider)
    {
        InSlider.maxValue = InMaxValue;
        InSlider.value = InValue;
    }
}
