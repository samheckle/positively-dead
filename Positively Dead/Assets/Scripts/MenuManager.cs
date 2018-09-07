using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Attributes
    GameObject[] islandObjects;
    GameObject menuObject, startObject;
    GameObject menuIMG, worldIMG;
    public Button norseButton, egyptButton, diyuButton;

    // Use this for initialization
    void Start()
    {
        // Instantiate
        islandObjects = GameObject.FindGameObjectsWithTag("Island");
        menuObject = GameObject.FindGameObjectWithTag("Menu");
        startObject = GameObject.FindGameObjectWithTag("Start");
        menuIMG = GameObject.FindGameObjectWithTag("MenuIMG");
        worldIMG = GameObject.FindGameObjectWithTag("WSIMG");

        // Give each button its own event listener
        Button btn1 = norseButton.GetComponent<Button>();
        btn1.onClick.AddListener(NorseOnClick);
        Button btn2 = egyptButton.GetComponent<Button>();
        btn2.onClick.AddListener(EgyptOnClick);
        Button btn3 = diyuButton.GetComponent<Button>();
        btn3.onClick.AddListener(DiyuOnClick);
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
        menuIMG.SetActive(false);
        worldIMG.SetActive(true);
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
        menuIMG.SetActive(true);
        worldIMG.SetActive(false);
    }

    // Returns the game to the Main Menu
    public void MainMenu()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene(0);
    }

    // Goes to the Norse Scene
    public void NorseOnClick()
    {
        Debug.Log("Going to Norse!");
        SceneManager.LoadScene(1);
    }

    // Goes to the Egypt Scene
    public void EgyptOnClick()
    {
        Debug.Log("Going to Egypt!");
        SceneManager.LoadScene(2);
    }

    // Goes to the Diyu Scene
    public void DiyuOnClick()
    {
        Debug.Log("Going to Diyu!");
        SceneManager.LoadScene(3);
    }

    // Quits the Game
    public void Quit()
    {
        Application.Quit();
    }
}
