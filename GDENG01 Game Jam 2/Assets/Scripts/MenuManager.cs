using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject StartScreenMenu;
    [SerializeField] GameObject CreditsScreen;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject Game;
    private bool isPaused = false;
    private bool isStartScreenOpen = true;
    private bool isGameStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseGame();
        OpenStartScreenMenu();
        EventBroadcaster.Instance.AddObserver(EventNames.OPEN_START_MENU, this.OpenStartScreenMenu);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_START, this.StartGame);
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.OPEN_START_MENU);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_START);
    }

    public void CloseStartScreenMenu()
    {
        isStartScreenOpen = false;
        StartScreenMenu.SetActive(isStartScreenOpen);
        CloseCreditsScreen();
    }
    public void OpenStartScreenMenu()
    {
        isStartScreenOpen = true;
        StartScreenMenu.SetActive(isStartScreenOpen);
        CloseGame();
    }
    public void CloseCreditsScreen()
    {
        CreditsScreen.SetActive(false);
    }
    public void OpenCreditsScreen()
    {
        CreditsScreen.SetActive(true);
    }
    private void PauseGame()
    {
        PauseScreen.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        Parameters pauseVal = new Parameters();
        pauseVal.PutExtra("PauseVal", isPaused);
        EventBroadcaster.Instance.PostEvent(EventNames.GAME_PAUSE, pauseVal);
    }
    public void ClickStartGame()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.GAME_START);
    }
    public void StartGame()
    {
        SetActiveGame(true);
        CloseStartScreenMenu();
        SetPause(false);
    }
    public void CloseGame()
    {
        SetActiveGame(false);
        SetPause(false);
    }
    private void SetActiveGame(bool val)
    {
        isGameStart = val;
        Game.SetActive(isGameStart);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        PauseGame();
    }
    private void SetPause(bool val)
    {
        isPaused = val;
        PauseGame();
    }

    // Update is called once per frame
    void Update()   
    {
        if (!isStartScreenOpen && isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.GAME_RESUME);
        }
        // During Gameplay
            if (!isStartScreenOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }

        if (!isStartScreenOpen && Input.GetKeyDown(KeyCode.X))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.OPEN_START_MENU);
        }
    }
}
