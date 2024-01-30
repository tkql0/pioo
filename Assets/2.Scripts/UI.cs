using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject GameStart_Panel;
    [SerializeField]
    GameObject GameOver_Panel;

    [SerializeField]
    GameObject Stop_Button;

    //public RectTransform stats_ui;

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
            //enter();
            Click_count = true;
            Time.timeScale = 0;
        }
        else
        {
            //exit();
            Click_count = false;
            Time.timeScale = 1;
        }
    }

    //void enter()
    //{
    //    stats_ui.anchoredPosition = Vector2.zero;
    //}

    //void exit()
    //{
    //    stats_ui.anchoredPosition = Vector2.up * 2000;
    //}
}
