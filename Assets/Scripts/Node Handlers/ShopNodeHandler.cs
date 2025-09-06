using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNodeHandler : MonoBehaviour
{
    public GameObject shopPanel;

    public void Initiate(Node node)
    {
        Debug.Log("💰 Loja aberta!");

        shopPanel.SetActive(true);
    }

    public void EndEvent()
    {
        shopPanel.SetActive(false);

        FindObjectOfType<PlayerMapProgress>().EndCurrentEvent();
    }
}
