using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Attributes
    // GameObject[] pauseObjects;
    GameObject menuObject;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        //pauseObjects = GameObject.FindGameObjectsWithTag("");
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
    /// For when the game needs to be paused
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


    // Shows objects with ShowOnPause tag
    public void ShowPaused()
    {
        /*
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        */
        menuObject.SetActive(true);
    }

    // Hides objects with ShowOnPause tag
    public void HidePaused()
    {
        /*
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);            
        }
        */
        menuObject.SetActive(false);
    }

    // Returns the game to the Main Menu and sets the time scale back to 1
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Loads inputted level
    public void LoadLevel()
    {
        // Brings game back to main menu        
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        menuObject.SetActive(false);
    }

    // Quits the Game
    public void Quit()
    {
        Application.Quit();
    }
}
