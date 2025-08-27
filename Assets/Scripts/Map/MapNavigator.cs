using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class MapNavigator : MonoBehaviour, IScrollHandler, IDragHandler, IBeginDragHandler
{
    [Header("Referências")]
    public RectTransform content;
    public RectTransform viewport;

    [Header("Configurações de Zoom")]
    public float zoomSpeed = 2f;
    public float zoomSmoothness = 10f;
    public float minScale = 0.5f;
    public float maxScale = 2.5f;

    [Header("Configurações de Arrasto")]
    public float dragSensitivity = 1f;
    public float moveSmoothness = 10f;

    private Vector3 targetScale;
    private Vector2 targetPosition;
    private Vector2 lastMousePos;

    void Start()
    {
        targetScale = content.localScale;
        targetPosition = content.anchoredPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePos;
            targetPosition += mouseDelta * dragSensitivity;
            lastMousePos = Input.mousePosition;
        }

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(content, Input.mousePosition, null, out localMousePos);

            float zoomDelta = scroll * zoomSpeed * Time.deltaTime;
            float newScale = Mathf.Clamp(targetScale.x + zoomDelta, minScale, maxScale);

            if (!Mathf.Approximately(newScale, targetPosition.x))
            {
                Vector2 pivotOffset = localMousePos * (newScale / targetScale.x - 1f);
                targetPosition -= pivotOffset;

                targetScale = new Vector3(newScale, newScale, 1);
            }
        }

        content.localScale = Vector3.Lerp(content.localScale, targetScale, Time.deltaTime * zoomSmoothness);
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * moveSmoothness);
    }

    public void OnScroll(PointerEventData eventData)
    {
        float scroll = eventData.scrollDelta.y;
        float newScale = Mathf.Clamp(targetScale.x + scroll * zoomSpeed, minScale, maxScale);

        targetScale = new Vector3(newScale, newScale, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = (eventData.position - lastMousePos) * dragSensitivity;

        targetPosition += delta;
        lastMousePos = eventData.position;
    }
}
