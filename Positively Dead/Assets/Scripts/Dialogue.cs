using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue {

    // Dialogue information
    private string text = "";
    private string speakerName = "";

    // Animation information
    private float textSpeed;

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

    // Use this for initialization
    public Dialogue(string speakerName, string text) {
        this.speakerName = speakerName;
        this.text = text;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
