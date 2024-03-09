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

    //int minValue = 0;
    //int maxValue = 0;

    //int playerLv = 0;
    //int playerLvPoint = 0;

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
    private Text _levelText;

    public void PlayerStat(Player InPlayer)
    {
        _healthText.text = (int)InPlayer.curHealth +
            " / " + (int)InPlayer.maxHealth;
        _damageText.text = InPlayer.damage + " / " +
            (InPlayer.damage + InPlayer.playerCriticalDamage);
        _speedText.text = (int)InPlayer.moveSpeed + " / " +
            ((int)InPlayer.moveSpeed + (int)InPlayer.moveAddSpeed);
        _attackPowerText.text = (int)InPlayer.attackMinPower +
            " / " + (int)InPlayer.attackMaxPower;
        _breathText.text = (int)InPlayer.curBreath +
            " / " + (int)InPlayer.maxBreath;
    }
}
