using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatButtonUI : MonoBehaviour
{
    //public int myKey;

    public int abilityID;

    public int statPoint = 0;

    float min;
    float max;

    [SerializeField]
    private TextMeshProUGUI _inputText;

    [SerializeField]
    private TextMeshProUGUI _resultText;

    [SerializeField]
    private TextMeshProUGUI _resultMinText;
    [SerializeField]
    private TextMeshProUGUI _resultMaxText;

    public GameObject fish;

    public RectTransform backGroundRect;

    Stack<GameObject> uiSpawnList = new Stack<GameObject>();

    Stack<GameObject> uiPoolList = new Stack<GameObject>();

    private void OnEnable()
    {
        statPoint = 0;

        _inputText.text = statPoint.ToString();
    }

    private void OnDisable()
    {
        ChoiceDeSpawn(_resultText, 0);
    }

    private void Update()
    {
        StatUpdate();
    }

    void StackFishUIObject()
    {
        if(uiPoolList.Count <= 0)
        {
            GameObject _fish = Instantiate(fish, backGroundRect);

            _fish.SetActive(false);

            uiPoolList.Push(_fish);
        }
    }

    public void Spawn()
    {
        StackFishUIObject();

        GameObject choiceUI = uiPoolList.Pop();

        uiSpawnList.Push(choiceUI);

        choiceUI.SetActive(true);

        //if (!choiceUI.TryGetComponent<RectTransform>(out var OutRect))
        //    return;

        float randomPositionX = Random.Range(-300, 300);
        float randomPositionY = Random.Range(-250, 250);

        choiceUI.transform.position = new Vector2(backGroundRect.position.x + randomPositionX, backGroundRect.position.y + randomPositionY);

        // �̹����� ��ų ����Ʈ�� �����ʰ� �ٲٱ�
        // ��ư�� ������ ��ų �ּ�ġ�� �ִ�ġ ���� �ٲ�� �ϰ�
        // Max ���س���
        // �ּ�ġ�� �ִ�ġ�� ������ ��ư�� ��������
        // for ������ uiSpawnList�� �ִ� ������Ʈ�� uiPoolList�� ���� ��Ű��
        // max�� ���س����� ���â ������
        // - ����� �̹� max�Դϴ�. ���� �ش� ����Ʈ�� ����Ͻðڽ��ϱ�? No Yes
    }

    public void DeSpawn()
    {
        if (uiSpawnList.Count <= 0)
            return;

        GameObject choiceUI = uiSpawnList.Pop();

        choiceUI.SetActive(false);

        uiPoolList.Push(choiceUI);
    }

    public void ChoiceButton(int myKey)
    {
        Player player = GameManager.OBJECT.player;

        float result = StatButtonClick(player, myKey);

        _inputText.text = StatResult(result, _resultText);


    }

    string StatResult(float InResult, TextMeshProUGUI InResultText)
    {
        switch (abilityID)
        {
            case 0:
                InResultText.text = "0";
                break;
            case (int)AbilityType.Health_Ability:
                InResultText.text = (InResult * 2f).ToString();
                break;
            case (int)AbilityType.Damage_Ability:
                InResultText.text = (InResult * 1f).ToString();
                break;
            case (int)AbilityType.Speed_Ability:
                InResultText.text = (InResult * 0.5f).ToString();
                break;
            case (int)AbilityType.Power_Ability:
            case (int)AbilityType.Breath_Ability:
            case (int)AbilityType.Storage_Ability:
                InResultText.text = (InResult * 1f).ToString();
                break;
        }

        return InResult.ToString();
    }

    float StatUp(float InStatPoint)
    {
        switch (abilityID)
        {
            case (int)AbilityType.Health_Ability:
                InStatPoint = InStatPoint * 2f;
                break;
            case (int)AbilityType.Damage_Ability:
                InStatPoint = InStatPoint * 1f;
                break;
            case (int)AbilityType.Speed_Ability:
                InStatPoint = InStatPoint * 0.5f;
                break;
            case (int)AbilityType.Power_Ability:
            case (int)AbilityType.Breath_Ability:
            case (int)AbilityType.Storage_Ability:
                InStatPoint = InStatPoint * 1f;
                break;
        }

        return InStatPoint;
    }

    bool _update = true;

    public void StatUpdate()
    {
        Player player = GameManager.OBJECT.player;

        if (_update)
        {
            switch (abilityID)
            {
                case (int)AbilityType.Health_Ability:
                    min = player.curHealth;
                    max = player.maxHealth;
                    break;
                case (int)AbilityType.Damage_Ability:
                    min = player.damage;
                    max = player.playerCriticalDamage;
                    break;
                case (int)AbilityType.Speed_Ability:
                    min = player.moveSpeed;
                    max = player.moveMaxSpeed;
                    break;
                case (int)AbilityType.Power_Ability:
                    min = player.attackMinPower;
                    max = player.attackMaxPower;
                    break;
                case (int)AbilityType.Breath_Ability:
                    min = player.curBreath;
                    max = player.maxBreath;
                    break;
                case (int)AbilityType.Storage_Ability:
                    min = player.moveSpeed;
                    max = player.moveMaxSpeed;
                    break;
            }
        }
        else
        {
            switch (abilityID)
            {
                case (int)AbilityType.Health_Ability:
                    player.curHealth = min;
                    player.maxHealth = max;
                    break;
                case (int)AbilityType.Damage_Ability:
                    player.damage = (int)min;
                    player.playerCriticalDamage = (int)max;
                    break;
                case (int)AbilityType.Speed_Ability:
                    player.moveSpeed = min;
                    player.moveMaxSpeed = max;
                    break;
                case (int)AbilityType.Power_Ability:
                    player.attackMinPower = min;
                    player.attackMaxPower = max;
                    break;
                case (int)AbilityType.Breath_Ability:
                    player.curBreath = min;
                    player.maxBreath = max;
                    break;
                case (int)AbilityType.Storage_Ability:
                    player.moveSpeed = min;
                    player.moveMaxSpeed = max;
                    break;
            }

            _update = true;
        }
        _resultMinText.text = min.ToString();
        _resultMaxText.text = max.ToString();
    }

    public int StatButtonClick(Player InPlayer,int InStatNumber)
    {
        switch (InStatNumber)
        {
            // enum������ �����
            case 1:
                // up
                if (InPlayer.LvPoint <= statPoint)
                    return InPlayer.LvPoint;

                ++statPoint;
                break;
            case 2:
                // down
                if (statPoint <= 0)
                    return statPoint;

                --statPoint;
                break;
            case 3:
                // MinResult
                float minReturn = min;

                min += StatUp(statPoint);

                if(min > max && abilityID != (int)AbilityType.Damage_Ability)
                {
                    min = minReturn;
                    _update = false;

                    ChoiceDeSpawn(_resultMinText, min);
                    statPoint = 0;
                    break;
                }
                else
                {
                    _update = false;

                    ChoiceDeSpawn(_resultMinText, min);

                    InPlayer.LvPoint -= statPoint;
                    statPoint = 0;
                    break;
                }
            case 4:
                // MaxResult
                max += StatUp(statPoint);

                _update = false;

                ChoiceDeSpawn(_resultMaxText, max);

                InPlayer.LvPoint -= statPoint;
                statPoint = 0;
                break;
        }
        // statPoint�� ���� ������� �̹��� ������ ��ġ Ȱ��ȭ
        // Result ��ư�� ������ ���� ���� �̹����� �׷� ������
        // ���� �׾���� ¦��� �� �̹����� ������ ���

        return statPoint;
    }

    void ChoiceDeSpawn(TextMeshProUGUI InResultText, float InStatPoint)
    {
        float DeSpawnCount = uiSpawnList.Count;

        for (int i = 0; i < DeSpawnCount; i++)
            DeSpawn();

        StatResult(InStatPoint, InResultText);
    }
}
