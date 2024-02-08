using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameStartPanel;

    [SerializeField]
    private GameObject _gameStopButton;

    public Text experienceTxt;
    public Text healthTxt;
    public Text breathTxt;

    public Slider healthSlider;
    public Slider breathSlider;
    public Slider experienceSlider;

    public RectTransform menu_ui;

    public bool isClick = false;

    private void Update()
    {
        ObjectController _objectController = GameManager.OBJECT;

        if (_gameStartPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (!isClick)
                Time.timeScale = 1;
        }

        if (healthSlider.value <= 0)
        {
            _objectController.player.isDie = true;
            Time.timeScale = 0;
        }

        if (experienceSlider.value == 100 && _objectController.player.isLv_up == false)
        {
            _objectController.player.isLv_up = true;
            Lv_Up();
        }
        healthTxt.text = (int)_objectController.player.curHealth + " / " + _objectController.player.maxHealth;
        breathTxt.text = (int)_objectController.player.curBreath + " / " + _objectController.player.maxBreath;

        healthSlider.maxValue = _objectController.player.maxHealth;
        breathSlider.maxValue = _objectController.player.maxBreath;
        experienceSlider.maxValue = _objectController.player.maxExperience;

        healthSlider.value = _objectController.player.curHealth;
        breathSlider.value = _objectController.player.curBreath;
        experienceSlider.value = _objectController.player.curExperience;
    }

    private void OnEnable()
    {
        
    }

    public void OnClick()
    {
        ObjectController _objectController = GameManager.OBJECT;
        SpawnController _spawnController = GameManager.SPAWN;

        if (_gameStartPanel)
        {
            _spawnController.GameStartSpawnPosition();

            healthSlider.value = _objectController.player.maxHealth;
            breathSlider.value = _objectController.player.maxBreath;
            experienceSlider.value = 0;
            _objectController.player.isDie = false;

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
    private void Lv_Up()
    {
        ObjectController _objectController = GameManager.OBJECT;

        _objectController.player.curExperience = 0;
        _objectController.player.PlayerLv++;
        experienceTxt.text = "Lv. " + GameManager.OBJECT.player.PlayerLv;
        _objectController.player.isLv_up = false;
        _objectController.player.maxHealth += 2;
    }

    private void Enter()
    {
        menu_ui.anchoredPosition = Vector2.zero;
    }

    private void Exit()
    {
        menu_ui.anchoredPosition = Vector2.up * 2000;
    }
}
