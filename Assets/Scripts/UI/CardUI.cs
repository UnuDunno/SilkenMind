using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardUI : MonoBehaviour
{
    [Header("Referências de UI")]
    public Image image;
    public TMP_Text cardName;
    public TMP_Text description;
    public TMP_Text fearValue;

    [HideInInspector] public CardInstance cardInstance;

    public void ConfigureCard(CardInstance instance)
    {
        cardInstance = instance;

        if (cardInstance == null || cardInstance.data == null)
        {
            Debug.LogWarning("Instância ou dados da carta são nulos!");
            return;
        }

        image.sprite = cardInstance.data.image;
        cardName.text = cardInstance.data.cardName;
        description.text = cardInstance.data.description;
        fearValue.text = cardInstance.data.fearValue.ToString();
    }

    public void UseCard()
    {
        if (cardInstance == null) return;

        cardInstance.UseCard();
        Destroy(gameObject);
    }
}
