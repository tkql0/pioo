using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _detailMenuPanel;

    [SerializeField]
    private Text _abilityValueNameText;
    [SerializeField]
    private Text _minValueNameText;
    [SerializeField]
    private Text _maxValueNameText;
    [SerializeField]
    private Text _minValueText;
    [SerializeField]
    private Text _maxValueText;
    [SerializeField]
    private Text _explanationText;

    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Text _damageText;
    [SerializeField]
    private Text _speedText;
    [SerializeField]
    private Text _attackPowerText;
    [SerializeField]
    private Text _breathText;
    [SerializeField]
    private Text _fishStorageText;

    [SerializeField]
    private Text _levelText;

    private AbilityType abilityType = AbilityType.NULL;

    private void Update()
    {
        if (GameManager.UI._isClick == false)
            return;

        PlayerStat();
    }

    public void PlayerStat()
    {
        Player player = GameManager.OBJECT.player;

        _healthText.text = "체력 : " + (int)player.curHealth + " / " +
            (int)player.maxHealth;
        _damageText.text = "공격력 : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
        _speedText.text = "속도 : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
        _attackPowerText.text = "공격사거리 : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
        _breathText.text = "잠수 시간 : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
        _fishStorageText.text = "물고기 저장소 공간 : " + player.fishItemMaxCount +
            " / 사용량 : " + player.digestionMaxCount;

        _levelText.text = "Lv. " + player.PlayerLv + " ( " + player.LvPoint + " )";

        if (GameManager.UI._isClick == false)
            _detailMenuPanel.SetActive(false);
    }

    public void Ability(int InAbility)
    {
        Player player = GameManager.OBJECT.player;

        if (_detailMenuPanel.activeSelf == false)
            _detailMenuPanel.SetActive(true);

        switch (InAbility)
        {
            case (int)AbilityType.Health_Ability:
                abilityType = AbilityType.Health_Ability;

                _abilityValueNameText.text = "체력";
                _minValueNameText.text = "현재 체력\n" +
                    (int)player.curHealth + " + 2";
                _maxValueNameText.text = "최대 체력\n" +
                    (int)player.maxHealth + " + 2";
                _explanationText.text = "test : 현재 체력에 포인트를 사용하더라도\n" +
                    "최대 체력을 넘을 수 없습니다.";
                break;
            case (int)AbilityType.Damage_Ability:
                abilityType = AbilityType.Damage_Ability;

                _abilityValueNameText.text = "공격력";
                _minValueNameText.text = "공격력\n" +
                    player.playerDamage + " + 1";
                _maxValueNameText.text = "추가 공격력\n" +
                    player.playerCriticalDamage + " + 1";
                _explanationText.text = "test : 추가 공격력은 치명타시\n" +
                    "증가하는 수치입니다.";
                break;
            case (int)AbilityType.Speed_Ability:
                abilityType = AbilityType.Speed_Ability;

                _abilityValueNameText.text = "속도";

                if (GameManager.UI.SetAbilityMax(player.moveSpeed, player.moveMaxSpeed))
                    _minValueNameText.text = "현재 속도\nMax";
                else
                    _minValueNameText.text = "현재 속도\n" +
                        player.moveSpeed + " + 0.5";

                _maxValueNameText.text = "최대 속도\n" +
                    player.moveMaxSpeed + " + 0.5";
                _explanationText.text = "test : 현재 속도에 포인트를 사용하더라도\n" +
                    "최대 속도를 넘을 수 없습니다.";
                break;
            case (int)AbilityType.Power_Ability:
                abilityType = AbilityType.Power_Ability;

                _abilityValueNameText.text = "사거리";

                if (GameManager.UI.SetAbilityMax(player.attackMinPower, player.attackMaxPower))
                    _minValueNameText.text = "현재 사거리\nMax";
                else
                    _minValueNameText.text = "현재 사거리\n" +
                        player.attackMinPower + " + 1";

                _maxValueNameText.text = "최대 사거리\n" +
                    player.attackMaxPower + " + 1";
                _explanationText.text = "test : 현재 사거리에 포인트를 사용하더라도\n" +
                    "최대 사거리를 넘을 수 없습니다.";
                break;
            case (int)AbilityType.Breath_Ability:
                abilityType = AbilityType.Breath_Ability;

                _abilityValueNameText.text = "잠수시간";
                _minValueNameText.text = "현재 잠수시간\n" +
                    (int)player.curBreath + " + 1";
                _maxValueNameText.text = "최대 잠수시간\n" +
                    (int)player.maxBreath + " + 1";
                _explanationText.text = "test : 현재 잠수시간에 포인트를 사용하더라도\n" +
                    "최대 잠수시간을 넘을 수 없습니다.";
                break;
            case (int)AbilityType.Storage_Ability:
                abilityType = AbilityType.Storage_Ability;

                _abilityValueNameText.text = "저장소";
                _minValueNameText.text = "사용량\n" +
                    player.digestionMaxCount + " + 1";
                _maxValueNameText.text = "저장공간\n" +
                    player.fishItemMaxCount + " + 1";
                _explanationText.text = ".";
                break;
        }
    }

    public void ChoiceValue(int InValue)
    {
        Player player = GameManager.OBJECT.player;

        if (player.LvPoint <= 0)
            return;

        if (InValue == Min)
            ChoiceMinValue(player);
        else
            ChoiceMaxValue(player);
    }

    private void ChoiceMinValue(Player InPlayer)
    {
        switch (abilityType)
        {
            case AbilityType.Health_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.curHealth, InPlayer.maxHealth))
                {
                    SetOverFlowValue(InPlayer.curHealth, InPlayer.maxHealth);
                    _minValueNameText.text = "현재 체력\nMax";
                    return;
                }
                InPlayer.curHealth += 2f;
                _minValueNameText.text = "현재 체력\n" +
                    (int)InPlayer.curHealth + " + 2";
                GameManager.UI.isHPUpdate = true;
                break;
            case AbilityType.Damage_Ability:
                InPlayer.playerDamage += 1;
                _minValueNameText.text = "공격력\n" +
                    InPlayer.playerDamage + " + 1";
                break;
            case AbilityType.Speed_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.moveSpeed, InPlayer.moveMaxSpeed))
                {
                    SetOverFlowValue(InPlayer.moveSpeed, InPlayer.moveMaxSpeed);
                    _minValueNameText.text = "현재 속도\nMax";
                    return;
                }
                InPlayer.moveSpeed += 0.5f;
                _minValueNameText.text = "현재 속도\n" +
                    InPlayer.moveSpeed + " + 0.5";
                break;
            case AbilityType.Power_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.attackMinPower, InPlayer.attackMaxPower))
                {
                    SetOverFlowValue(InPlayer.attackMinPower, InPlayer.attackMaxPower);
                    _minValueNameText.text = "현재 사거리\nMax";
                    return;
                }
                InPlayer.attackMinPower += 1f;
                _minValueNameText.text = "현재 사거리\n" +
                    InPlayer.attackMinPower + " + 1";
                break;
            case AbilityType.Breath_Ability:

                if (GameManager.UI.SetAbilityMax(InPlayer.curBreath, InPlayer.maxBreath))
                {
                    SetOverFlowValue(InPlayer.curBreath, InPlayer.maxBreath);
                    _minValueNameText.text = "현재 잠수시간\nMax";
                    return;
                }
                InPlayer.curBreath += 1f;
                _minValueNameText.text = "현재 잠수시간\n" +
                    (int)InPlayer.curBreath + " + 1";
                break;
            case AbilityType.Storage_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.fishEatCount, InPlayer.fishItemMaxCount))
                {
                    SetOverFlowValue(InPlayer.digestionMaxCount, InPlayer.fishItemMaxCount);
                    _minValueNameText.text = "식사량\nMax";
                    return;
                }
                InPlayer.digestionMaxCount += 1;
                _minValueNameText.text = "식사량\n" +
                    InPlayer.digestionMaxCount + " + 1";
                break;
        }

        UsePoint(InPlayer);
    }

    private void ChoiceMaxValue(Player InPlayer)
    {
        switch (abilityType)
        {
            case AbilityType.Health_Ability:
                if (InPlayer.maxHealth >= 80)
                {
                    _maxValueNameText.text = "최대 체력\nMax";
                    return;
                }
                InPlayer.maxHealth += 2f;
                _maxValueNameText.text = "최대 체력\n" +
                    (int)InPlayer.maxHealth + " + 2";
                GameManager.UI.isHPUpdate = true;

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "현재 체력\n" +
                        (int)InPlayer.curHealth + " + 2";
                break;
            case AbilityType.Damage_Ability:
                InPlayer.playerCriticalDamage += 1;
                _maxValueNameText.text = "추가 공격력\n" +
                    InPlayer.playerCriticalDamage + " + 1";
                    break;
            case AbilityType.Speed_Ability:
                if(InPlayer.moveMaxSpeed >= 25)
                {
                    _maxValueNameText.text = "현재 사거리\nMax";
                    return;
                }

                InPlayer.moveMaxSpeed += 0.5f;
                _maxValueNameText.text = "최대 속도\n" +
                    InPlayer.moveMaxSpeed + " + 0.5";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "현재 속도\n" +
                        InPlayer.moveSpeed + " + 0.5";
                break;
            case AbilityType.Power_Ability:
                InPlayer.attackMaxPower += 1;
                _maxValueNameText.text = "최대 사거리\n" +
                    InPlayer.attackMaxPower + " + 1";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "현재 사거리\n" +
                        InPlayer.attackMinPower + " + 1";
                break;
            case AbilityType.Breath_Ability:
                InPlayer.maxBreath += 1f;
                _maxValueNameText.text = "최대 잠수시간\n" +
                    (int)InPlayer.maxBreath + " + 1";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "현재 잠수시간\n" +
                        (int)InPlayer.curBreath + " + 1";
                break;
            case AbilityType.Storage_Ability:
                InPlayer.fishItemMaxCount += 1;
                _maxValueNameText.text = "저장공간\n" +
                    InPlayer.fishItemMaxCount + " + 1";
                break;
        }

        UsePoint(InPlayer);
    }

    private void UsePoint(Player InPlayer)
    {
        --InPlayer.LvPoint;
        _levelText.text = "Lv. " + InPlayer.PlayerLv + " ( " + InPlayer.LvPoint + " )";
    }

    private void SetOverFlowValue(float InMinValue, float InMaxValue)
    {
        InMinValue = InMaxValue;
    }

    private const int Min = 1;
}