using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Attributes
    GameObject[] islandObjects;                             
    GameObject menuObject, startObject;                     
    GameObject menuIMG, worldIMG;
    public Button norseBtn, egyptBtn, diyuBtn;              
    static bool norseClicked, egyptClicked, diyuClicked;

    // Use this for initialization
    void Start()
    {
        // Instantiate
        islandObjects = GameObject.FindGameObjectsWithTag("Island");
        menuObject = GameObject.FindGameObjectWithTag("Menu");
        startObject = GameObject.FindGameObjectWithTag("Start");
        menuIMG = GameObject.FindGameObjectWithTag("MenuIMG");
        worldIMG = GameObject.FindGameObjectWithTag("WSIMG");

        // Create buttons to add event listeners for the proper function
        Button btn1 = norseBtn.GetComponent<Button>();
        btn1.onClick.AddListener(NorseOnClick);
        Button btn2 = egyptBtn.GetComponent<Button>();
        btn2.onClick.AddListener(EgyptOnClick);
        Button btn3 = diyuBtn.GetComponent<Button>();
        btn3.onClick.AddListener(DiyuOnClick);
        HideIslands();
    }

    // Shows the island options and the menu buttion
    // Buttons will be active depending if they've been clicked on before
    public void ShowIslands()
    {
        if (norseClicked)
            norseBtn.interactable = false;
        if (egyptClicked)
            egyptBtn.interactable = false;
        if (diyuClicked)
            diyuBtn.interactable = false;

        foreach (GameObject g in islandObjects)
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
        menuIMG.SetActive(true);
        worldIMG.SetActive(false);
        startObject.SetActive(true);
        menuObject.SetActive(false);
    }

    // Goes to the Norse Scene
    public void NorseOnClick()
    {
        norseClicked = true;
        SceneManager.LoadScene(1);
    }

    // Goes to the Egypt Scene
    public void EgyptOnClick()
    {
        egyptClicked = true;
        SceneManager.LoadScene(2);
    }

    // Goes to the Diyu Scene
    public void DiyuOnClick()
    {
        diyuClicked = true;
        SceneManager.LoadScene(3);
    }

    // Quits the Game (place holder function if needed)
    public void Quit()
    {
        Application.Quit();
    }
}
