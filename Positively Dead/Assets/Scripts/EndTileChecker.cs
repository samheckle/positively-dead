using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTileChecker : MonoBehaviour
{
    private GameObject player;

    private Text levelTxt;

    private Text levelTxt2;

    public List<GameObject> levelOverObjects;

    private GameObject canvas = null;
    private OpenScene loadSceneManager;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas)
        {
            loadSceneManager = canvas.GetComponent<OpenScene>();
        }
        levelTxt = levelOverObjects[0].GetComponent<Text>();
        levelTxt2 = levelOverObjects[2].GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 0.2f)
        {
            if (SceneManager.GetActiveScene().name != "ChineseMinigame4")
                StartCoroutine(WaitForKeyDown(KeyCode.KeypadEnter));
            //else
                
        }

        if (timer != 0)
            foreach (GameObject g in levelOverObjects)
                g.SetActive(true);
        else
            foreach (GameObject g in levelOverObjects)
                g.SetActive(false);
    }

    void PopUpText()
    {
        levelTxt.text = "Tap to Continue";
        levelTxt2.text = "You Made It!";       
    }

    float timer = 0.0f;
    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        timer += Time.unscaledDeltaTime;
        PopUpText();
        if (Input.touchCount > 0 || Input.GetKeyDown(keyCode) || Input.GetMouseButtonDown(0))
        {
            //Resume Game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            timer = 0;
        }
        yield return null;
    }
}
