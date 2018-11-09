using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: JaJuan Webster
/// Main Menu of the game
/// </summary>
public class MenuManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// Goes to the intro scene of the game
    /// </summary>
    public void IntroStart()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Goes to the map selection scene after dialogue finishes
    /// </summary>
    public void ToMap()
    {
        SceneManager.LoadScene("MapSelection", LoadSceneMode.Single);
    }
}