using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int maxHealth = 100;
    public int currentHealth;

    public int maxFear = 5;
    public int currentFear;

    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent<int> onFearChanged;

    private int initialHealth;

    void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        initialHealth = maxHealth;
        currentHealth = maxHealth;
        currentFear = 0;
    }

    void Start()
    {
        NotifyHealthChanged();
        NotifyFearChanged();
    }

    private void NotifyHealthChanged()
    {
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void NotifyFearChanged()
    {
        onFearChanged?.Invoke(currentFear);
    }

    public void ResetStats()
    {
        Debug.Log("Resetando status do jogador");

        currentHealth = initialHealth;
        currentFear = 0;

        NotifyHealthChanged();
        NotifyFearChanged();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0) currentHealth = 0;

        NotifyHealthChanged();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        NotifyHealthChanged();
    }

    public void IncreaseMaxHealth(int amount)
    {
        if (amount <= 0) return;

        maxHealth += amount;
        currentHealth += amount;

        NotifyHealthChanged();
    }

    public void IncreaseMaxFear(int amount)
    {
        if (amount <= 0) return;

        maxFear += amount;

        NotifyFearChanged();
    }

    public void SetFear(int amount)
    {
        currentFear = amount;
        NotifyFearChanged();
    }

    public bool TrySpendFear(int amount)
    {
        if (currentFear < amount) return false;

        currentFear -= amount;

        NotifyFearChanged();

        return true;
    }

    public void AddFear(int amount)
    {
        currentFear += amount;

        if (currentFear > maxFear) currentFear = maxFear;

        NotifyFearChanged();
    }
}
