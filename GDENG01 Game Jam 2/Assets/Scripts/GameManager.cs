using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject StartScreenMenu;
    [SerializeField] GameObject CreditsScreen;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject FirstPersonCameraController;
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
    }
    public void StartGame()
    {
        SetActiveGame(true);
        CloseStartScreenMenu();
    }
    public void CloseGame()
    {
        SetActiveGame(false);
        isPaused = false;
    }
    private void SetActiveGame(bool val)
    {
        isGameStart = val;
        FirstPersonCameraController.SetActive(isGameStart);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
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
