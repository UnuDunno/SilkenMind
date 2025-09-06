using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatNodeHandler : MonoBehaviour
{
    public GameObject combatPanel;
    public CombatManager combatManager;

    public void Initiate(Node node)
    {
        Debug.Log("⚔️ Iniciando combate!");

        combatPanel.SetActive(true);

        CombatManager.instance.StartCombat();
    }
}
