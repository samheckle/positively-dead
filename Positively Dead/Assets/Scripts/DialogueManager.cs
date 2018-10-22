using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

/// <summary>
/// Author: Nikolas Whiteside
/// Date: September 6, 2018
/// Description: Manages a group of dialogue for a particular scene.
/// </summary>
public class DialogueManager : MonoBehaviour {
    
    //Types dialogue text to screen
    private Typewriter typer;
    
    //The current dialogue node
    private Dialogue currentDialogue;

    private OpenScene loadSceneManager;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private Text dialogueBox;

    [SerializeField]
    private Text speakerName;

    //Continue buttons
    public GameObject button1;
    public GameObject button2;
    public GameObject advanceButton;

    //Character images
    public GameObject characterLeft;
    public GameObject characterRight;

	// Use this for initialization
	void Start () {
        typer = gameObject.GetComponent<Typewriter>();
        loadSceneManager = gameObject.GetComponent<OpenScene>();
        button1.SetActive(false);
        button2.SetActive(false);

        LoadScene(sceneName);

        DisplayDialogue(currentDialogue);
    }

    // Update is called once per frame
    void Update() {
        if (typer.animComplete) {
            switch (currentDialogue.ResponseCount) {
                case 0:
                    advanceButton.SetActive(true);
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
    /// Displays a dialogue to the screen
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    void DisplayDialogue (Dialogue dialogue) {
        typer.typeDelay = dialogue.TextSpeed;
        typer.finalText = dialogue.Text;

        //If there is a new speaker, swap their images
        if (speakerName.text != "" && speakerName.text != dialogue.SpeakerName) {
            StartCoroutine(CharacterSwap(dialogue.SpeakerName));
        }

        speakerName.text = dialogue.SpeakerName;
    }

    /// <summary>
    /// Swaps character images based on who is speaking
    /// </summary>
    IEnumerator CharacterSwap (string name) {
        float startime = Time.time;
        Vector3 char1start = characterLeft.transform.position;
        Vector3 char2start = characterRight.transform.position;
        Vector3 direction = Vector3.zero;

        if (name == characterLeft.tag)
        {
            direction = new Vector3(1f, 0, 0);
        }
        else if (name == characterRight.tag)
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
    public void OnClick (int index) {
        //If this is the end of the scene, then load the next unity scene
        if (currentDialogue.EndsScene) {
            SceneManager.LoadScene(nextScene);
        } else {

            //If the index is beyond the number of options, display the default option
            if (index >= currentDialogue.DialogueCount) index = 0;

            //Display the dialogue at index and deactivate the buttons
            currentDialogue = currentDialogue.NextDialogue(index);
            Debug.Log(currentDialogue.DialogueOptions);

            DisplayDialogue(currentDialogue);
            button1.SetActive(false);
            button2.SetActive(false);
        }
    }

    /// <summary>
    /// Advances the scene if the user taps the screen, given that both
    /// response buttons are deactivated
    /// </summary>
    public void OnScreenTap ()
    {
        //If this is the end of the scene, then load the next unity scene
        if (currentDialogue.EndsScene)
        {
            loadSceneManager.TriggerLoad(nextScene);
        }
        else
        {
            //Display the dialogue at index and deactivate the buttons
            currentDialogue = currentDialogue.NextDialogue(0);

            DisplayDialogue(currentDialogue);
            advanceButton.SetActive(false);
        }
    }

    /// <summary>
    /// Loads a scene from the given file
    /// </summary>
    void LoadScene (string filename) {
        filename = "Assets/DialogueScripts/" + filename;
        using (StreamReader r = new StreamReader(filename)) {
            string json = r.ReadToEnd();
            List<Dialogue> items = JsonConvert.DeserializeObject<List<Dialogue>>(json);

            /*Dialogue tempDialogue;
            for(int i = 0; i < items.Count - 1; i++) {
                tempDialogue = items[i];
                Debug.Log("Current dialogue: " + tempDialogue.Text);
                if (tempDialogue.DialogueOptions == null) {
                    if (tempDialogue.IsLeaf && items[i + 1].IsLeaf && (i + 2) < items.Count) {
                        Debug.Log("From a leaf: " + items[i + 2].Text);
                        tempDialogue.AddDialogueOption(items[i + 2]);
                    }
                    else {
                        Debug.Log("Next in list: " + items[i + 1].Text);
                        tempDialogue.AddDialogueOption(items[i + 1]);
                    }
                } else {
                    Debug.Log("REMOVE");
                    items.RemoveAt(i);
                    i--;
                }
            }*/

            currentDialogue = items[0];
        }
    }
}
