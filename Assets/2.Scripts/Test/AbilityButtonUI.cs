using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _abilityRect;
    // ���Ʒ��� ������ �ٲܰ�

    private RectTransform _myRect;
    // Ŭ���� ��Ȱ��ȭ �� �ڱ��ڽ�

    //private bool choice;

    public int myKey;
    // ���� ��ũ��Ʈ���� ��Ȱ��ȭ�� UI�� Ȱ��ȭ ��ų�� �ʿ��� key��
    // �����غ��ϱ� ���� ��Ȱ��ȭ ��ų �ʿ� ����?

    private void Start()
    {
        _myRect = GetComponent<RectTransform>();
        //choice = false;

        GameManager.UI.abilityUI[myKey] = _abilityRect;
    }

    public void ChoiceButton()
    {
        GameManager.UI.AbilityButtonClick(myKey);
    }
}
