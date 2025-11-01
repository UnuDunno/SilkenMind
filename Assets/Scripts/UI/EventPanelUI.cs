using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPanelUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image eventImage;
    public GameObject imageFrame;

    public GameObject curiosityGroup;
    public GameObject choiceGroup;
    public GameObject identificationGroup;
    public GameObject outcomeGroup;

    public Button curiosityContinueButton;
    public Button choice1Button;
    public TextMeshProUGUI choice1Text;
    public Button choice2Button;
    public TextMeshProUGUI choice2Text;

    public TextMeshProUGUI questionText;
    public Button idOption1Button;
    public TextMeshProUGUI idOption1Text;
    public Button idOption2Button;
    public TextMeshProUGUI idOption2Text;

    public TextMeshProUGUI outcomeText;
    public Button outcomeContinueButton;

    private EventNodeHandler currentHandler;
    private CuriosityEventSO currentCuriosityEvent;
    private ChoiceEventSO currentChoiceEvent;
    private IdentificationEventSO currentIdEvent;

    void Awake()
    {
        RemoveListeners();

        curiosityContinueButton.onClick.AddListener(OnCuriosityContinue);
        outcomeContinueButton.onClick.AddListener(CloseEvent);

        choice1Button.onClick.AddListener(() => OnChoiceSelected(0));
        choice2Button.onClick.AddListener(() => OnChoiceSelected(1));

        idOption1Button.onClick.AddListener(() => OnIdentificationSelected(0));
        idOption2Button.onClick.AddListener(() => OnIdentificationSelected(1));
    }

    private void RemoveListeners()
    {
        curiosityContinueButton.onClick.RemoveAllListeners();
        outcomeContinueButton.onClick.RemoveAllListeners();
        choice1Button.onClick.RemoveAllListeners();
        choice2Button.onClick.RemoveAllListeners();
        idOption1Button.onClick.RemoveAllListeners();
        idOption2Button.onClick.RemoveAllListeners();
    }

    public void Setup(BaseEventSO eventData, EventNodeHandler handler)
    {
        currentHandler = handler;

        currentCuriosityEvent = null;
        currentChoiceEvent = null;
        currentIdEvent = null;

        HideAllGroups();

        titleText.text = eventData.eventTitle;
        descriptionText.text = eventData.eventDescription;

        bool hasImage = eventData.eventImage != null;
        eventImage.sprite = eventData.eventImage;
        if (imageFrame != null) imageFrame.SetActive(hasImage);
        else eventImage.gameObject.SetActive(hasImage);

        eventData.SetupPanel(this);
    }

    private void HideAllGroups()
    {
        curiosityGroup.SetActive(false);
        choiceGroup.SetActive(false);
        identificationGroup.SetActive(false);
        outcomeGroup.SetActive(false);
    }

    public void DisplayCuriosityEvent(CuriosityEventSO eventData)
    {
        currentCuriosityEvent = eventData;
        curiosityGroup.SetActive(true);
    }

    public void DisplayChoiceEvent(ChoiceEventSO eventData)
    {
        currentChoiceEvent = eventData;
        choice1Text.text = eventData.choice1Text;
        choice2Text.text = eventData.choice2Text;

        choiceGroup.SetActive(true);
    }


    public void DisplayIdentificationEvent(IdentificationEventSO eventData)
    {
        currentIdEvent = eventData;
        questionText.text = eventData.questionText;
        idOption1Text.text = eventData.option1Text;
        idOption2Text.text = eventData.option2Text;
        identificationGroup.SetActive(true);
    }


    private void OnCuriosityContinue()
    {
        if (currentCuriosityEvent != null) currentCuriosityEvent.GrantReward();

        CloseEvent();
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        if (currentChoiceEvent == null) return;

        choiceGroup.SetActive(false);

        currentChoiceEvent.ApplyPunishment(choiceIndex);

        outcomeText.text = (choiceIndex == 0) ? currentChoiceEvent.choice1OutcomeText : currentChoiceEvent.choice2OutcomeText;
        outcomeGroup.SetActive(true);
    } 

    private void OnIdentificationSelected(int choiceIndex)
    {
        if (currentIdEvent == null) return;

        identificationGroup.SetActive(false);

        bool wasCorrect = (choiceIndex == 0) ? currentIdEvent.option1IsCorrect : currentIdEvent.option2IsCorrect;

        currentIdEvent.ApplyOutcome(wasCorrect);

        outcomeText.text = wasCorrect ? currentIdEvent.correctOutcomeText : currentIdEvent.incorrectOutcomeText;
        outcomeGroup.SetActive(true);
    }

    private void CloseEvent()
    {
        if (currentHandler != null) currentHandler.EndEvent();
    }
}
