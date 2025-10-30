using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    public UnityEvent<int> onGoldChanged;

    [SerializeField]
    private int currentGold;

    public int startingGold = 100;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentGold = startingGold;
        NotifyGoldChanged();
    }

    private void NotifyGoldChanged()
    {
        if (onGoldChanged != null)
        {
            onGoldChanged.Invoke(currentGold);
        }
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        currentGold += amount;

        Debug.Log($"+{amount} Ouro. Novo total: {currentGold}");

        NotifyGoldChanged();
    }
    
    public bool TrySpendGold(int amountToSpend)
    {
        if (amountToSpend > currentGold)
        {
            Debug.Log($"Falha ao gastar {amountToSpend}. Saldo insuficiente");

            return false;
        }

        currentGold -= amountToSpend;

        Debug.Log($"-{amountToSpend} Ouro. Novo total: {currentGold}");

        NotifyGoldChanged();

        return true;
    }
}
