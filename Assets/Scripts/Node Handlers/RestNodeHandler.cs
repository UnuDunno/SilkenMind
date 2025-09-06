using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestNodeHandler : MonoBehaviour
{
    public GameObject restPanel;

    public void Initiate(Node node)
    {
        Debug.Log("🛌 Descansando e recuperando vida!");

        restPanel.SetActive(true);
    }

    public void EndEvent()
    {
        restPanel.SetActive(false);

        FindObjectOfType<PlayerMapProgress>().EndCurrentEvent();
    }
}
