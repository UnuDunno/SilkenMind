using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestNodeHandler : MonoBehaviour
{
    public GameObject restPanel;
    public Button healButton;
    public Button increaseMaxHealthButton;

    private CombatManager combatManager;
    private PlayerMapProgress playerMapProgress;

    private readonly int maxHealthIncrease = 5;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        playerMapProgress = FindObjectOfType<PlayerMapProgress>();

        if (healButton != null)
        {
            healButton.onClick.AddListener(Heal);
        }
        if (increaseMaxHealthButton != null)
        {
            increaseMaxHealthButton.onClick.AddListener(IncreaseMaxHealth);
        }

        restPanel.SetActive(false);
    }

    public void Initiate(Node node)
    {
        restPanel.SetActive(true);
    }

    public void EndEvent()
    {
        restPanel.SetActive(false);

        playerMapProgress.EndCurrentEvent();
    }

    private void Heal()
    {
        if (combatManager == null) return;

        combatManager.HealPlayer(combatManager.playerMaxHP);

        EndEvent();
    }

    private void IncreaseMaxHealth()
    {
        if (combatManager == null) return;

        combatManager.playerMaxHP += maxHealthIncrease;
        combatManager.HealPlayer(maxHealthIncrease);

        EndEvent();
    }
}
