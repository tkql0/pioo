using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _abilityRect;
    // 위아래로 종류만 바꿀거

    private RectTransform _myRect;
    // 클릭시 비활성화 될 자기자신

    //private bool choice;

    public int myKey;
    // 상위 스크립트에서 비활성화된 UI를 활성화 시킬때 필요한 key값
    // 생각해보니까 딱히 비활성화 시킬 필요 없나?

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
