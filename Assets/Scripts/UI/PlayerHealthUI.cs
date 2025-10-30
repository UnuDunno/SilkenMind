using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    void Start()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerHealthUI não encontrou PlayerStats");
            return;
        }

        PlayerStats.Instance.onHealthChanged.AddListener(UpdateHealth);

        UpdateHealth(PlayerStats.Instance.currentFear, PlayerStats.Instance.maxHealth);
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }
        if (healthText != null)
        {
            healthText.text = $"{current} / {max}";
        }
    }

    void OnDestroy()
    {
        if(PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onHealthChanged.RemoveListener(UpdateHealth);
        }
    }
}
