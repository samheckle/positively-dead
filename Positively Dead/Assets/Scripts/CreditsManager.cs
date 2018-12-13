using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// Author: JaJuan Webster
/// Main Menu of the game
/// </summary>
public class CreditsManager : MonoBehaviour
{
    public List<GameObject> menuUI;
    public List<GameObject> creditsUI;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject g in menuUI)
            g.SetActive(false);

        foreach (GameObject g in creditsUI)
            g.SetActive(true);
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

    public void Credits()
    {
        foreach (GameObject g in creditsUI)
            g.SetActive(true);

        foreach (GameObject g in menuUI)
            g.SetActive(false);
    }

    public void ToMenu()
    {
        foreach (GameObject g in menuUI)
            g.SetActive(true);

        foreach (GameObject g in creditsUI)
            g.SetActive(false);
    }
}