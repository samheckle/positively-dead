using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int PossibleResponses
    {
        get { return responseOptions.Count; }
    }

    // Use this for initialization
    public Dialogue (string speakerName, string text, float speed, List<Dialogue> dialogueOptions = null, List<string> responseOptions = null) {
        this.speakerName = speakerName;
        this.text = text;
        this.textSpeed = speed;
        this.dialogueOptions = dialogueOptions;
        this.responseOptions = responseOptions;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Returns a response to the dialogue given an index
    /// </summary>
    public Dialogue NextDialogue (int index = 0) {
        if (dialogueOptions != null) {
            return dialogueOptions[index];
        } else {
            return null;
        }
    }
}
