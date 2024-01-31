using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject GameStart_Panel;

    [SerializeField]
    private GameObject Stop_Button;

    public Text experienceTxt;
    public Text healthTxt;
    public Text breathTxt;

    public Slider healthSlider;
    public Slider breathSlider;
    public Slider experienceSlider;

    public RectTransform menu_ui;

    public bool Click_count = false;

    private void Update()
    {
        if (GameStart_Panel.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (!Click_count)
                Time.timeScale = 1;
        }

        if (healthSlider.value <= 0)
        {
            GameTree.GAME.objectController.player.isDie = true;
            Time.timeScale = 0;
        }

        if (experienceSlider.value == 100 && GameTree.GAME.objectController.player.isLv_up == false)
        {
            GameTree.GAME.objectController.player.isLv_up = true;
            Lv_Up();
        }
        healthTxt.text = (int)GameTree.GAME.objectController.player.curHealth + " / " + GameTree.GAME.objectController.player.maxHealth;
        breathTxt.text = (int)GameTree.GAME.objectController.player.curBreath + " / " + GameTree.GAME.objectController.player.maxBreath;

        healthSlider.maxValue = GameTree.GAME.objectController.player.maxHealth;
        breathSlider.maxValue = GameTree.GAME.objectController.player.maxBreath;
        experienceSlider.maxValue = GameTree.GAME.objectController.player.maxExperience;

        healthSlider.value = GameTree.GAME.objectController.player.curHealth;
        breathSlider.value = GameTree.GAME.objectController.player.curBreath;
        experienceSlider.value = GameTree.GAME.objectController.player.curExperience;
    }

    private void OnEnable()
    {
        healthSlider.value = GameTree.GAME.objectController.player.maxHealth;
        breathSlider.value = GameTree.GAME.objectController.player.maxBreath;
        experienceSlider.value = 0;
    }

    public void OnClick()
    {
        if (GameStart_Panel == true)
        {
            //GameTree.UI.player++;
            GameStart_Panel.SetActive(false);
        }
    }

    public void OnStop()
    {
        if (Click_count == false)
        {
            enter();
            Click_count = true;
            Time.timeScale = 0;
        }
        else
        {
            exit();
            Click_count = false;
            Time.timeScale = 1;
        }
    }
    private void Lv_Up()
    {
        GameTree.GAME.objectController.player.curExperience = 0;
        GameTree.GAME.objectController.player.PlayerLv++;
        experienceTxt.text = "Lv. " + GameTree.GAME.objectController.player.PlayerLv;
        GameTree.GAME.objectController.player.isLv_up = false;
        GameTree.GAME.objectController.player.maxHealth += 2;
    }

    void enter()
    {
        menu_ui.anchoredPosition = Vector2.zero;
    }

    void exit()
    {
        menu_ui.anchoredPosition = Vector2.up * 2000;
    }
}
