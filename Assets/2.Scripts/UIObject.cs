using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameStartPanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _gameStopButton;

    public Text experienceTxt;
    public Text healthTxt;
    public Text breathTxt;

    public Text FishCount;

    public Slider healthSlider;
    public Slider breathSlider;
    public Slider experienceSlider;

    public RectTransform menu_ui;

    public bool isClick = false;
    private bool isStart = true;

    private void Update()
    {
        if (isStart || GameManager.OBJECT.player.isDie)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        SetDataUpdate(GameManager.OBJECT.player);

        OnLevelUp(GameManager.OBJECT.player);
    }

    private void SetDataUpdate(Player InPlayer)
    {
        if (healthSlider.value <= 0)
            InPlayer.isDie = true;

        healthTxt.text = (int)InPlayer.curHealth +
            " / " + InPlayer.maxHealth;
        breathTxt.text = (int)InPlayer.curBreath +
            " / " + InPlayer.maxBreath;

        FishCount.text = "Fish\n" +(int)InPlayer.fishItemCount +
            " / " + InPlayer.fishItemMaxCount;

        SetSliderUpdate(InPlayer.maxHealth, InPlayer.curHealth, healthSlider);
        SetSliderUpdate(InPlayer.maxBreath, InPlayer.curBreath, breathSlider);
        SetSliderUpdate(InPlayer.maxExperience, InPlayer.curExperience, experienceSlider);
    }

    public void OnClick()
    {
        if (_gameStartPanel)
        {
            isStart = false;
            GameManager.SPAWN.GameStartSpawnPosition();

            healthSlider.value = GameManager.OBJECT.player.maxHealth;
            breathSlider.value = GameManager.OBJECT.player.maxBreath;
            experienceSlider.value = 0;

            _gameStartPanel.SetActive(false);
        }
    }

    public void OnStop()
    {
        if (isClick == false)
        {
            Enter();
            isClick = true;
            Time.timeScale = 0;
        }
        else
        {
            Exit();
            isClick = false;
            Time.timeScale = 1;
        }
    }
    private void OnLevelUp(Player InPlayer)
    {
        if (experienceSlider.value != InPlayer.maxExperience)
            return;

        InPlayer.curExperience = 0;
        InPlayer.PlayerLv++;
        experienceTxt.text = "Lv. " + InPlayer.PlayerLv;
        InPlayer.maxHealth += 2;
        InPlayer.maxExperience += 5;
    }

    private void Enter()
    {
        menu_ui.anchoredPosition = Vector2.zero;
    }

    private void Exit()
    {
        menu_ui.anchoredPosition = Vector2.up * 2000;
    }

    private void SetSliderUpdate(float InMaxValue, float InValue, Slider InSlider)
    {
        InSlider.maxValue = InMaxValue;
        InSlider.value = InValue;
    }
}
