using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerGoldUI : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.onGoldChanged.AddListener(UpdateGoldText);

            UpdateGoldText(PlayerWallet.Instance.GetCurrentGold());
        }
        else
        {
            Debug.LogError("PlayerGoldUI não encontrou o PlayerWaller");

            goldText.text = "Erro!";
        }
    }

    public void UpdateGoldText(int newAmount)
    {
        goldText.text = $"Ouro: {newAmount}";
    }

    void OnDestroy()
    {
        if(PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.onGoldChanged.RemoveListener(UpdateGoldText);
        }
    }
}
