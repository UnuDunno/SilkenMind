using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanelUIReferences : MonoBehaviour
{
    public GameObject cardHandContainer;
    public Button endTurnButton;

    public TextMeshProUGUI combatLogText;
    public TextMeshProUGUI enemyIntentText;

    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public Button victoryContinueButton;
    public Button defeatContinueButton;

    public EnemyHealthUI enemyHealthUI;

    public BlockUI playerBlockUI;
    public BlockUI enemyBlockUI;

    public Image enemySpriteRenderer;
}
