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
            GameManager.OBJECT.player.isDie = true;
            Time.timeScale = 0;
        }

        if (experienceSlider.value == 100 && GameManager.OBJECT.player.isLv_up == false)
        {
            GameManager.OBJECT.player.isLv_up = true;
            Lv_Up();
        }
        healthTxt.text = (int)GameManager.OBJECT.player.curHealth + " / " + GameManager.OBJECT.player.maxHealth;
        breathTxt.text = (int)GameManager.OBJECT.player.curBreath + " / " + GameManager.OBJECT.player.maxBreath;

        healthSlider.maxValue = GameManager.OBJECT.player.maxHealth;
        breathSlider.maxValue = GameManager.OBJECT.player.maxBreath;
        experienceSlider.maxValue = GameManager.OBJECT.player.maxExperience;

        healthSlider.value = GameManager.OBJECT.player.curHealth;
        breathSlider.value = GameManager.OBJECT.player.curBreath;
        experienceSlider.value = GameManager.OBJECT.player.curExperience;
    }

    private void OnEnable()
    {
        healthSlider.value = GameManager.OBJECT.player.maxHealth;
        breathSlider.value = GameManager.OBJECT.player.maxBreath;
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
        GameManager.OBJECT.player.curExperience = 0;
        GameManager.OBJECT.player.PlayerLv++;
        experienceTxt.text = "Lv. " + GameManager.OBJECT.player.PlayerLv;
        GameManager.OBJECT.player.isLv_up = false;
        GameManager.OBJECT.player.maxHealth += 2;
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
