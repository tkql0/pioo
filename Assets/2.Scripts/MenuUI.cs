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

        _healthText.text = "ü�� : " + (int)player.curHealth + " / " +
            (int)player.maxHealth;
        _damageText.text = "���ݷ� : " + player.playerDamage + " / " +
            (player.playerDamage + player.playerCriticalDamage);
        _speedText.text = "�ӵ� : " + (int)player.moveSpeed + " / " +
            (int)player.moveMaxSpeed;
        _attackPowerText.text = "���ݻ�Ÿ� : " + (int)player.attackMinPower +
            " / " + (int)player.attackMaxPower;
        _breathText.text = "��� �ð� : " + (int)player.curBreath +
            " / " + (int)player.maxBreath;
        _fishStorageText.text = "����� ����� ���� : " + player.fishItemMaxCount +
            " / ��뷮 : " + player.digestionMaxCount;

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

                if (GameManager.UI.SetAbilityMax(player.moveSpeed, player.moveMaxSpeed))
                    _minValueNameText.text = "���� �ӵ�\nMax";
                else
                    _minValueNameText.text = "���� �ӵ�\n" +
                        player.moveSpeed + " + 0.5";

                _maxValueNameText.text = "�ִ� �ӵ�\n" +
                    player.moveMaxSpeed + " + 0.5";
                _explanationText.text = "test : ���� �ӵ��� ����Ʈ�� ����ϴ���\n" +
                    "�ִ� �ӵ��� ���� �� �����ϴ�.";
                break;
            case (int)AbilityType.Power_Ability:
                abilityType = AbilityType.Power_Ability;

                _abilityValueNameText.text = "��Ÿ�";

                if (GameManager.UI.SetAbilityMax(player.attackMinPower, player.attackMaxPower))
                    _minValueNameText.text = "���� ��Ÿ�\nMax";
                else
                    _minValueNameText.text = "���� ��Ÿ�\n" +
                        player.attackMinPower + " + 1";

                _maxValueNameText.text = "�ִ� ��Ÿ�\n" +
                    player.attackMaxPower + " + 1";
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
                _minValueNameText.text = "��뷮\n" +
                    player.digestionMaxCount + " + 1";
                _maxValueNameText.text = "�������\n" +
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
                    _minValueNameText.text = "���� ü��\nMax";
                    return;
                }
                InPlayer.curHealth += 2f;
                _minValueNameText.text = "���� ü��\n" +
                    (int)InPlayer.curHealth + " + 2";
                GameManager.UI.isHPUpdate = true;
                break;
            case AbilityType.Damage_Ability:
                InPlayer.playerDamage += 1;
                _minValueNameText.text = "���ݷ�\n" +
                    InPlayer.playerDamage + " + 1";
                break;
            case AbilityType.Speed_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.moveSpeed, InPlayer.moveMaxSpeed))
                {
                    SetOverFlowValue(InPlayer.moveSpeed, InPlayer.moveMaxSpeed);
                    _minValueNameText.text = "���� �ӵ�\nMax";
                    return;
                }
                InPlayer.moveSpeed += 0.5f;
                _minValueNameText.text = "���� �ӵ�\n" +
                    InPlayer.moveSpeed + " + 0.5";
                break;
            case AbilityType.Power_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.attackMinPower, InPlayer.attackMaxPower))
                {
                    SetOverFlowValue(InPlayer.attackMinPower, InPlayer.attackMaxPower);
                    _minValueNameText.text = "���� ��Ÿ�\nMax";
                    return;
                }
                InPlayer.attackMinPower += 1f;
                _minValueNameText.text = "���� ��Ÿ�\n" +
                    InPlayer.attackMinPower + " + 1";
                break;
            case AbilityType.Breath_Ability:

                if (GameManager.UI.SetAbilityMax(InPlayer.curBreath, InPlayer.maxBreath))
                {
                    SetOverFlowValue(InPlayer.curBreath, InPlayer.maxBreath);
                    _minValueNameText.text = "���� ����ð�\nMax";
                    return;
                }
                InPlayer.curBreath += 1f;
                _minValueNameText.text = "���� ����ð�\n" +
                    (int)InPlayer.curBreath + " + 1";
                break;
            case AbilityType.Storage_Ability:
                if (GameManager.UI.SetAbilityMax(InPlayer.fishEatCount, InPlayer.fishItemMaxCount))
                {
                    SetOverFlowValue(InPlayer.digestionMaxCount, InPlayer.fishItemMaxCount);
                    _minValueNameText.text = "�Ļ緮\nMax";
                    return;
                }
                InPlayer.digestionMaxCount += 1;
                _minValueNameText.text = "�Ļ緮\n" +
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
                    _maxValueNameText.text = "�ִ� ü��\nMax";
                    return;
                }
                InPlayer.maxHealth += 2f;
                _maxValueNameText.text = "�ִ� ü��\n" +
                    (int)InPlayer.maxHealth + " + 2";
                GameManager.UI.isHPUpdate = true;

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "���� ü��\n" +
                        (int)InPlayer.curHealth + " + 2";
                break;
            case AbilityType.Damage_Ability:
                InPlayer.playerCriticalDamage += 1;
                _maxValueNameText.text = "�߰� ���ݷ�\n" +
                    InPlayer.playerCriticalDamage + " + 1";
                    break;
            case AbilityType.Speed_Ability:
                if(InPlayer.moveMaxSpeed >= 25)
                {
                    _maxValueNameText.text = "���� ��Ÿ�\nMax";
                    return;
                }

                InPlayer.moveMaxSpeed += 0.5f;
                _maxValueNameText.text = "�ִ� �ӵ�\n" +
                    InPlayer.moveMaxSpeed + " + 0.5";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "���� �ӵ�\n" +
                        InPlayer.moveSpeed + " + 0.5";
                break;
            case AbilityType.Power_Ability:
                InPlayer.attackMaxPower += 1;
                _maxValueNameText.text = "�ִ� ��Ÿ�\n" +
                    InPlayer.attackMaxPower + " + 1";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "���� ��Ÿ�\n" +
                        InPlayer.attackMinPower + " + 1";
                break;
            case AbilityType.Breath_Ability:
                InPlayer.maxBreath += 1f;
                _maxValueNameText.text = "�ִ� ����ð�\n" +
                    (int)InPlayer.maxBreath + " + 1";

                if (GameManager.UI._isMaxValue == true)
                    _minValueNameText.text = "���� ����ð�\n" +
                        (int)InPlayer.curBreath + " + 1";
                break;
            case AbilityType.Storage_Ability:
                InPlayer.fishItemMaxCount += 1;
                _maxValueNameText.text = "�������\n" +
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