using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: JaJuan Webster
/// Manages UI in the game
/// </summary>
public class UIManager : MonoBehaviour
{
    // Attributes
    GameObject menuObject;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        menuObject = GameObject.FindGameObjectWithTag("Menu");
        HidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        // Uses the ESC button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseControl();
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    /// <summary>
    /// Shows objects with ShowOnPause tag
    /// </summary> 
    public void ShowPaused()
    {
        menuObject.SetActive(true);
    }

    /// <summary>
    /// Hides objects with ShowOnPause tag
    /// </summary>
    public void HidePaused()
    {
        menuObject.SetActive(false);
    }

    /// <summary>
    /// Returns Game to Main Menu
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// Quits out the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}