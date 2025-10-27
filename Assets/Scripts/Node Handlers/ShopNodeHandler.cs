using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopNodeHandler : MonoBehaviour
{
    public ShopPanel shopPanel;

    void Start()
    {
        shopPanel = FindObjectOfType<ShopPanel>();
    }

    public void Initiate(Node node)
    {
        if (shopPanel != null)
        {
            shopPanel.Open();

            Debug.Log("Entrou na loja!");
        }
        else
        {
            Debug.LogWarning("ShopPanel não encontrado na cena.");
        }
    }
}
