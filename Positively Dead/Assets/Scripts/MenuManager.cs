using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Attributes
    GameObject[] islandObjects;
    GameObject menuObject;
    GameObject startObject;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        islandObjects = GameObject.FindGameObjectsWithTag("Island");
        menuObject = GameObject.FindGameObjectWithTag("Menu");
        startObject = GameObject.FindGameObjectWithTag("Start");
        HideIslands();
    }

    // Shows the island options and the menu buttion
    public void ShowIslands()
    {
        foreach(GameObject g in islandObjects)
        {
            g.SetActive(true);
        }
        menuObject.SetActive(true);
        startObject.SetActive(false);
    }

    // Hides the island options and the menu buttion
    public void HideIslands()
    {
        foreach (GameObject g in islandObjects)
        {
            g.SetActive(false);
        }
        menuObject.SetActive(false);
        startObject.SetActive(true);
    }

    // Returns the game to the Main Menu
    public void MainMenu()
    {
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
