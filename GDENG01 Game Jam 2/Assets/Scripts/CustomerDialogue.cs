using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerDialogue : MonoBehaviour
{

    [SerializeField] GameObject DialogueBox;
    [SerializeField] TextMeshProUGUI TextScript;
    [SerializeField] ChoiceManager choiceManager;

    private bool AskingReading = false;
    private bool CheckingAnswer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueBox.SetActive(false);

        // Events for each possible Dialogue
        EventBroadcaster.Instance.AddObserver("AskReading", AskReading);
        EventBroadcaster.Instance.AddObserver("RightAnswer", RightAnswer);
        EventBroadcaster.Instance.AddObserver("WrongAnswer", WrongAnswer);
    }
    void AskReading()
    {
        DialogueBox.SetActive(true);
        TextScript.text = "I would like to ask for a reading on my past present and future";
        AskingReading = true;
    }

    void RightAnswer()
    {
        EventBroadcaster.Instance.PostEvent("DeactivateChoices");
        DialogueBox.SetActive(true);
        TextScript.text = "Wow! That does sound like me!";
        CheckingAnswer = true;
    }

    void WrongAnswer()
    {
        EventBroadcaster.Instance.PostEvent("DeactivateChoices");
        DialogueBox.SetActive(true);
        TextScript.text = "What are you saying? That's not me!";
        CheckingAnswer = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Press E to Close Dialogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueBox.SetActive(false);
            if (AskingReading)
            {
                EventBroadcaster.Instance.PostEvent("ActivateChoices");
                EventBroadcaster.Instance.PostEvent("SetChoices");
                AskingReading = false;

            }
            if (CheckingAnswer)
            {
                if (choiceManager.QuestionNumber != 0) EventBroadcaster.Instance.PostEvent("ActivateChoices");
                CheckingAnswer = false;
            }
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("AskReading");
        EventBroadcaster.Instance.RemoveObserver("RightAnswer");
        EventBroadcaster.Instance.RemoveObserver("WrongAnswer");
    }

}
