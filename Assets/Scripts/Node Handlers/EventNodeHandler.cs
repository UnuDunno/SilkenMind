using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventNodeHandler : MonoBehaviour
{
    public GameObject eventPanel;
    private EventPanelUI eventPanelUI;

    public List<BaseEventSO> eventPool;

    public UnityEvent onEventEnded;

    public void Initiate(Node node)
    {
        Debug.Log("📜 Evento narrativo iniciado!");

        if (eventPanelUI == null)
        {
            eventPanelUI = eventPanel.GetComponent<EventPanelUI>();
            if (eventPanelUI == null)
            {
                Debug.LogError("EventPanel não tem o script EventPanelUI");
                return;
            }
        }

        if (eventPool == null || eventPool.Count == 0)
        {
            Debug.LogError("Event Pool está vazio");
            EndEvent();

            return;
        }

        int randIndex = Random.Range(0, eventPool.Count);
        BaseEventSO selectedEvent = eventPool[randIndex];

        eventPanelUI.Setup(selectedEvent, this);
        eventPanel.SetActive(true);
    }

    public void EndEvent()
    {
        eventPanel.SetActive(false);

        onEventEnded.Invoke();
    }
}
