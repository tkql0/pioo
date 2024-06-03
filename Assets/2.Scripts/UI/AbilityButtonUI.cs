using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _abilityRect;
    // ���Ʒ��� ������ �ٲܰ�

    private RectTransform _myRect;
    // Ŭ���� ��Ȱ��ȭ �� �ڱ��ڽ�

    public int myKey;
    // ���� ��ũ��Ʈ���� ��Ȱ��ȭ�� UI�� Ȱ��ȭ ��ų�� �ʿ��� key��
    // �����غ��ϱ� ���� ��Ȱ��ȭ ��ų �ʿ� ����?

    private void OnEnable()
    {
        _myRect = GetComponent<RectTransform>();

        GameManager.UI.abilityUI[myKey] = _abilityRect;
    }

    public void ChoiceButton()
    {
        GameManager.UI.AbilityButtonClick(myKey);
    }
    // ��Ȱ��ȭ�ϰų� ���� �ٲٰų�
    // �ϰ������ ü�� ��ư�� ������ ��ư�� ������� ��ư�̹����� ����ǳڷ� �ٲ����� �ϴµ�
}
