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
        EventBroadcaster.Instance.AddObserver("FlipCard", FlipCard);
        EventBroadcaster.Instance.AddObserver("ResetCard", ResetCard);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isSelected", isSelected);
        animator.SetBool("isFlipped", isFlipped);
    }

    void FlipCard()
    {
        if (cardRandomizer.Card1.id == id || cardRandomizer.Card2.id == id || cardRandomizer.Card3.id == id)
            isFlipped = !isFlipped;
    }

    void ResetCard()
    {
        isFlipped = true;
    }
    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("FlipCard");
        EventBroadcaster.Instance.RemoveObserver("ResetCard");
    }
}
