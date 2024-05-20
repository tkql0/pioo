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

    public Slider slider;

    public Vector2 pos;
    // ������ ����
    float range;
    // ���̽�ƽ�� ������ ����

    float limitRange;

    Vector2 inputDirection;

    bool _isTouch = false;

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
        if (_isTouch)
            InputControlVector();

        LimitRange();
    }

    private void InputControlVector()
    {
        GameManager.OBJECT.player.PlayerMove(inputDirection);
    }

    private void LimitRange()
    {
        limitRange = -range + range * 2 / slider.maxValue * (slider.maxValue - slider.value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouch = true;
    }

    public void OnDrag(PointerEventData eventData)
    { // Ŭ���� ���¿��� ������ �� �Լ�
        // eventData�� ��ġ�Ǵ� Ŭ���ϴ� �̺�Ʈ ������ �����ִ�
        // ���⼭ ���� ��ǥ ������ �̿��� ���̽�ƽ�� ��ġ�� ������ �ش�
        pos = new Vector2(eventData.position.x - backGroundRect.position.x,
            eventData.position.y - backGroundRect.position.y);
        pos = Vector2.ClampMagnitude(pos, range);

        if(!GameManager.OBJECT.player._isRun)
            pos.y = Mathf.Clamp(pos.y, limitRange, range);

        joyStickRect.localPosition = pos;

        inputDirection = pos.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isTouch = false;
        joyStickRect.localPosition = Vector2.zero;

        GameManager.OBJECT.player.PlayerMove(Vector2.zero);
    }
}
