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
    private Text _experienceTxt;
    [SerializeField]
    private Text _healthTxt;
    [SerializeField]
    private Text _breathTxt;

    [SerializeField]
    private Text _fishCount;

    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Slider _breathSlider;
    [SerializeField]
    private Slider _experienceSlider;

    [SerializeField]
    private RectTransform _menuUI;

    private bool _isClick = false;
    private bool _isStart = true;

    private void Update()
    {
        if (_isStart || GameManager.OBJECT.player.isDie == true)
            SetGameStop();

        SetSliderDataUpdate(GameManager.OBJECT.player);

        LevelUp(GameManager.OBJECT.player);
    }

    private void SetSliderDataUpdate(Player InPlayer)
    {
        if (_healthSlider.value <= 0)
        {
            _gameOverPanel.SetActive(true);
            InPlayer.isDie = true;
        }

        _healthTxt.text = (int)InPlayer.curHealth +
            " / " + InPlayer.maxHealth;
        _breathTxt.text = (int)InPlayer.curBreath +
            " / " + InPlayer.maxBreath;

        _fishCount.text = "Fish\n" + InPlayer.fishItemCount +
            " / " + InPlayer.fishItemMaxCount;

        GameManager.UI.SetSliderUpdate(InPlayer.maxHealth, InPlayer.curHealth, _healthSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxBreath, InPlayer.curBreath, _breathSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxExperience, InPlayer.curExperience, _experienceSlider);
    }

    private void Stat()
    {

    }

    public void OnClick()
    {
        if (!_gameStartPanel)
            return;

        _isStart = false;
        SetGameStart();
        GameManager.SPAWN.GameStartSpawnPosition();
        GameManager.SPAWN.TestGameStartSpawnPosition();

        _healthSlider.value = GameManager.OBJECT.player.maxHealth;
        _breathSlider.value = GameManager.OBJECT.player.maxBreath;
        _experienceSlider.value = 0;

        _gameStartPanel.SetActive(false);
    }

    public void OnStop()
    {
        if (!_isClick)
        {
            GameManager.UI.SetUIEnter(_menuUI);
            SetGameStop();
            _isClick = true;
        }
        else
        {
            GameManager.UI.SetUIExit(_menuUI);
            SetGameStart();
            _isClick = false;
        }
    }

    public void OnReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LevelUp(Player InPlayer)
    {
        if (_experienceSlider.value != InPlayer.maxExperience)
            return;

        _experienceTxt.text = "Lv. " + GameManager.UI.GetLevelUpStat(InPlayer);
    }

    private void SetGameStop() => Time.timeScale = 0;
    private void SetGameStart() => Time.timeScale = 1;
}