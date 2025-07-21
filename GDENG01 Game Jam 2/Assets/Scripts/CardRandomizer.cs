using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] Deck;

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] TextMeshProUGUI ButtonText1;
    [SerializeField] TextMeshProUGUI ButtonText2;
    [SerializeField] TextMeshProUGUI ButtonText3;

    [SerializeField] TextMeshProUGUI CardDisplay;

    [SerializeField] GameObject Choices;

    private GameObject[] ChosenCards = new GameObject[3];

    public int CardNumber = 0;
    public bool isActive = false;

    public int CardID;

    private int SelectedButton = 3;
    private int CurrentQuestion = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Choices.SetActive(false);

        EventBroadcaster.Instance.AddObserver("CardChosen", ChooseCard);
        EventBroadcaster.Instance.AddObserver("Restart", RestartCards);
    }

    void ChooseCard()
    {
        isActive = true;

        if (CardNumber < 3) ChosenCards[CardNumber] = Deck[CardID];
        Debug.Log(CardNumber);
        CardNumber++;
    }

    void RestartCards()
    {
        isActive = false;
        CardNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CurrentQuestion);

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("you chose 1");
            SelectedButton = 0;
            CurrentQuestion++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("you chose 2");
            SelectedButton = 1;
            CurrentQuestion++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("you chose 3");
            SelectedButton = 2;
            CurrentQuestion++;
        }

        if (CardNumber == 3)
        {
            Choices.SetActive(true);

            TarotCardController Card1 = ChosenCards[0].GetComponent<TarotCardController>();
            TarotCardController Card2 = ChosenCards[1].GetComponent<TarotCardController>();
            TarotCardController Card3 = ChosenCards[2].GetComponent<TarotCardController>();

            if (CurrentQuestion == 0)
            {
                CardDisplay.text = "Card (Past): " + Card1.name;
                Choice(Card1);
            }
            else if (CurrentQuestion == 1)
            {
                CardDisplay.text = "Card (Present): " + Card1.name;
                Choice(Card2);
            }
            else if (CurrentQuestion == 2)
            {
                CardDisplay.text = "Card (Future): " + Card1.name;
                Choice(Card3);
            }
        }
    }

    void Choice(TarotCardController CorrectCard)
    {
        int CorrectAnswer = Random.Range(0, 3);
        Debug.Log("Correct answer is" + CorrectAnswer);

        if (CorrectAnswer == 0)
        {
            ButtonText1.text = CorrectCard.upright_description;
            ButtonText2.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
            ButtonText3.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
        }
        else if(CorrectAnswer == 1)
        {
            ButtonText2.text = CorrectCard.upright_description;
            ButtonText1.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
            ButtonText3.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
        }
        else if(CorrectAnswer == 2)
        {
            ButtonText3.text = CorrectCard.upright_description;
            ButtonText1.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
            ButtonText2.text = Deck[Random.Range(0, 22)].GetComponent<TarotCardController>().upright_description;
        }

        if (SelectedButton == CorrectAnswer)
        {
            EventBroadcaster.Instance.PostEvent("RightAnswer");
        }
        else if (SelectedButton != 3)
        {
            EventBroadcaster.Instance.PostEvent("WrongAnswer");
        }
        
    }
}
