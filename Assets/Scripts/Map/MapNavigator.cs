using UnityEngine;
using UnityEngine.EventSystems;

public class MapNavigator : MonoBehaviour
{
    public RectTransform content;
    public RectTransform viewport;

    public float dragSpeed = 1.0f;

    public float zoomSpeed = 8f;
    public float zoomStep = 0.2f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.5f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    private float targetZoom = 1f;
    private Camera uiCamera;

    void Start()
    {
        if (content == null)
        {
            enabled = false;
            return;
        }

        if (viewport == null)
        {
            viewport = transform as RectTransform;
        }

        uiCamera = Camera.main;
        targetZoom = content.localScale.x;
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 diff = Input.mousePosition - dragOrigin;
            dragOrigin = Input.mousePosition;
            content.anchoredPosition += (Vector2)diff * dragSpeed;
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, Input.mousePosition, uiCamera, out mousePos);

            Vector3 beforeZoom = content.InverseTransformPoint(uiCamera.ScreenToWorldPoint(Input.mousePosition));

            targetZoom = Mathf.Clamp(targetZoom + scroll * zoomStep, minZoom, maxZoom);

            content.localScale = Vector3.Lerp(content.localScale, Vector3.one * targetZoom, Time.deltaTime * (zoomSpeed * 60));

            Vector3 afterZoom = content.InverseTransformPoint(uiCamera.ScreenToWorldPoint(Input.mousePosition));
            Vector3 offset = afterZoom - beforeZoom;

            content.localPosition -= offset;
        }
        else
        {
            content.localScale = Vector3.Lerp(content.localScale, Vector3.one * targetZoom, Time.deltaTime * (zoomSpeed * 60));
        }
    }
}
