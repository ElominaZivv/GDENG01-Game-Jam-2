using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    bool spread = false;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("spread", this.toggleSpread);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void toggleSpread()
    {
        spread = !spread;
        animator.SetBool("SpreadOut", spread);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("spread");
    }
}
