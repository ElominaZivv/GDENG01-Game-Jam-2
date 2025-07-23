using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    bool spread = false;
    bool close = true;
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("spread", this.toggleSpread);
        EventBroadcaster.Instance.AddObserver("closeDeck", this.toggleClose);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void toggleSpread()
    {
        spread = !spread;
        close = !close;
        animator.SetBool("SpreadOut", spread);
        animator.SetBool("CloseDeck", close);
    }
    void toggleClose()
    {
        spread = !spread;
        close = !close;
        animator.SetBool("SpreadOut", spread);
        animator.SetBool("CloseDeck", close);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("spread");
        EventBroadcaster.Instance.RemoveObserver("closeDeck");
    }
}
