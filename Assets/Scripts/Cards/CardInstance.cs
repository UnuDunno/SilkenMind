using UnityEngine;

public class CardInstance : MonoBehaviour
{
    public BaseCard data;
    public bool corrupted;
    private CardUI cardUI;

    void Awake()
    {
        if(cardUI == null) cardUI = GetComponent<CardUI>();
    }

    public void Initialize(BaseCard baseData)
    {
        data = baseData;
        corrupted = false;

        if(cardUI == null) cardUI = GetComponent<CardUI>();

        if (cardUI != null) cardUI.ConfigureCard(this);
    }

    public void UseCard()
    {
        if (data == null || corrupted) return;

        if (CombatManager.Instance != null) CombatManager.Instance.TryPlayCard(this);
    }
}
