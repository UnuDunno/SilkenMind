using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeHandler : MonoBehaviour
{
    public GameObject eventPanel;

    public void Initiate(Node node)
    {
        Debug.Log("📜 Evento narrativo iniciado!");

        eventPanel.SetActive(true);
    }

    public void EndEvent()
    {
        eventPanel.SetActive(false);

        FindObjectOfType<PlayerMapProgress>().EndCurrentEvent();
    }
}
