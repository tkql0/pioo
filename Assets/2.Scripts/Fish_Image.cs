using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish_Image : MonoBehaviour
{
    public Sprite[] boneFish;
    private Image fishImage;

    private void Awake()
    {
        fishImage = GetComponent<Image>();
    }

    private void Update()
    {
        SetHealthImage();
    }

    public void SetHealthImage()
    {
        int fishNumber = (int)GameManager.OBJECT.player.DigestionTime / 2;

        if(GameManager.OBJECT.player.fishItemCount <= 0 )
        {
            fishImage.sprite = boneFish[11];
            return;
        }

        switch (fishNumber)
        {
            case 0:
                fishImage.sprite = boneFish[0];
                break;
            case 1:
                fishImage.sprite = boneFish[1];
                break;
            case 2:
                fishImage.sprite = boneFish[2];
                break;
            case 3:
                fishImage.sprite = boneFish[3];
                break;
            case 4:
                fishImage.sprite = boneFish[4];
                break;
            case 5:
                fishImage.sprite = boneFish[5];
                break;
            case 6:
                fishImage.sprite = boneFish[6];
                break;
            case 7:
                fishImage.sprite = boneFish[7];
                break;
            case 8:
                fishImage.sprite = boneFish[8];
                break;
            case 9:
                fishImage.sprite = boneFish[9];
                break;
            case 10:
                fishImage.sprite = boneFish[10];
                break;
            case 11:
                fishImage.sprite = boneFish[11];
                break;
        }
    }
}
