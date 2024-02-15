using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameStartPanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _gameStopButton;

    [SerializeField]
    private Text experienceTxt;
    [SerializeField]
    private Text healthTxt;
    [SerializeField]
    private Text breathTxt;

    [SerializeField]
    private Text FishCount;

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider breathSlider;
    [SerializeField]
    private Slider experienceSlider;

    [SerializeField]
    private RectTransform menu_ui;

    public bool isClick = false;
    private bool isStart = true;

    private void Update()
    {
        if (isStart || GameManager.OBJECT.player.isDie == true)
            SetGameStop();

        SetDataUpdate(GameManager.OBJECT.player);

        LevelUp(GameManager.OBJECT.player);
    }

    private void SetDataUpdate(Player InPlayer)
    {
        if (healthSlider.value <= 0)
        {
            _gameOverPanel.SetActive(true);
            InPlayer.isDie = true;
        }

        healthTxt.text = (int)InPlayer.curHealth +
            " / " + InPlayer.maxHealth;
        breathTxt.text = (int)InPlayer.curBreath +
            " / " + InPlayer.maxBreath;

        FishCount.text = "Fish\n" + InPlayer.fishItemCount +
            " / " + InPlayer.fishItemMaxCount;

        GameManager.UI.SetSliderUpdate(InPlayer.maxHealth, InPlayer.curHealth, healthSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxBreath, InPlayer.curBreath, breathSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxExperience, InPlayer.curExperience, experienceSlider);
    }

    public void OnClick()
    {
        if (!_gameStartPanel)
            return;

        isStart = false;
        SetGameStart();
        GameManager.SPAWN.GameStartSpawnPosition();

        healthSlider.value = GameManager.OBJECT.player.maxHealth;
        breathSlider.value = GameManager.OBJECT.player.maxBreath;
        experienceSlider.value = 0;

        _gameStartPanel.SetActive(false);
    }

    public void OnStop()
    {
        if (isClick == false)
        {
            GameManager.UI.SetUIEnter(menu_ui);
            SetGameStop();
            isClick = true;
        }
        else
        {
            GameManager.UI.SetUIExit(menu_ui);
            SetGameStart();
            isClick = false;
        }
    }

    public void OnReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LevelUp(Player InPlayer)
    {
        if (experienceSlider.value != InPlayer.maxExperience)
            return;

        InPlayer.curExperience = 0;
        InPlayer.PlayerLv++;
        experienceTxt.text = "Lv. " + InPlayer.PlayerLv;
        InPlayer.maxHealth += 2;
        InPlayer.maxExperience += 5;
    }

    private void SetGameStop() => Time.timeScale = 0;
    private void SetGameStart() => Time.timeScale = 1;
}
