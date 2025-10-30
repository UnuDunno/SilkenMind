using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFearUI : MonoBehaviour
{
    private TextMeshProUGUI fearText;

    void Awake()
    {
        fearText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.Log("PlayerFearUI não encontrou PlayerStats");
            return;
        }

        PlayerStats.Instance.onFearChanged.AddListener(UpdateFear);

        
        UpdateFear(PlayerStats.Instance.currentFear);
    }

    public void UpdateFear(int current)
    {
        if (fearText != null && PlayerStats.Instance != null)
        {
            fearText.text = $"{current} / {PlayerStats.Instance.maxFear}";
        }
    }
    
    void OnDestroy()
    {
        if(PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onFearChanged.RemoveListener(UpdateFear);
        }
    }
}
