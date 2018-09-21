using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/// <summary>
/// Author: Nikolas Whiteside
/// Date: September 6, 2018
/// Description: A data container for dialogue.
/// </summary>
public class Dialogue {

    // Dialogue information
    private string text = "";
    private string speakerName = "";

    // Animation information
    private float textSpeed;

    // Dialogue tree options
    private List<Dialogue> dialogueOptions;
    private List<string> responseOptions;

    private bool endsScene;

    public string Text {
        get { return text; }
        set { text = value; }
    }

    public string SpeakerName
    {
        get { return speakerName; }
        set { speakerName = value; }
    }

    public float TextSpeed
    {
        get { return textSpeed; }
        set { textSpeed = value; }
    }

    public List<string> ResponseOptions
    {
        get { return responseOptions; }
        set { responseOptions = value; }
    }

    public int ResponseCount
    {
        get {
            if (responseOptions == null)
            {
                return 0;
            }
            return responseOptions.Count;
        }
    }

    public int DialogueCount
    {
        get
        {
            if (responseOptions == null)
            {
                return 0;
            }
            return dialogueOptions.Count;
        }
    }

    public bool EndsScene
    {
        get
        {
            return endsScene;
        }
    }

    /// <summary>
    /// Constructor that only takes dialogue text and a speed.
    /// </summary>
    /// <param name="speakerName">Name of character speaking</param>
    /// <param name="text">Dialogue text</param>
    /// <param name="speed">Speed to write the text to the screen</param>
    public Dialogue (string speakerName, string text, float speed) {
        this.speakerName = speakerName;
        this.text = text;
        this.textSpeed = speed;
        this.dialogueOptions = new List<Dialogue>(2);
        this.responseOptions = new List<string>(2);
    }

    /// <summary>
    /// Constructor that allows the user to add lists of dialogue
    /// </summary>
    /// <param name="speakerName">Name of character speaking</param>
    /// <param name="text">Dialogue text</param>
    /// <param name="speed">Speed to write the text to the screen</param>
    /// <param name="dialogueOptions">List of all possible next dialogues</param>
    /// <param name="responseOptions">List of possible player responses</param>
    public Dialogue (string speakerName, string text, float speed, List<Dialogue> dialogueOptions, List<string> responseOptions) {
        this.speakerName = speakerName;
        this.text = text;
        this.textSpeed = speed;
        this.dialogueOptions = dialogueOptions;
        this.responseOptions = responseOptions;
    }

    /// <summary>
    /// Constructor that accepts one dialogue and response
    /// </summary>
    /// <param name="speakerName">Name of character speaking</param>
    /// <param name="text">Dialogue text</param>
    /// <param name="speed">Speed to write the text to the screen</param>
    /// <param name="nextDialogue">The next dialogue</param>
    /// <param name="response">The player response</param>
    public Dialogue (string speakerName, string text, float speed, Dialogue nextDialogue, string response = "Continue") {
        this.speakerName = speakerName;
        this.text = text;
        this.textSpeed = speed;
        this.dialogueOptions = new List<Dialogue>(1) { nextDialogue };
        this.responseOptions = new List<string>(1) { response };
    }

    [JsonConstructor]
    public Dialogue (string speakerName, string text, float textSpeed, List<string> responseOptions, List<Dialogue> dialogueOptions, bool endsScene = false) {
        this.speakerName = speakerName;
        this.text = text;
        this.textSpeed = textSpeed;
        this.responseOptions = responseOptions;
        this.dialogueOptions = dialogueOptions;
        this.endsScene = endsScene;
    }

    public void AddDialogueOptions (Dialogue option) {
        dialogueOptions.Add(option);
    }

    /// <summary>
    /// Returns a response to the dialogue given an index
    /// </summary>
    public Dialogue NextDialogue (int index = 0) {
        if (dialogueOptions != null && dialogueOptions.Count > index) {
            return dialogueOptions[index];
        } else {
            return null;
        }
    }
}
