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
        // ����� ������Ʈ�� �ƴ϶� �޴��� ������ �ٲٰ� �ϴ°� ��������
    }
}
