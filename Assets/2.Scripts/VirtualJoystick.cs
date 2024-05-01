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
    { // 캐릭터에게 입력 벡터를 전달
        GameManager.OBJECT.player.PlayerMove(inputDirection);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    { // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
        // 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 작동하지 않음
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        GameManager.OBJECT.player.PlayerMove(Vector2.zero);
    }

    // 조이스틱이 Player의 호흡게이지에 영향을 받았으면 좋겠어
}
