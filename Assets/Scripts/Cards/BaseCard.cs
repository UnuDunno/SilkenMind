using System;
using UnityEngine;

public enum CardType { Attack, Defense, Effect, Trauma }

[CreateAssetMenu(fileName = "NovaCarta", menuName = "Cartas/BaseCard")]
public class BaseCard : ScriptableObject {
    public string cardName;
    public string description;
    public Sprite image;
    public CardType type;
    public int fearValue;

    public virtual void ExecuteEffect() {
        Debug.Log($"Executando efeito da carta: {cardName}");
    }
}
