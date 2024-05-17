using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform backGroundRect;
    // ��׶��� �̹���
    [SerializeField]
    RectTransform joyStickRect;
    // ���̽�ƽ �̹���
    //[SerializeField]
    //Transform Player;
    // ������ ĳ����

    //public Slider slider;

    Vector2 pos;
    // ������ ����
    float range;
    // ���̽�ƽ�� ������ ����

    //bool touchFlag = false;
    // ���� ���̽�ƽ�� ��������� Ȯ��

    public Vector2 inputDirection;

    void Start()
    {
        backGroundRect = gameObject.GetComponent<RectTransform>();
        // Ŭ���� �� Ŭ���� �� ��ǥ�� ������ ���� ���
        range = backGroundRect.rect.width * 0.5f;
        // ���̽�ƽ�� ������ ������ ��׶��� ũ���� ������ ����(������)
        // ���ϴ� ũ��� ���氡��
        // �̷��� ��׶��尡 ������ ��׶��� ũ�� ��ŭ ���� �ǰڳ�
    }

    void Update()
    {
        
    }

    private void InputControlVector()
    {
        //GameManager.OBJECT.player.PlayerMove(inputDirection);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    { // Ŭ���� ���¿��� ������ �� �Լ�
        // eventData�� ��ġ�Ǵ� Ŭ���ϴ� �̺�Ʈ ������ �����ִ�
        // ���⼭ ���� ��ǥ ������ �̿��� ���̽�ƽ�� ��ġ�� ������ �ش�
        pos = new Vector2(eventData.position.x - backGroundRect.position.x,
            eventData.position.y - backGroundRect.position.y);
        pos = Vector2.ClampMagnitude(pos, range);

        //pos.y = Mathf.Clamp(pos.y, slider.minValue, backGroundRect.rect.width * 0.5f);
        // slider�� minValue�� ���̽�ƽ�� ������ ������ŭ ���س��� �� ���Ϸ� ���������ʰ�

        joyStickRect.localPosition = pos;

        inputDirection = pos;


    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
