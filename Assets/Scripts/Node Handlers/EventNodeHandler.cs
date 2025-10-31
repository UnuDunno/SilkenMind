using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventNodeHandler : MonoBehaviour
{
    public GameObject eventPanel;
    private EventPanelUI eventPanelUI;

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

                EndEvent();

                return;
            }
        }

        EventNodeData eventNodeData = node.data as EventNodeData;

        if (eventNodeData == null)
        {
            Debug.LogError($"Os dados do nó '{node.name}' não são do tipo EventNodeData");

            EndEvent();

            return;
        }

        if (eventNodeData.nodeEventPool == null || eventNodeData.nodeEventPool.Count == 0)
        {
            Debug.LogError($"O 'Node Event Pool' do asset '{eventNodeData.name}' está vazio");

            EndEvent();

            return;
        }

        int randIndex = Random.Range(0, eventNodeData.nodeEventPool.Count);
        BaseEventSO selectedEvent = eventNodeData.nodeEventPool[randIndex];

        eventPanelUI.Setup(selectedEvent, this);
        eventPanel.SetActive(true);
    }

    public void EndEvent()
    {
        eventPanel.SetActive(false);

        onEventEnded.Invoke();
    }
}
