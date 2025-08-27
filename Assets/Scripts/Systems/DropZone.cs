using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var cardUI = eventData.pointerDrag.GetComponent<CardUI>();

        if (cardUI == null)
        {
            return;
        }

        cardUI.UseCard();
    }
}
