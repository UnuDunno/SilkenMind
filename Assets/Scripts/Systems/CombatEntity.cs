using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatEntity
{
    public int maxHealth;
    public int currentHealth;
    public int currentBlock;

    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent<int> onBlockChanged;

    public CombatEntity(int hp)
    {
        maxHealth = hp;
        currentHealth = hp;
        currentBlock = 0;

        onHealthChanged = new UnityEvent<int, int>();
        onBlockChanged = new UnityEvent<int>();
    }

    public void TakeDamage(int damage)
    {
        int originalBlock = currentBlock;

        if (currentBlock > 0)
        {
            int blockedDamage = Mathf.Min(currentBlock, damage);

            currentBlock -= blockedDamage;
            damage -= blockedDamage;
        }

        if (damage > 0)
        {
            currentHealth -= damage;

            if (currentHealth < 0) currentHealth = 0;
        }

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentBlock != originalBlock)
        {
            onBlockChanged?.Invoke(currentBlock);
        }
    }

    public void AddBlock(int amount)
    {
        currentBlock += amount;

        onBlockChanged?.Invoke(currentBlock);
    }

    public void ResetBlock()
    {
        currentBlock = 0;
        onBlockChanged?.Invoke(currentBlock);
    }
}
