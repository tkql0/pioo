using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform backGroundRect;
    // 백그라운드 이미지
    [SerializeField]
    RectTransform joyStickRect;
    // 조이스틱 이미지
    //[SerializeField]
    //Transform Player;
    // 움직일 캐릭터

    public Slider slider;

    public Vector2 pos;
    // 움직일 방향
    float range;
    // 조이스틱이 움직일 범위

    float limitRange;

    Vector2 inputDirection;

    bool _isTouch = false;

    void Start()
    {
        backGroundRect = gameObject.GetComponent<RectTransform>();
        // 클릭할 때 클릭한 곳 좌표의 보정을 위해 사용
        range = backGroundRect.rect.width * 0.5f;
        // 조이스틱이 움직일 범위를 백그라운드 크기의 반으로 설정(반지름)
        // 원하는 크기로 변경가능
        // 이러면 백그라운드가 없더라도 백그라운드 크기 만큼 조절 되겠네
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
    { // 클릭한 상태에서 움직일 때 함수
        // eventData는 터치또는 클릭하는 이베트 정보를 갖고있다
        // 여기서 얻은 좌표 정보를 이용해 조이스틱의 위치를 변경해 준다
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
