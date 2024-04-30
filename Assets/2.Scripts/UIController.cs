using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum AbilityType
{
    NULL,
    Health_Ability,
    Damage_Ability,
    Speed_Ability,
    Power_Ability,
    Breath_Ability,
    Storage_Ability,
}

public class UIController
{
    public bool _isClick = false;
    public bool _isStart = true;

    public bool isHPUpdate = false;

    public bool _isMaxValue = false;

    public void OnEnable()
    {

    }

    public void SetUIEnter(RectTransform InUI)
    {
        InUI.anchoredPosition = new Vector2(55, 60);
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
        InPlayer.LvPoint++;
        InPlayer.curHealth = InPlayer.maxHealth;
        GameManager.UI.isHPUpdate = true;

        return ++InPlayer.PlayerLv;
    }

    public bool SetAbilityMax(float InMin, float InMax)
    {
        _isMaxValue = false;

        if (InMin >= InMax)
            _isMaxValue = true;

        return _isMaxValue;
    }
}
