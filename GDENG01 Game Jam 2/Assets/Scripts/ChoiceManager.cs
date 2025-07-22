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

    // private TarotCardController Card1 = new cardRandomizer.ChosenCards[0].GetComponent<TarotCardController>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Choices.SetActive(false);
        NewAnswer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedAnswer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedAnswer = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedAnswer = 2;
        }
    }

    private void NewAnswer()
    {
        CorrectAnswer = Random.Range(0, 3);
    }

    private bool CheckAnswer()
    {
        return SelectedAnswer == CorrectAnswer;
    }
}
