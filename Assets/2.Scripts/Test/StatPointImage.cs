using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatPointImages
{
    Empty = 0,
    Stack1,
    Stack2,
    Stack3,
    Stack4,
    Stack5,
}

public class StatPointImage : MonoBehaviour
{
    public Sprite[] statPoint;
    private Image statPointImage;

    private void Awake()
    {
        statPointImage = GetComponent<Image>();
    }

    public void SetStatPointImage(StatPointImages InImages)
    {
        switch (InImages)
        {
            case StatPointImages.Stack1:
                statPointImage.sprite = statPoint[1];
                break;
            case StatPointImages.Stack2:
                statPointImage.sprite = statPoint[2];
                break;
            case StatPointImages.Stack3:
                statPointImage.sprite = statPoint[3];
                break;
            case StatPointImages.Stack4:
                statPointImage.sprite = statPoint[4];
                break;
            case StatPointImages.Stack5:
                statPointImage.sprite = statPoint[5];
                break;
        }
    }
}
