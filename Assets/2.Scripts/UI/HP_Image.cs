using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HealthImages
{
    Empty = 0,
    Half,
    Full,
}

public class HP_Image : MonoBehaviour
{
    public Sprite fullHealth, halfHealth, emptyHealth;
    private Image healthImage;

    private void Awake()
    {
        healthImage = GetComponent<Image>();
    }

    public void SetHealthImage(HealthImages InImages)
    {
        switch (InImages)
        {
            case HealthImages.Empty:
                healthImage.sprite = emptyHealth;
                break;
            case HealthImages.Half:
                healthImage.sprite = halfHealth;
                break;
            case HealthImages.Full:
                healthImage.sprite = fullHealth;
                break;
        }
    }
}
