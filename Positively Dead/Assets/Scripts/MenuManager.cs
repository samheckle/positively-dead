using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: JaJuan Webster
/// Main Menu of the game
/// </summary>
public class MenuManager : MonoBehaviour
{
    // Attributes
    GameObject startObject;

    // Use this for initialization
    void Start()
    {
        // Instantiate
        startObject = GameObject.FindGameObjectWithTag("Start");
    }

    /// <summary>
    /// Goes to the intro scene of the game
    /// </summary>
    public void IntroStart()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
}