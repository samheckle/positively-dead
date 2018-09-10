using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Author: Nikolas Whiteside
/// Date: September 6, 2018
/// Description: Manages a group of dialogue for a particular scene.
/// </summary>
public class DialogueManager : MonoBehaviour {
    
    private List<Dialogue> dialogues;
    private Typewriter typer;

    // Single dialogue node
    private Dialogue currentDialogue;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Text dialogueBox;

    [SerializeField]
    private Text speakerName;

    public GameObject button1;
    public GameObject button2;

	// Use this for initialization
	void Start () {
        typer = gameObject.GetComponent<Typewriter>();
        button1.SetActive(false);
        button2.SetActive(false);

		Debug.Log("scene has loaded");
        /* 
         * Open file based on scene name
         * Parse through for character name, text and speed
         * Save each block as it's own dialogue object in the dictionary
         * FOR SAKE OF DEMO - kickstart dialogue runthrough
         */
        Dialogue temp = new Dialogue("Nik", "Eskrrt, nah much bruh, what's tight fam?", 0.01f, new List<Dialogue>() { new Dialogue("Nik", "Suck my asshole", 0.01f) }, new List<string>() { "Goodnight sir" });
        dialogues = new List<Dialogue>() { temp, new Dialogue("Nik", "Aight, be that way bitch.", 0.01f) };

        currentDialogue = new Dialogue("Nik", "Yo, what's up?", 0.05f, dialogues, null);
        currentDialogue.ResponseOptions = new List<string>() { "What's good son?", "Fuck off!" };
        dialogues.Add(new Dialogue("Nik", "*Leaves and goes to Target where he lives*", 0.1f));

        DisplayDialogue(currentDialogue);
	}

    /// <summary>
    /// Displays a dialogue to the screen
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    void DisplayDialogue (Dialogue dialogue)
    {
        typer.typeDelay = dialogue.TextSpeed;
        typer.finalText = dialogue.Text;
        speakerName.text = dialogue.SpeakerName;
    }

    /// <summary>
    /// Sets the next dialogue on button click
    /// </summary>
    /// <param name="index">Index of next dialogue</param>
    public void OnClick(int index) {
        currentDialogue = currentDialogue.NextDialogue(index);
        DisplayDialogue(currentDialogue);
        button1.SetActive(false);
        button2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (typer.animComplete && currentDialogue.PossibleResponses > 0) {
            button1.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[0];
            button1.SetActive(true);
            if (currentDialogue.PossibleResponses > 1) {
                button2.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[1];
                button2.SetActive(true);
            }
        }
	}

    /// <summary>
    /// Loads a scene from the given file
    /// </summary>
    void LoadScene(string fileName) {

    }
}
