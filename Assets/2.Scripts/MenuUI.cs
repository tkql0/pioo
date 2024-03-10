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

        _healthText.text = "ü�� : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
        _damageText.text = "���ݷ� : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
        _speedText.text = "�ӵ� : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
        _attackPowerText.text = "���ݻ�Ÿ� : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
        _breathText.text = "��� �ð� : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
        _fishStorageText.text = "����� : " + player.fishItemMaxCount +
            " / �Ļ緮 : " + player.fishEatCount;
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

                _abilityValueNameText.text = "ü��";
                _minValueNameText.text = "���� ü��\n" +
                    (int)player.curHealth + " + 2";
                _maxValueNameText.text = "�ִ� ü��\n" +
                    (int)player.maxHealth + " + 2";
                _explanationText.text = "test : ���� ü�¿� ����Ʈ�� ����ϴ���\n" +
                    "�ִ� ü���� ���� �� �����ϴ�.";
                break;
            case (int)AbilityType.Damage_Ability:
                abilityType = AbilityType.Damage_Ability;

                _abilityValueNameText.text = "���ݷ�";
                _minValueNameText.text = "���ݷ�\n" +
                    player.playerDamage + " + 1";
                _maxValueNameText.text = "�߰� ���ݷ�\n" +
                    player.playerCriticalDamage + " + 1";
                _explanationText.text = "test : �߰� ���ݷ��� ġ��Ÿ��\n" +
                    "�����ϴ� ��ġ�Դϴ�.";
                break;
            case (int)AbilityType.Speed_Ability:
                abilityType = AbilityType.Speed_Ability;

                _abilityValueNameText.text = "�ӵ�";

                if ((int)player.moveSpeed == (int)player.moveMaxSpeed)
                    _minValueNameText.text = "���� �ӵ�\n" + "Max";
                else
                    _minValueNameText.text = "���� �ӵ�\n" +
                        (int)player.moveSpeed + " + 0.5";

                _maxValueNameText.text = "�ִ� �ӵ�\n" +
                    (int)player.moveMaxSpeed + " + 0.5";
                _explanationText.text = "test : ���� �ӵ��� ����Ʈ�� ����ϴ���\n" +
                    "�ִ� �ӵ��� ���� �� �����ϴ�.";
                break;
            case (int)AbilityType.Power_Ability:
                abilityType = AbilityType.Power_Ability;

                _abilityValueNameText.text = "��Ÿ�";

                if((int)player.attackMinPower == (int)player.attackMaxPower)
                    _minValueNameText.text = "���� ��Ÿ�\n" + "Max";
                else
                    _minValueNameText.text = "���� ��Ÿ�\n" +
                        (int)player.attackMinPower + " + 1";

                _maxValueNameText.text = "�ִ� ��Ÿ�\n" +
                    (int)player.attackMaxPower + " + 1";
                _explanationText.text = "test : ���� ��Ÿ��� ����Ʈ�� ����ϴ���\n" +
                    "�ִ� ��Ÿ��� ���� �� �����ϴ�.";
                break;
            case (int)AbilityType.Breath_Ability:
                abilityType = AbilityType.Breath_Ability;

                _abilityValueNameText.text = "����ð�";
                _minValueNameText.text = "���� ����ð�\n" +
                    (int)player.curBreath + " + 1";
                _maxValueNameText.text = "�ִ� ����ð�\n" +
                    (int)player.maxBreath + " + 1";
                _explanationText.text = "test : ���� ����ð��� ����Ʈ�� ����ϴ���\n" +
                    "�ִ� ����ð��� ���� �� �����ϴ�.";
                break;
            case (int)AbilityType.Storage_Ability:
                abilityType = AbilityType.Storage_Ability;

                _abilityValueNameText.text = "�����";
                _minValueNameText.text = "�������\n" +
                    player.fishItemMaxCount + " + 1";
                _maxValueNameText.text = "�Ļ緮\n" +
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

                _minValueNameText.text = "���� ü��\n" +
                    (int)player.curHealth + " + 2";
                _healthText.text = "ü�� : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
                break;
            case AbilityType.Damage_Ability:
                player.playerDamage += 1;
                _minValueNameText.text = "���ݷ�\n" +
                    player.playerDamage + " + 1";
                _damageText.text = "���ݷ� : " + player.playerDamage + " / " +
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
                _minValueNameText.text = "���� �ӵ�\n" +
                    player.moveSpeed + " + 0.5";
                }
                _speedText.text = "�ӵ� : " + (int)player.moveSpeed + " / " +
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
                    _minValueNameText.text = "���� ��Ÿ�\n" +
                        player.attackMinPower + " + 1";
                }
                _attackPowerText.text = "���ݻ�Ÿ� : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
                break;
            case AbilityType.Breath_Ability:
                if (player.curBreath >= player.maxBreath)
                    player.curBreath = player.maxBreath;
                else
                    player.curBreath += 1f;

                _minValueNameText.text = "���� ����ð�\n" +
                    (int)player.curBreath + " + 1";
                _breathText.text = "��� �ð� : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
                break;
            case AbilityType.Storage_Ability:
                player.fishItemMaxCount += 1;
                _minValueNameText.text = "�������\n" +
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
                _maxValueNameText.text = "�ִ� ü��\n" +
                    (int)player.maxHealth + " + 2";
                _healthText.text = "ü�� : " + (int)player.curHealth +
            " / " + (int)player.maxHealth;
                break;
            case AbilityType.Damage_Ability:
                player.playerCriticalDamage += 1;
                _maxValueNameText.text = "�߰� ���ݷ�\n" +
                    player.playerCriticalDamage + " + 1";
                _damageText.text = "���ݷ� : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
                break;
            case AbilityType.Speed_Ability:
                player.moveMaxSpeed += 0.5f;
                _maxValueNameText.text = "�ִ� �ӵ�\n" +
                    player.moveMaxSpeed + " + 0.5";
                _speedText.text = "�ӵ� : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
                break;
            case AbilityType.Power_Ability:
                player.attackMaxPower += 1;
                _maxValueNameText.text = "�ִ� ��Ÿ�\n" +
                    player.attackMaxPower + " + 1";
                _attackPowerText.text = "���ݻ�Ÿ� : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
                break;
            case AbilityType.Breath_Ability:
                player.maxBreath += 1f;
                _maxValueNameText.text = "�ִ� ����ð�\n" +
                    (int)player.maxBreath + " + 1";
                _breathText.text = "��� �ð� : " + (int)player.curBreath +
             " / " + (int)player.maxBreath;
                break;
            case AbilityType.Storage_Ability:
                player.fishEatCount += 1;
                _maxValueNameText.text = "�Ļ緮\n" +
                    player.fishEatCount + " + 1";
                break;
        }

        --player.LvPoint;
        _levelText.text = "Lv. " + player.PlayerLv + " ( " + player.LvPoint + " )";
    }
}
