using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    [SerializeField]
    private Canvas mainCanvas;
    private Vector2 inputDirection;

    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(isInput)
        {
            InputControlVector();
        }
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var scaledAnchoredPosition = rectTransform.anchoredPosition *
            mainCanvas.transform.localScale.x;
        var inputPos = eventData.position - scaledAnchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ?
            inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    private void InputControlVector()
    { // ĳ���Ϳ��� �Է� ���͸� ����
        GameManager.OBJECT.player.PlayerMove(inputDirection);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    { // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
        // Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� �۵����� ����
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        GameManager.OBJECT.player.PlayerMove(Vector2.zero);
    }

    // ���̽�ƽ�� Player�� ȣ��������� ������ �޾����� ���ھ�
}
