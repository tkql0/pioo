using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    [SerializeField]
    private Canvas mainCanvas;
    public Vector2 inputDirection;

    [SerializeField]
    private RectTransform BreathSlider;

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

        // 수영 버튼을 눌렀을때 호흡량 아래로 움직일 수 있고
        // 누르지 않았을떄는 호흡량 아래로 안내려가게 바꿔야지
    }

    private void InputControlVector()
    {
        GameManager.OBJECT.player.PlayerMove(inputDirection);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
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
