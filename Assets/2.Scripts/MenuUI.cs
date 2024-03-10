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

    public void PlayerStat()
    {
        Player player = GameManager.OBJECT.player;

        _healthText.text = "체력 : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
        _damageText.text = "공격력 : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
        _speedText.text = "속도 : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
        _attackPowerText.text = "공격사거리 : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
        _breathText.text = "잠수 시간 : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
        _fishStorageText.text = "저장소 : " + player.fishItemMaxCount +
            " / 식사량 : " + player.fishEatCount;
        _levelText.text = "Lv. " + player.PlayerLv + " ( " + player.LvPoint + " )";

        if(GameManager.UI._isClick == false)
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

                if ((int)player.moveSpeed == (int)player.moveMaxSpeed)
                    _minValueNameText.text = "현재 속도\n" + "Max";
                else
                    _minValueNameText.text = "현재 속도\n" +
                        (int)player.moveSpeed + " + 0.5";

                _maxValueNameText.text = "최대 속도\n" +
                    (int)player.moveMaxSpeed + " + 0.5";
                _explanationText.text = "test : 현재 속도에 포인트를 사용하더라도\n" +
                    "최대 속도를 넘을 수 없습니다.";
                break;
            case (int)AbilityType.Power_Ability:
                abilityType = AbilityType.Power_Ability;

                _abilityValueNameText.text = "사거리";

                if((int)player.attackMinPower == (int)player.attackMaxPower)
                    _minValueNameText.text = "현재 사거리\n" + "Max";
                else
                    _minValueNameText.text = "현재 사거리\n" +
                        (int)player.attackMinPower + " + 1";

                _maxValueNameText.text = "최대 사거리\n" +
                    (int)player.attackMaxPower + " + 1";
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
                _minValueNameText.text = "저장공간\n" +
                    player.fishItemMaxCount + " + 1";
                _maxValueNameText.text = "식사량\n" +
                    player.fishEatCount + " + 1";
                _explanationText.text = ".";
                break;
        }
    }

    public void ChoiceMinValue()
    {
        Player player = GameManager.OBJECT.player;

        if (player.LvPoint <= 0)
            return;

        switch (abilityType)
        {
            case AbilityType.Health_Ability:
                if (player.curHealth >= player.maxHealth)
                    player.curHealth = player.maxHealth;
                else
                    player.curHealth += 2f;

                _minValueNameText.text = "현재 체력\n" +
                    (int)player.curHealth + " + 2";
                _healthText.text = "체력 : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
                break;
            case AbilityType.Damage_Ability:
                player.playerDamage += 1;
                _minValueNameText.text = "공격력\n" +
                    player.playerDamage + " + 1";
                _damageText.text = "공격력 : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
                break;
            case AbilityType.Speed_Ability:
                if (player.moveSpeed >= player.moveMaxSpeed)
                {
                    player.moveSpeed = player.moveMaxSpeed;
                    _minValueNameText.text = "Max";
                    return;
                }
                else
                {
                    player.moveSpeed += 0.5f;
                _minValueNameText.text = "현재 속도\n" +
                    player.moveSpeed + " + 0.5";
                }
                _speedText.text = "속도 : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
                break;
            case AbilityType.Power_Ability:
                if (player.attackMinPower >= player.attackMaxPower)
                {
                    player.attackMinPower = player.attackMaxPower;
                    _minValueNameText.text = "Max";
                    return;
                }
                else
                {
                    player.attackMinPower += 1f;
                    _minValueNameText.text = "현재 사거리\n" +
                        player.attackMinPower + " + 1";
                }
                _attackPowerText.text = "공격사거리 : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
                break;
            case AbilityType.Breath_Ability:
                if (player.curBreath >= player.maxBreath)
                    player.curBreath = player.maxBreath;
                else
                    player.curBreath += 1f;

                _minValueNameText.text = "현재 잠수시간\n" +
                    (int)player.curBreath + " + 1";
                _breathText.text = "잠수 시간 : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
                break;
            case AbilityType.Storage_Ability:
                player.fishItemMaxCount += 1;
                _minValueNameText.text = "저장공간\n" +
                    player.fishItemMaxCount + " + 1";
                break;
        }

        --player.LvPoint;
        _levelText.text = "Lv. " + player.PlayerLv + " ( " + player.LvPoint + " )";
    }
    public void ChoiceMaxValue()
    {
        Player player = GameManager.OBJECT.player;

        if (player.LvPoint <= 0)
            return;

        switch (abilityType)
        {
            case AbilityType.Health_Ability:
                player.maxHealth += 2f;
                _maxValueNameText.text = "최대 체력\n" +
                    (int)player.maxHealth + " + 2";
                _healthText.text = "체력 : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
                break;
            case AbilityType.Damage_Ability:
                player.playerCriticalDamage += 1;
                _maxValueNameText.text = "추가 공격력\n" +
                    player.playerCriticalDamage + " + 1";
                _damageText.text = "공격력 : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
                break;
            case AbilityType.Speed_Ability:
                player.moveMaxSpeed += 0.5f;
                _maxValueNameText.text = "최대 속도\n" +
                    player.moveMaxSpeed + " + 0.5";
                _speedText.text = "속도 : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
                break;
            case AbilityType.Power_Ability:
                player.attackMaxPower += 1;
                _maxValueNameText.text = "최대 사거리\n" +
                    player.attackMaxPower + " + 1";
                _attackPowerText.text = "공격사거리 : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
                break;
            case AbilityType.Breath_Ability:
                player.maxBreath += 1f;
                _maxValueNameText.text = "최대 잠수시간\n" +
                    (int)player.maxBreath + " + 1";
                _breathText.text = "잠수 시간 : " + (int)player.curBreath +
             " / " + (int)player.maxBreath;
                break;
            case AbilityType.Storage_Ability:
                player.fishEatCount += 1;
                _maxValueNameText.text = "식사량\n" +
                    player.fishEatCount + " + 1";
                break;
        }

        --player.LvPoint;
        _levelText.text = "Lv. " + player.PlayerLv + " ( " + player.LvPoint + " )";
    }
}
