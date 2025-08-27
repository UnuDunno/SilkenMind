using System.Diagnostics;
using UnityEngine;

public class CardInstance {
    public BaseCard data;
    public bool corrupted;

    public CardInstance(BaseCard baseData) {
        data = baseData;
        corrupted = false;
    }

    public void UseCard() {
        if (!corrupted) {
            data.ExecuteEffect();
        }
        else {
            UnityEngine.Debug.Log($"Carta {data.cardName} está corrompida e causa um efeito negativo!");
        }
    }
}
