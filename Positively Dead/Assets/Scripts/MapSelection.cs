using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: JaJuan Webster
/// Controls the Map Selection of the game
/// </summary>
public class MapSelection : MonoBehaviour
{
    // Attributes
    public Button norseBtn, egyptBtn, diyuBtn;
    static bool norseClicked, egyptClicked, diyuClicked;

    // Use this for initialization
    void Start()
    {
        // Create buttons to add event listeners for the proper function
        Button btn1 = norseBtn.GetComponent<Button>();
        btn1.onClick.AddListener(NorseOnClick);
        Button btn2 = egyptBtn.GetComponent<Button>();
        btn2.onClick.AddListener(EgyptOnClick);
        Button btn3 = diyuBtn.GetComponent<Button>();
        btn3.onClick.AddListener(DiyuOnClick);
        ShowIslands();
    }

    /// <summary>
    /// Shows the island options and the menu buttion
    /// </summary>
    public void ShowIslands()
    {
        if (norseClicked)
        {
            norseBtn.interactable = false;
            diyuBtn.interactable = true;
        }            
        else
        {
            diyuBtn.interactable = false;
            egyptBtn.interactable = false;            
        }

        if (diyuClicked)
        {
            diyuBtn.interactable = false;
            egyptBtn.interactable = true;
        }            
        else
            egyptBtn.interactable = false;

        if (egyptBtn)
            egyptBtn.interactable = false;
    }

    /// <summary>
    /// Returns the game to the Menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// Goes to Norse Scene
    /// </summary>
    public void NorseOnClick()
    {
        norseClicked = true;
        SceneManager.LoadScene("Island 1 - NORSE", LoadSceneMode.Single);
    }

    /// <summary>
    /// Goes to Diyu Scene
    /// </summary>
    public void DiyuOnClick()
    {
        diyuClicked = true;
        SceneManager.LoadScene("Island 2 - DIYU", LoadSceneMode.Single);
    }

    /// <summary>
    /// Goes to Egypt Scene
    /// </summary>
    public void EgyptOnClick()
    {
        egyptClicked = true;
        SceneManager.LoadScene("Island 3 - EGYPT", LoadSceneMode.Single);
    }
}