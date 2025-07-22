using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    private float internalClock = 0.0f;
    [SerializeField] float TimeToFlicker = 3.0f;
    private bool TarotReadingStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_PAUSE, this.PauseGame);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESUME, this.ResumeGame);
    }
    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_PAUSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESUME);
    }

    void PauseGame()
    {
        isPaused = true;
    }

    void ResumeGame()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            internalClock += Time.deltaTime;

            if (internalClock >= TimeToFlicker && !TarotReadingStart)
            {
                EventBroadcaster.Instance.PostEvent("StartFlicker");
                TarotReadingStart = true;
            }
        }
    }
}
