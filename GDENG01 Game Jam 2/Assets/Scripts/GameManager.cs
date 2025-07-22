using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject StartScreenMenu;
    [SerializeField] GameObject CreditsScreen;
    [SerializeField] GameObject PauseScreen;
    private bool isPaused = false;
    bool isStartScreenOpen = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void CloseStartScreenMenu()
    {
        StartScreenMenu.SetActive(false);
        CloseCreditsScreen();
        isStartScreenOpen = false;
    }
    public void OpenStartScreenMenu()
    {
        StartScreenMenu.SetActive(true);
        isStartScreenOpen = true;
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
    private void TogglePause()
    {
        isPaused = !isPaused;
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStartScreenOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isStartScreenOpen && Input.GetKeyDown(KeyCode.X))
        {
            isStartScreenOpen = true;
            OpenStartScreenMenu();
        }
    }
}
