using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool _isStatusUpdate = false;
    public bool _isStart = true;

    public bool isHPUpdate = false;

    public bool _isMaxValue = false;

    //public int abilityNumber;

    public void OnEnable()
    {

    }

    public void SetUIEnter(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.zero;
    }

    public void SetUIExit(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.up * 2000;
    }

    public void SetStatsUIEnter(RectTransform InUI)
    {
        InUI.anchoredPosition = Vector2.up * 40;
    }

    public void SetSliderUpdate(float InMaxValue, float InValue, Slider InSlider)
    {
        InSlider.maxValue = InMaxValue;
        InSlider.value = InValue;
    }

    public int GetLevelUpStat(Player InPlayer)
    {
        InPlayer.curExperience = 0;
        InPlayer.maxHealth += 1;
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

    public void AbilityButtonClick(int InAbilityNumber)
    {
        AbilityButtonNonClick(InAbilityNumber);

        switch (InAbilityNumber)
        {
            case (int)AbilityType.Health_Ability:
            case (int)AbilityType.Damage_Ability:
            case (int)AbilityType.Speed_Ability:
            case (int)AbilityType.Power_Ability:
            case (int)AbilityType.Breath_Ability:
            case (int)AbilityType.Storage_Ability:
                if (abilityUI[InAbilityNumber].localPosition.y != 40)
                    SetStatsUIEnter(abilityUI[InAbilityNumber]);
                break;

        }
    }

    public RectTransform[] abilityUI = new RectTransform[7];

    public void AbilityButtonNonClick(int InAbilityNumber)
    {
        for(int i = 1; i < abilityUI.Length; i++)
        {
            if(i != InAbilityNumber)
                SetUIExit(abilityUI[i]);
        }
    }
}
