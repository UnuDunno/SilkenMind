using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType { Gold, MaxHealth, Fear }
public enum PunishmentType { None, TakeDamage, LoseCard }

public abstract class BaseEventSO : ScriptableObject
{
    public string eventTitle;
    public string eventDescription;
    public Sprite eventImage;

    public abstract void SetupPanel(EventPanelUI panel);
}
