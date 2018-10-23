using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles all scene transitions in the game.
/// Author: Nikolas Whiteside
/// Date: 10/12/2018
/// </summary>
public class OpenScene : MonoBehaviour {

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

    IEnumerator LoadScene() {
        yield return new WaitForSeconds(3);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while(!asyncLoad.isDone) {
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
	public void DisplayCanvas(Canvas myCanvas) {
		myCanvas.gameObject.SetActive (!myCanvas.gameObject.activeInHierarchy);
	}
}
