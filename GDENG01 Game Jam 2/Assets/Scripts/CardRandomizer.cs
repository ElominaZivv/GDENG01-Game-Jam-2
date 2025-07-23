using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardRandomizer : MonoBehaviour
{
    [SerializeField] public GameObject[] Deck;

    [SerializeField] TextMeshProUGUI ButtonText1;
    [SerializeField] TextMeshProUGUI ButtonText2;
    [SerializeField] TextMeshProUGUI ButtonText3;

    [SerializeField] TextMeshProUGUI CardDisplay;

    [SerializeField] GameObject Choices;

    public GameObject[] ChosenCards = new GameObject[3];

    private int CardIndex1;
    private int CardIndex2; 
    private int CardIndex3;

    public TarotCardController Card1;
    public TarotCardController Card2;
    public TarotCardController Card3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Generates a new set of chosen Cards
        EventBroadcaster.Instance.AddObserver("Restart", RestartCards);

        // First Set of Cards
        AssignCards();
    }
    void RestartCards()
    {
        AssignCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generates 3 indexes
    private void GenerateIndexes()
    {
        CardIndex1 = Random.Range(0, 22);

        do
        {
            CardIndex2 = Random.Range(0, 22);
        } while (CardIndex2 == CardIndex1);

        do
        {
            CardIndex3 = Random.Range(0, 22);
        } while (CardIndex3 == CardIndex1 || CardIndex3 == CardIndex2);
    }

    private void AssignCards()
    {
        GenerateIndexes();
        ChosenCards[0] = Deck[CardIndex1];
        ChosenCards[1] = Deck[CardIndex2];
        ChosenCards[2] = Deck[CardIndex3];
        FlipCards();
    }

    private void FlipCards() 
    {
        Card1 = ChosenCards[0].GetComponent<TarotCardController>();
        Card2 = ChosenCards[1].GetComponent<TarotCardController>();
        Card3 = ChosenCards[2].GetComponent<TarotCardController>();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("Restart");
    }
}
