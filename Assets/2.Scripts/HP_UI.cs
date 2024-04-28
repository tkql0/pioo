using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    public int curHealth;
    public int MaxHealth;

    public Image[] healths;

    public Sprite fullHealth;
    // 2
    public Sprite halfHealth;
    // 1
    public Sprite emptyHealth;
    // 0

    // MaxHealth가 20을 넘기면 뒤에 X 1 을 붙이자
    // curHealth가 20단위로 넘어가면 색을 바꾸자

    private void Update()
    {
        if(curHealth > MaxHealth)
        {
            curHealth = MaxHealth;
        }

        for(int i = 0; i < healths.Length; i++)
        {
            if(i < curHealth)
            {
                healths[i].sprite = fullHealth;
            }
            else
            {
                healths[i].sprite = emptyHealth;
            }

            if(i < MaxHealth)
            {
                healths[i].enabled = true;
            }
            else
            {
                healths[i].enabled = false;
            }
        }
    }
}
