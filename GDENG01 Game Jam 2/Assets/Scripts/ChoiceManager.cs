using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText1;
    [SerializeField] TextMeshProUGUI ButtonText2;
    [SerializeField] TextMeshProUGUI ButtonText3;

    [SerializeField] TextMeshProUGUI CardDisplay;
    [SerializeField] GameObject Choices;
    [SerializeField] CardRandomizer cardRandomizer;

    private int SelectedAnswer = 0;
    private int CorrectAnswer;
    private int QuestionNumber;

    private TarotCardController Card1;
    private TarotCardController Card2;
    private TarotCardController Card3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver("ActivateChoices", ActivateChoices);
        EventBroadcaster.Instance.AddObserver("DeactivateChoices", DeactivateChoices);

        Choices.SetActive(false);
        NewAnswer();
    }

    void ActivateChoices()
    {
        Choices.SetActive(true);
    }

    void DeactivateChoices()
    {
        Choices.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // Assign Cards for each Question
        Card1 = cardRandomizer.ChosenCards[0].GetComponent<TarotCardController>();
        Card2 = cardRandomizer.ChosenCards[1].GetComponent<TarotCardController>();
        Card3 = cardRandomizer.ChosenCards[2].GetComponent<TarotCardController>();

        // Get KeyboardInput
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedAnswer = 0;

            if (Choices.activeSelf)
            {
                SetChoices();
                QuestionNumber++;
            }

            // Check if Correct
            CheckAnswer();
            NewAnswer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedAnswer = 1;

            if (Choices.activeSelf)
            {
                SetChoices();
                QuestionNumber++;
            }

            // Check if Correct
            CheckAnswer();
            NewAnswer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedAnswer = 2;

            if (Choices.activeSelf)
            {
                SetChoices();
                QuestionNumber++;
            }

            // Check if Correct
            CheckAnswer();
            NewAnswer();
        }

        // To know which Question we are On
        if (QuestionNumber == 0)
        {
            // Set text to know Which Card
            CardDisplay.text = "Past: " + Card1.name;
        }
        else if (QuestionNumber == 1)
        {
            // Set text to know Which Card
            CardDisplay.text = "Present: " + Card2.name;
        }
        else if (QuestionNumber == 2)
        {
            // Set text to know Which Card
            CardDisplay.text = "Future: " + Card3.name;
        }
        else
        {
            // Reset Which Question We are on
            QuestionNumber = 0;
        }
    }

    private void NewAnswer()
    {
        CorrectAnswer = Random.Range(0, 3);
    }

    private void CheckAnswer()
    {
        if (SelectedAnswer == CorrectAnswer) EventBroadcaster.Instance.PostEvent("RightAnswer");
        else EventBroadcaster.Instance.PostEvent("WrongAnswer");
    }

    private void SetChoices()
    {
        if (CorrectAnswer == 0)
        {
            ButtonText1.text = Card1.upright_description;
        }
        else if (CorrectAnswer == 1)
        {
            ButtonText2.text = Card2.upright_description;
        }
        else if (CorrectAnswer == 2)
        {
            ButtonText3.text = Card3.upright_description;
        }
    }
}
