using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public NodeData data;
    public List<Node> nextNodes = new List<Node>();
    public bool completed = false;
    
    public Image background;
    public Image icon;
    public GameObject playerMarker;
    public Button button;
    private CanvasGroup canvasGroup;

    private Color originalColor;


    void Awake()
    {
        if (background == null) background = GetComponent<Image>();
        if (button == null) button = GetComponent<Button>();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        if (background != null)
        {
            originalColor = background.color;
        }
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnClick());
        }

        if (playerMarker != null)
        {
            playerMarker.SetActive(false);
        }

        UpdateIcon();
    }

    private void OnClick()
    {
        if (completed) return;

        PlayerMapProgress progress = FindObjectOfType<PlayerMapProgress>();
        if (progress != null)
        {
            progress.SelectNode(this);
        }
    }

    public void Configure(NodeData nodeData)
    {
        data = nodeData;

        if (icon != null && nodeData != null && nodeData.icon != null)
        {
            icon.sprite = nodeData.icon;
        }

        if (background != null && nodeData != null)
        {
            background.color = nodeData.color;
            originalColor = nodeData.color;
        }
    }

    private void UpdateIcon()
    {
        if (icon == null || data == null || data.icon == null) return;

        icon.sprite = data.icon;
    }

    public void SetColor(Color color)
    {
        if (background == null) return;

        background.color = color;
    }

    public void ResetColor()
    {
        if (background == null) return;

        background.color = originalColor;
    }

    public void SetAlpha(float alpha)
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = alpha;
    }

    public void SetInteractable(bool interactable)
    {
        if (button != null)
        {
            button.interactable = interactable;
        }

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = interactable;
            canvasGroup.interactable = interactable;
        }
    }

    public void SetPlayerActive(bool active)
    {
        if (playerMarker == null) return;

        playerMarker.SetActive(active);
    }

    public void SetAsCompleted()
    {
        completed = true;
        SetColor(Color.grey);
        SetAlpha(0.5f);
        SetInteractable(false);
        SetPlayerActive(false);
    }
}
