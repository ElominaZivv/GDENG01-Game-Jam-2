using UnityEngine;

public class TarotCardController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] string upright_description;
    [SerializeField] string reversed_description;
    [SerializeField] bool upright = true;
    [SerializeField] public bool isSelected = false;
    [SerializeField] bool isFlipped = false;

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
        isFlipped = !isFlipped;
    }
}
