using UnityEngine;

public class TarotCardController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] public int id;
    [SerializeField] public string name;
    [SerializeField] public string upright_description;
    [SerializeField] public string reversed_description;
    [SerializeField] public bool upright = true;
    [SerializeField] public bool isSelected = false;
    [SerializeField] public bool isFlipped = false;

    [SerializeField] CardRandomizer cardRandomizer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isSelected", isSelected);
        animator.SetBool("isFlipped", isFlipped);
    }

    void OnMouseOver()
    {
        isSelected = true;
    }

    void OnMouseExit()
    {
        isSelected = false;
    }

    void OnMouseDown()
    {
        if (cardRandomizer.CardNumber < 3)
        {
            isFlipped = !isFlipped;
            cardRandomizer.CardID = id;
            EventBroadcaster.Instance.PostEvent("CardChosen");
        }    
    }
}
