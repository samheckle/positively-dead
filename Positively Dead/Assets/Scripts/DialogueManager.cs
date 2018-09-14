using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
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

        LoadScene(sceneName);

        DisplayDialogue(currentDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (typer.animComplete && currentDialogue.PossibleResponses > 0)
        {
            button1.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[0];
            button1.SetActive(true);
            if (currentDialogue.PossibleResponses > 1)
            {
                button2.GetComponentInChildren<Text>().text = currentDialogue.ResponseOptions[1];
                button2.SetActive(true);
            }
        }
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

    /// <summary>
    /// Loads a scene from the given file
    /// </summary>
    void LoadScene(string filename)
    {
        filename = "Assets/DialogueScripts/" + filename;
        using (StreamReader r = new StreamReader(filename))
        {
            string json = r.ReadToEnd();
            List<Dialogue> items = JsonConvert.DeserializeObject<List<Dialogue>>(json);
            currentDialogue = items[0];
        }
    }
}
