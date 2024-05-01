using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public GameObject healthPrefab;

    private List<HP_Image> healths = new List<HP_Image>();

    private void OnEnable()
    {
        GameManager.OBJECT.player.OnPlayerHP += DrawHealths;
    }

    private void OnDisable()
    {
        GameManager.OBJECT.player.OnPlayerHP -= DrawHealths;
    }

    private void Update()
    {
        if(GameManager.UI.isHPUpdate)
        {
            DrawHealthUpdate();
        }
    }

    private void DrawHealths()
    {
        for (int i = 0; i < healths.Count; i++)
        {
            int healthState = (int)Mathf.Clamp
                (GameManager.OBJECT.player.curHealth - (i * 2), 0, 2);

            healths[i].SetHealthImage((HealthImages)healthState);
        }
    }

    public void DrawHealthUpdate()
    {
        ClearHealths();

        float maxHealthRemainder = GameManager.OBJECT.player.maxHealth % 2;
        // �ִ� ü���� ¦������ Ȧ������ Ȯ��
        int healthToMake = (int)((GameManager.OBJECT.player.maxHealth / 2) + maxHealthRemainder);

        for(int i = 0; i < healthToMake; i++)
        {
            CreateEmptyHealths();
        }

        DrawHealths();

        GameManager.UI.isHPUpdate = false;
    }

    public void CreateEmptyHealths()
    {
        GameObject newHealth = Instantiate(healthPrefab);
        newHealth.transform.SetParent(transform);

        if (!newHealth.TryGetComponent<HP_Image>(out var OutHP_Image))
            return;

        OutHP_Image.SetHealthImage(HealthImages.Empty);
        healths.Add(OutHP_Image);
    }

    public void ClearHealths()
    {
        foreach(Transform OutChildTransform in transform)
        {
            Destroy(OutChildTransform.gameObject);
        }

        healths = new List<HP_Image>();
    }
}
// ������Ʈ�� ����°� �ƴ϶� ��Ȱ��ȭ ��Ű����
