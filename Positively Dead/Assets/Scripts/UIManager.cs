using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: JaJuan Webster
/// Manages UI in the game
/// </summary>
public class UIManager : MonoBehaviour
{
    // Attributes
    GameObject[] pauseObjects;
    GameObject scoreboard;
    SpriteRenderer backgroundImage;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        Time.timeScale = 1.0f;
        pauseObjects = GameObject.FindGameObjectsWithTag("Menu");
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
        backgroundImage = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        HidePaused();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
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
        foreach (GameObject g in pauseObjects)
            g.SetActive(true);

        if (SceneManager.GetSceneByName("NorseMinigame").isLoaded)
        {
            scoreboard.SetActive(false);
        }

        backgroundImage.color = new Color(0.70f, 0.70f, 0.70f);
    }

    /// <summary>
    /// Hides objects with ShowOnPause tag
    /// </summary>
    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
            g.SetActive(false);

        if (SceneManager.GetSceneByName("NorseMinigame").isLoaded)
        {
            scoreboard.SetActive(true);
        }

        backgroundImage.color = new Color(1, 1, 1);
    }

    /// <summary>
    /// Returns Game to Main Menu
    /// </summary>
    public void MapSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MapSelect", LoadSceneMode.Single);
    }

    /// <summary>
    /// Quits out the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}