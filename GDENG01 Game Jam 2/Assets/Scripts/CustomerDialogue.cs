using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerDialogue : MonoBehaviour
{

    [SerializeField] GameObject DialogueBox;
    [SerializeField] TextMeshProUGUI TextScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueBox.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TextScript.text = "I would like to ask for a reading on my past present and future";
            DialogueBox.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueBox.SetActive(false);
        }

    }

}
