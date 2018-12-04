using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/// <summary>
/// Author: Nikolas Whiteside
/// Manages a group of dialogue for a particular scene.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    //Types dialogue text to screen
    private Typewriter typer;

    //The current dialogue node
    private Dialogue currentDialogue;

    //The karma built during this scene
    private int playerKarma;

    private OpenScene loadSceneManager;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private Text dialogueBox;

    [SerializeField]
    private Text speakerName;

    [SerializeField]
    private GameObject background;

    //Continue buttons
    public GameObject button1;
    public GameObject button2;
    public bool touchAdvance;

    //Character images
    public GameObject characterLeft;
    public GameObject characterRight;

    // Use this for initialization
    void Start()
    {
        typer = gameObject.GetComponent<Typewriter>();
        loadSceneManager = gameObject.GetComponent<OpenScene>();
        button1.SetActive(false);
        button2.SetActive(false);

        playerKarma = 0;

        LoadScene(sceneName);

        DisplayDialogue(currentDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (typer.animComplete)
        {
            switch (currentDialogue.ResponseCount)
            {
                case 0:
                    OnScreenTap();
                    break;
                case 1:
                    button1.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[0];
                    button1.SetActive(true);
                    break;
                case 2:
                    button1.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[0];
                    button2.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[1];
                    button1.SetActive(true);
                    button2.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Checks dialogue and dynamically adjust it's position based off the Response count
    /// </summary>
    void CheckDialogue()
    {
        switch (currentDialogue.ResponseCount)
        {
            case 0:
                background.transform.position = new Vector3(background.transform.position.x, -3.0f);
                break;
            case 1:
                background.transform.position = new Vector3(background.transform.position.x, -2.5f);
                button1.transform.position = new Vector3(button1.transform.position.x, -4.3f);
                break;
            case 2:
                background.transform.position = new Vector3(background.transform.position.x, -1.9f);
                button1.transform.position = new Vector3(button1.transform.position.x, -3.7f);
                button2.transform.position = new Vector3(button2.transform.position.x, -4.5f);
                break;
            default:
                background.transform.position = new Vector3(background.transform.position.x, -3.0f);
                break;
        }
    }

    /// <summary>
    /// Displays a dialogue to the screen
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    void DisplayDialogue(Dialogue dialogue)
    {
        typer.typeDelay = dialogue.TextSpeed;
        typer.currentDelay = dialogue.TextSpeed;
        typer.finalText = dialogue.Text;

        //If there is a new speaker, swap their images
        if (speakerName.text != "" && speakerName.text != dialogue.SpeakerName)
        {
            StartCoroutine(CharacterSwap(dialogue.SpeakerName));
        }

        speakerName.text = dialogue.SpeakerName;

        CheckDialogue();
    }

    /// <summary>
    /// Swaps character images based on who is speaking
    /// </summary>
    IEnumerator CharacterSwap(string name)
    {
        float startime = Time.time;
        Vector3 char1start = characterLeft.transform.position;
        Vector3 char2start = characterRight.transform.position;
        Vector3 direction = Vector3.zero;

        if (name == characterLeft.name)
        {
            direction = new Vector3(1f, 0, 0);
        }
        else if (name == characterRight.name)
        {
            direction = new Vector3(-1f, 0, 0);
        }

        Vector3 char1end = char1start + direction;

        while (char1start != char1end && ((Time.time - startime) * 4f) < 1f)
        {
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * 4f);

            characterLeft.transform.position += direction * move;
            characterRight.transform.position += direction * move;

            yield return null;
        }
    }

    /// <summary>
    /// Sets the next dialogue on button click
    /// </summary>
    /// <param name="index">Index of next dialogue</param>
    public void OnClick(int index)
    {
        if (typer.animComplete)
        {
            UpdateKarma(currentDialogue.EndsScene);

            //If this is the end of the scene, then load the next unity scene
            if (currentDialogue.EndsScene)
            {
                loadSceneManager.TriggerLoad(nextScene);
            }
            else
            {
                //If the index is beyond the number of options, display the default option
                if (index >= currentDialogue.DialogueCount) index = 0;

                //Display the dialogue at index and deactivate the buttons
                currentDialogue = currentDialogue.NextDialogue(index);

                button1.SetActive(false);
                button2.SetActive(false);
                DisplayDialogue(currentDialogue);
            }
        }
    }

    public void OnClick()
    {
        if (typer.animComplete)
        {
            UpdateKarma(currentDialogue.EndsScene);

            //If this is the end of the scene, then load the next unity scene
            if (currentDialogue.EndsScene)
            {
                loadSceneManager.TriggerLoad(nextScene);
            }
            else
            {
                //Display the dialogue at index and deactivate the buttons
                currentDialogue = currentDialogue.NextDialogue(0);

                button1.SetActive(false);
                button2.SetActive(false);
                DisplayDialogue(currentDialogue);
            }
        }
    }

    /// <summary>
    /// Advances the scene if the user taps the screen, given that both
    /// response buttons are deactivated
    /// </summary>
    public void OnScreenTap()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                OnClick();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    /// <summary>
    /// Loads a scene from the given file
    /// </summary>
    void LoadScene(string filename)
    {
        filename = Application.streamingAssetsPath + "/" + filename;

        if (Application.platform != RuntimePlatform.Android)
        {
            filename = "file://" + filename;
        }

        WWW reader = new WWW(filename);
        while (!reader.isDone) { }

        string jsonString = reader.text;

        List<Dialogue> items = JsonConvert.DeserializeObject<List<Dialogue>>(jsonString);

        currentDialogue = items[0];
    }

    /// <summary>
    /// Updates the player's Karma value and updates the playerPrefs if it is the end of the scene
    /// </summary>
    void UpdateKarma(bool endsScene)
    {
        playerKarma += currentDialogue.Karma;
        if (endsScene)
        {
            if (PlayerPrefs.HasKey("Karma"))
            {
                PlayerPrefs.SetInt("Karma", PlayerPrefs.GetInt("Karma") + playerKarma);
            }
            else
            {
                PlayerPrefs.SetInt("Karma", playerKarma);
            }
        }
    }
}