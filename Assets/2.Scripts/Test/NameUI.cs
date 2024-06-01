using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField]
    private string _playerNameText;

    [SerializeField]
    private TextMeshProUGUI _playerLevelText;

    private void OnEnable()
    {
        _playerNameText = "Olxrota";
    }

    private void Update()
    {
        Player player = GameManager.OBJECT.player;

        _playerLevelText.text = " Lv. " + player.PlayerLv + " - " + _playerNameText + " ( " + player.LvPoint + " )";
        // 여기는 업데이트가 아니라 메뉴로 들어오면 바꾸게 하는게 좋으려나
    }
}
