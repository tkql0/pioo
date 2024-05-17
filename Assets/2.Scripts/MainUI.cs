using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameStartPanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _gameStopButton;

    [SerializeField]
    private Text _experienceText;
    //[SerializeField]
    //private Text _healthText;
    [SerializeField]
    private Text _breathText;

    [SerializeField]
    private TextMeshProUGUI _fishCount;

    //[SerializeField]
    //private Slider _healthSlider;
    [SerializeField]
    private Slider _breathSlider;
    [SerializeField]
    private Slider _experienceSlider;

    [SerializeField]
    private GameObject _menuUI;
    [SerializeField]
    private RectTransform _statusUI;

    private bool _isMoveChangeChoose = false;

    private bool _isPlayerSwimmingChoose = false;

    private Color damageColor;
    private float damageTime;

    private void Start()
    {
        damageColor = new Color(1f, 0f, 0f, 0.1f);
        damageTime = 5f;
    }

    private void Update()
    {
        if (GameManager.UI._isStart || GameManager.OBJECT.player.isDie == true)
            SetGameStop();

        SetSliderDataUpdate(GameManager.OBJECT.player);

        LevelUp(GameManager.OBJECT.player);
        Damage(GameManager.OBJECT.player);
    }

    private void SetSliderDataUpdate(Player InPlayer)
    {
        //if (_healthSlider.value <= 0)
        //{
        //    _gameOverPanel.SetActive(true);
        //    InPlayer.isDie = true;
        //}

        if(InPlayer.isDie)
            _gameOverPanel.SetActive(true);

        //_healthText.text = (int)InPlayer.curHealth +
        //    " / " + InPlayer.maxHealth;
        _breathText.text = (int)InPlayer.curBreath +
            " / " + InPlayer.maxBreath;

        bool digestionMax = InPlayer.digestionCount >= InPlayer.digestionMaxCount ? true : false;
        bool fishItemMax = InPlayer.fishItemCount >= InPlayer.fishItemMaxCount ? true : false;

        if (digestionMax && fishItemMax)
        {
            _fishCount.text = "Fish\n" + "Max(" + InPlayer.digestionCount + ")" +
                " / " + "Max(" + InPlayer.fishItemCount + ")";
        }
        else if (!digestionMax && fishItemMax)
        {
            _fishCount.text = "Fish\n" + InPlayer.digestionCount +
                " / " + "Max(" + InPlayer.fishItemCount + ")";
        }
        else if (digestionMax && !fishItemMax)
        {
            _fishCount.text = "Fish\n" + "Max(" + InPlayer.digestionCount + ")" +
                " / " + InPlayer.fishItemCount;
        }
        else
        {
            _fishCount.text = "Fish\n" + InPlayer.digestionCount +
                " / " + InPlayer.fishItemCount;
        }

        //GameManager.UI.SetSliderUpdate(InPlayer.maxHealth, InPlayer.curHealth, _healthSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxBreath, InPlayer.curBreath, _breathSlider);
        GameManager.UI.SetSliderUpdate(InPlayer.maxExperience, InPlayer.curExperience, _experienceSlider);
    }

    public void OnClick()
    {
        if (!_gameStartPanel)
            return;

        GameManager.UI._isStart = false;
        SetGameStart();
        GameManager.SPAWN.GameStartSpawnPosition();

        //_healthSlider.value = GameManager.OBJECT.player.maxHealth;
        _breathSlider.value = GameManager.OBJECT.player.maxBreath;
        _experienceSlider.value = 0;

        _gameStartPanel.SetActive(false);

        GameManager.UI.isHPUpdate = true;
    }
    public void GameQuit()
    {
        Application.Quit();
        // 빌드에서만 사용
    }

    public void GameStop()
    {
        GameManager.UI.SetUIExit(_statusUI);
        _menuUI.SetActive(true);
        GameManager.UI._isStatusUpdate = false;

        SetGameStop();
    }

    public void GameStart()
    {
        _menuUI.SetActive(false);
        SetGameStart();
    }

    public void StatusUIEnter()
    {
        GameManager.UI._isStatusUpdate = true;
        GameManager.OBJECT.player.ExperienceUp();
        GameManager.UI.SetUIEnter(_statusUI);
        _menuUI.SetActive(false);
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LevelUp(Player InPlayer)
    {
        if (_experienceSlider.value != InPlayer.maxExperience)
            return;

        _experienceText.text = "Lv. " + GameManager.UI.GetLevelUpStat(InPlayer);
    }

    [SerializeField]
    private Image damageImage;

    public void Damage(Player InPlayer)
    {
        if(InPlayer.isDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageTime * Time.deltaTime);
        }

        InPlayer.isDamage = false;
    }

    public void PlayerSwimmingDown(Button InButton)
    {
        ColorBlock colorBlock = InButton.colors;

        colorBlock.normalColor = new Color(1, 0, 0, 1);
        colorBlock.highlightedColor = colorBlock.normalColor;
        colorBlock.pressedColor = colorBlock.normalColor;

        GameManager.OBJECT.player._isSwimmingJump = false;
        InButton.colors = colorBlock;
    }

    public void PlayerSwimmingUp(Button InButton)
    {
        ColorBlock colorBlock = InButton.colors;

        if (!GameManager.OBJECT.player.isSwimming)
        {
            // 이미지를 넣거나 색을 바꾸는게 좋을듯

            return;
        }

        colorBlock.normalColor = Color.white;
        colorBlock.highlightedColor = colorBlock.normalColor;
        colorBlock.pressedColor = colorBlock.normalColor;

        GameManager.OBJECT.player._isSwimmingJump = true;
        InButton.colors = colorBlock;
    }

    public void PlayerSwimming(Button InButton)
    {
        ColorBlock colorBlock = InButton.colors;

        if (!GameManager.OBJECT.player.isSwimming)
        { // 수영 중이 아닐 때 버튼이 눌리지 않게
            // 이미지를 넣거나 색을 바꾸는게 좋을듯

            return;
        }

        _isPlayerSwimmingChoose = !_isPlayerSwimmingChoose;

        colorBlock.normalColor = _isPlayerSwimmingChoose ? new Color(1, 0, 0, 1):Color.white;

        colorBlock.highlightedColor = colorBlock.normalColor;
        colorBlock.selectedColor = colorBlock.normalColor;

        GameManager.OBJECT.player._isSwimmingJump = _isPlayerSwimmingChoose ? false : true;
        InButton.colors = colorBlock;
    }

    public void PlayerMoveChange(Button InButton)
    {
        ColorBlock colorBlock = InButton.colors;

        Player player = GameManager.OBJECT.player;
        _isMoveChangeChoose = !_isMoveChangeChoose;

        colorBlock.normalColor = _isMoveChangeChoose ? new Color(1, 0, 0, 1) : Color.white;
        player._isMoveChangeTest = _isMoveChangeChoose ? true : false;

        colorBlock.highlightedColor = colorBlock.normalColor;
        colorBlock.selectedColor = colorBlock.normalColor;

        InButton.colors = colorBlock;
    }

    private void SetGameStop() => Time.timeScale = 0;
    private void SetGameStart() => Time.timeScale = 1;
}