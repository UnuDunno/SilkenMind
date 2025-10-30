using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    public TextMeshProUGUI blockText;

    public void UpdateBlock(int amount)
    {
        if (amount < 0) amount = 0;

        blockText.text = amount.ToString();
    }
}
