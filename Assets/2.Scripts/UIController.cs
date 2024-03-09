using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController
{
    public bool _isClick = false;
    public bool _isStart = true;

    public void OnEnable()
    {

    }

    public void SetUIEnter(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.down * 35;
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

    public int GetLevelUpStat(Player InPlayer)
    {
        InPlayer.curExperience = 0;
        InPlayer.maxHealth += 5;
        InPlayer.maxExperience += 5;

        return ++InPlayer.PlayerLv;
    }
}
