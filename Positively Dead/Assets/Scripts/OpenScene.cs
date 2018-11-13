using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Nikolas Whiteside
/// Handles all scene transitions in the game. 
/// </summary>
public class OpenScene : MonoBehaviour
{
    private bool loadScene = false;

    [SerializeField]
    private string scene;

    [SerializeField]
    private Text loadingText;

    [SerializeField]
    private GameObject loadingPanel;

    void Start()
    {
        loadingPanel.SetActive(false);
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            //display the load progress
            loadingText.text = "Loading " + (asyncLoad.progress * 100) + "%";
            //provide tap to continue option
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Tap to Continue";
                if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// Triggers a loading screen with the original scene name
    /// </summary>
    public void TriggerLoad()
    {
        if (!loadScene)
        {
            loadScene = true;
            loadingPanel.SetActive(true);
            loadingText.text = "Loading...";
            StartCoroutine(LoadScene());
        }
    }

    /// <summary>
    /// Updates the scene name and triggers a loading screen
    /// </summary>
    /// <param name="scene"></param>
    public void TriggerLoad(string scene)
    {
        this.scene = scene;
        if (!loadScene)
        {
            loadScene = true;
            loadingPanel.SetActive(true);
            loadingText.text = "Loading...";
            StartCoroutine(LoadScene());
        }
    }

    /// <summary>
    /// Toggles the display of a canvas on or off.
    /// </summary>
    /// <param name="canvasName">The canvas to display.</param>
    public void DisplayCanvas(Canvas myCanvas)
    {
        myCanvas.gameObject.SetActive(!myCanvas.gameObject.activeInHierarchy);
    }
}